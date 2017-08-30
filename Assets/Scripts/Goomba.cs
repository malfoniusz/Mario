using UnityEngine;

public class Goomba : MonoBehaviour
{
    public GameObject pointsFloating;
    public int points = 100;
    public float speed = 30;
    public float bounceHeight = 300;
    public float changeDirectionDelay = 0.1f;
    public float minimalVelocity = 1f;
    public float minimalJumpHeight = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    private BoxCollider2D objectCollider;
    private BoxCollider2D triggerCollider;
    private Vector2 direction = Vector2.left;
    private float time = 0;
    private int playerMask;
    private int enemyMask;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        objectCollider = GetComponent<BoxCollider2D>();
        triggerCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        playerMask = LayerMask.NameToLayer("Player");
        enemyMask = LayerMask.NameToLayer("Enemy");
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(playerMask, enemyMask);
        UpdateVelocity();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (Mathf.Abs(rb.velocity.x) < minimalVelocity && time > changeDirectionDelay)
        {
            time = 0;
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        direction *= -1;
        UpdateVelocity();
    }

    void UpdateVelocity()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform playerTrans = collision.gameObject.transform;
            if (playerTrans.position.y > transform.position.y + minimalJumpHeight)
            {
                Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRB.velocity = new Vector2(playerRB.velocity.x, bounceHeight);

                GameObject pointsObject = Instantiate(pointsFloating);
                pointsObject.transform.GetChild(0).position = transform.GetChild(0).position;
                pointsObject.GetComponent<PointsFloating>().SetPoints(PlayerCombo.Combo(points));

                anim.SetTrigger("IsDead");
                DisableObject();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player touched enemy or stays in enemy - hurt player

            // Debug.Log("NOT TOP");
        }
    }

    private void DisableObject()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
        audioSource.Play();
        rb.isKinematic = true;
        objectCollider.enabled = false;
        triggerCollider.enabled = false;
    }

    private void Event_Destroy()
    {
        Destroy(gameObject);
    }

}
