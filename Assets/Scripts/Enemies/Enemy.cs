using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public bool stop = false;

    public AudioClip kickClip;
    public BoxCollider2D objectCollider;
    public BoxCollider2D triggerCollider;
    public GameObject pointsFloating;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public int points = 100;
    public float speed = 30;
    public float bounceHeight = 200;
    
    protected GameObject player;
    protected Rigidbody2D rb;
    protected PlayerDeath playerDeath;
    protected float time = 0;
    protected int direction = -1;
    
    private float colliderHeight;
    private bool activated = false;
    private Vector2 savedVelocity = Vector2.zero;
    private const float PLAYER_FALLING_FAST = 400f;
    private WallBounce wallBounce = new WallBounce();
    private float PLAYER_IMMUNITY_DURATION = 0.4f;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerDeath = player.GetComponent<PlayerDeath>();
        colliderHeight = objectCollider.size.y;
    }

    private void Start()
    {
        Physics2D.IgnoreCollision(objectCollider, player.GetComponent<BoxCollider2D>());
    }

    protected virtual void Update()
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
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
        bool bounce = wallBounce.Bounce(rb, speed);
        if (bounce)
        {
            ChangeDirectionBehaviour();
        }
    }

    protected virtual void ChangeDirectionBehaviour()
    {
        direction *= -1;
        UpdateVelocity();
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
        pointsObject.transform.GetChild(0).position = transform.position;
        pointsObject.GetComponent<PointsFloating>().SetPoints(ComboPoints.Combo(points), false);
    }

    protected virtual void EnemyStompedBehaviour()
    {
        Destroy(gameObject);
    }

    protected void DisableObject()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        objectCollider.enabled = false;
        triggerCollider.enabled = false;
    }

    public void EnemyFireballBehaviour(int fallDirection)
    {
        DisableObject();
        rb.velocity = new Vector2(fallDirection * speed, 0);
        spriteRenderer.flipY = true;

        anim.SetTrigger("DeathByFireball");
        audioSource.clip = kickClip;
        audioSource.Play();
        PointsSpawn();
    }

}
