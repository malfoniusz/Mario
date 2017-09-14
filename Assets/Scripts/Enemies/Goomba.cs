using UnityEngine;

public class Goomba : MonoBehaviour
{
    static public bool stop = false;

    public GameObject pointsFloating;
    public int points = 100;
    public float speed = 30;
    public float bounceHeight = 200;

    protected float MINIMAL_HEIGHT_BIG = 10f;
    private const float MINIMAL_HEIGHT_SMALL = 1f;
    private const float PLAYER_VELOCITY_SWITCH = 300f;

    private GameObject child;
    private PlayerDeath playerDeath;
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    private BoxCollider2D objectCollider;
    private BoxCollider2D triggerCollider;
    private Vector2 direction = Vector2.left;
    private int playerMask;
    private int enemyMask;
    private bool activated = false;
    private Vector2 savedVelocity = Vector2.zero;
    private WallBounce wallBounce = new WallBounce();

    protected virtual void Awake()
    {
        child = transform.GetChild(0).gameObject;
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
        StopAndResume();

        if (!stop)
        {
            MovingBehaviour();
        }
    }

    void StopAndResume()
    {
        if (stop && savedVelocity == Vector2.zero)
        {
            savedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            anim.enabled = false;
        }

        if (!stop && savedVelocity != Vector2.zero)
        {
            rb.velocity = savedVelocity;
            savedVelocity = Vector2.zero;
            anim.enabled = true;
        }
    }

    void MovingBehaviour()
    {
        if (activated == false)
        {
            CheckVisibility();
        }
        else
        {
            bool bounce = wallBounce.Bounce(rb);
            if (bounce)
            {
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
            float minimalHeight = playerVelYAbs < PLAYER_VELOCITY_SWITCH ? MINIMAL_HEIGHT_BIG : MINIMAL_HEIGHT_SMALL;

            if (playerTrans.position.y > transform.position.y + minimalHeight)
            {
                Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRB.velocity = new Vector2(playerRB.velocity.x, bounceHeight);

                GameObject pointsObject = Instantiate(pointsFloating);
                pointsObject.transform.GetChild(0).position = transform.GetChild(0).position;
                pointsObject.GetComponent<PointsFloating>().SetPoints(PlayerCombo.Combo(points), false);

                EnemyStomped(collision);
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

    protected virtual void EnemyStomped(Collider2D collision)
    {
        anim.SetTrigger("IsDead");
        audioSource.Play();
        DisableObject();
    }

    private void DisableObject()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        objectCollider.enabled = false;
        triggerCollider.enabled = false;
    }

    private void Event_Destroy()
    {
        Destroy(gameObject);
    }

}
