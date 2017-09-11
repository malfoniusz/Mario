using UnityEngine;

public class Goomba : MonoBehaviour
{
    public GameObject child;
    public GameObject pointsFloating;
    public int points = 100;
    public float speed = 30;
    public float bounceHeight = 300;

    private PlayerDeath playerDeath;
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    private BoxCollider2D objectCollider;
    private BoxCollider2D triggerCollider;
    private Vector2 direction = Vector2.left;
    private float directionDelay = 0.1f;
    private float directionTime = 0;
    private float minimalVelocity = 1f;
    private int playerMask;
    private int enemyMask;
    private bool activated = false;

    private void Awake()
    {
        playerDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        objectCollider = GetComponent<BoxCollider2D>();
        triggerCollider = child.GetComponent<BoxCollider2D>();
        playerMask = LayerMask.NameToLayer("Player");
        enemyMask = LayerMask.NameToLayer("Enemy");
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(playerMask, enemyMask);
    }

    private void Update()
    {
        if (activated == false)
        {
            CheckVisibility();
        }
        else
        {
            directionTime += Time.deltaTime;

            if (Mathf.Abs(rb.velocity.x) < minimalVelocity && directionTime > directionDelay)
            {
                directionTime = 0;
                ChangeDirection();
            }
        }
    }

    void CheckVisibility()
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        bool camVis = (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1);

        if (camVis == true)
        {
            activated = true;
            UpdateVelocity();
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
            float playerVelYAbs = Mathf.Abs(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);

            const float MINIMAL_HEIGHT_BIG = 10f;
            const float MINIMAL_HEIGHT_SMALL = 1f;
            const float PLAYER_VELOCITY_SWITCH = 300f;

            float minimalHeight = playerVelYAbs < PLAYER_VELOCITY_SWITCH ? MINIMAL_HEIGHT_BIG : MINIMAL_HEIGHT_SMALL;

            if (playerTrans.position.y > transform.position.y + minimalHeight)
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
            playerDeath.Die();
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
