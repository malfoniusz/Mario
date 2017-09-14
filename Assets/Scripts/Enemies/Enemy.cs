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
    private float colliderHeight;

    private PlayerDeath playerDeath;
    private Vector2 direction = Vector2.left;
    private int playerMask;
    private int enemyMask;
    private bool activated = false;
    private Vector2 savedVelocity = Vector2.zero;
    private const float PLAYER_FALLING_FAST = 400f;
    private WallBounce wallBounce = new WallBounce();

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

    void ChangeDirection()
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
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyCollision(collision);
        }
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

    private void EnemyStomped(Collider2D collision)
    {
        PlayerBounce(collision);
        PointsSpawn();
        EnemyStompedBehaviour();
    }

    private void PlayerBounce(Collider2D collision)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDeath.Die();
        }
    }

}
