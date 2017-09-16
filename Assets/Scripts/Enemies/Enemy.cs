using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public bool stop = false;

    public GameObject pointsFloating;
    public int points = 100;
    public float speed = 30;
    public float bounceHeight = 200;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerDeath playerDeath;
    protected float time = 0;
    protected int direction = -1;

    private float colliderHeight;
    private int playerMask;
    private int enemyMask;
    private bool activated = false;
    private Vector2 savedVelocity = Vector2.zero;
    private const float PLAYER_FALLING_FAST = 400f;
    private WallBounce wallBounce = new WallBounce();
    private float PLAYER_IMMUNITY_DURATION = 0.4f;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliderHeight = GetComponent<BoxCollider2D>().size.y;

        playerDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
        playerMask = LayerMask.NameToLayer("Player");
        enemyMask = LayerMask.NameToLayer("Enemy");
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(playerMask, enemyMask);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        StopAndResume();

        if (!stop)
        {
            MovingBehaviour();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyCollision(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && time > PLAYER_IMMUNITY_DURATION)
        {
            EnemyKillingPlayer();
        }
    }

    protected virtual void EnemyKillingPlayer()
    {
        playerDeath.Die();
    }

    void StopAndResume()
    {
        if (stop && savedVelocity == Vector2.zero)
        {
            savedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            if (anim != null) anim.enabled = false;
        }

        if (!stop && savedVelocity != Vector2.zero)
        {
            rb.velocity = savedVelocity;
            savedVelocity = Vector2.zero;
            if (anim != null) anim.enabled = true;
        }
    }

    protected virtual void MovingBehaviour()
    {
        if (activated == false)
        {
            CheckVisibility();
        }
        else
        {
            ChangeDirection();
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

    protected void ChangeDirection()
    {
        bool bounce = wallBounce.Bounce(rb);
        if (bounce)
        {
            direction *= -1;
            UpdateVelocity();
        }
    }

    void UpdateVelocity()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    protected virtual void EnemyCollision(Collider2D collision)
    {
        Transform playerTrans = collision.gameObject.transform;
        float playerVelYAbs = Mathf.Abs(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);

        bool fallingDownFast = playerVelYAbs > PLAYER_FALLING_FAST;
        // sideToSide | player.y = enemy.y --- stomp | player.y - playerHeight/2 = enemy.y + enemyHeight/2 | playerHeight/2 is the allowed threshold
        if (playerTrans.position.y > transform.position.y + colliderHeight / 2 || fallingDownFast)
        {
            EnemyStomped(collision);
        }
    }

    protected virtual void EnemyStomped(Collider2D collision)
    {
        time = 0;
        PlayerBounce(collision);
        PointsSpawn();
        EnemyStompedBehaviour();
    }

    protected void PlayerBounce(Collider2D collision)
    {
        Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
        playerRB.velocity = new Vector2(playerRB.velocity.x, bounceHeight);
    }

    private void PointsSpawn()
    {
        GameObject pointsObject = Instantiate(pointsFloating);
        pointsObject.transform.GetChild(0).position = transform.GetChild(0).position;
        pointsObject.GetComponent<PointsFloating>().SetPoints(PlayerCombo.Combo(points), false);
    }

    protected virtual void EnemyStompedBehaviour()
    {
        Destroy(gameObject);
    }

}
