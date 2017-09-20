using UnityEngine;

public class Enemy : Moving
{
    public AudioSource audioSource;
    public AudioClip kickClip;
    public SpriteRenderer spriteRenderer;
    public float bounceHeight = 200;

    protected GameObject player;
    protected PlayerDeath playerDeath;
    protected float time = 0;

    private float PLAYER_IMMUNITY_DURATION = 0.4f;
    private const float PLAYER_FALLING_FAST = 400f;
    private float colliderHeight;
    private bool activated = false;

    protected override void Awake()
    {
        base.Awake();
        colliderHeight = objectCollider.size.y;
        player = GameObject.FindGameObjectWithTag("Player");
        playerDeath = player.GetComponent<PlayerDeath>();
    }

    protected override void Start()
    {
        Physics2D.IgnoreCollision(objectCollider, player.GetComponent<BoxCollider2D>());
    }

    protected virtual void Update()
    {
        time += Time.deltaTime;
    }

    protected override void MovingBehaviour()
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

    protected override void CollisionEnter(Collider2D collision)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && time > PLAYER_IMMUNITY_DURATION)
        {
            EnemyKillingPlayer();
        }
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

    protected virtual void EnemyKillingPlayer()
    {
        playerDeath.Die();
    }

    protected virtual void EnemyStomped(Collider2D collision)
    {
        time = 0;
        PlayerBounce(collision);
        PointsSpawn();
        EnemyStompedBehaviour();
    }

    protected virtual void EnemyStompedBehaviour()
    {
        Destroy(parent);
    }

    protected void PlayerBounce(Collider2D collision)
    {
        Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
        playerRB.velocity = new Vector2(playerRB.velocity.x, bounceHeight);
    }

    protected void DisableObject()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        objectCollider.enabled = false;
        triggerCollider.enabled = false;
    }

}
