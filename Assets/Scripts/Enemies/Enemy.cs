using UnityEngine;

public class Enemy : Moving
{
    public AudioSource audioSource;
    public AudioClip kickClip;
    public SpriteRenderer spriteRenderer;
    public Transform[] stompedChecks;
    public float bounceHeight = 200;

    protected PlayerPowerup playerPowerup;
    protected PlayerInvincibility playerInv;
    protected float time = 0;

    private const float COLLISION_ERROR = 3f;
    private float PLAYER_IMMUNITY_DURATION = 0.4f;
    private bool activated = false;
    private Vector2 FIREBALL_KNOCKBACK = new Vector2(60, 320);
    private float FIREBALL_DESTRUCTION_DELAY = 2f;

    protected override void Awake()
    {
        base.Awake();
        playerPowerup = player.GetComponent<PlayerPowerup>();
        playerInv = player.GetComponent<PlayerInvincibility>();
    }

    protected virtual void Update()
    {
        time += Time.deltaTime;
    }

    protected override void MovingBehaviour()
    {
        if (activated == false) CheckVisibility();
        else                    base.MovingBehaviour();
    }

    protected override void CollisionEnter(Collider2D collision)
    {
        bool enemyStomped = Contact.ContactPoints(stompedChecks, LayerNames.GetPlayer());
        float playerVelY = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        bool playerFalling = (playerVelY < 0);

        if (enemyStomped && playerFalling)
        {
            EnemyStomped(collision);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagNames.player)
        {
            if (playerInv.GetInvincible())  HitByInvPlayer(collision);
            else                            CollisionEnter(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagNames.player)
        {
            if (playerInv.GetInvincible())              HitByInvPlayer(collision);
            else if (time > PLAYER_IMMUNITY_DURATION)   EnemyHittingPlayer();
        }
    }

    protected void HitByInvPlayer(Collider2D playerCollider)
    {
        float hitDirection = Mathf.Sign(player.GetComponent<Rigidbody2D>().velocity.x);
        HitByFireball(hitDirection);
    }

    public void HitByFireball(float fallDirection)
    {
        DisableObject(false, false);
        rb.velocity = new Vector2(fallDirection * FIREBALL_KNOCKBACK.x, FIREBALL_KNOCKBACK.y);
        spriteRenderer.flipY = true;

        audioSource.clip = kickClip;
        audioSource.Play();
        SpawnComboPoints();

        StartCoroutine(Destruction.DelayedDestroy(FIREBALL_DESTRUCTION_DELAY, gameObject));
    }

    void CheckVisibility()
    {
        Vector3 visTest = TagNames.GetCamera().WorldToViewportPoint(transform.position);
        bool camVis = (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1);

        if (camVis == true)
        {
            activated = true;
            UpdateVelocity();
        }
    }

    protected virtual void EnemyHittingPlayer()
    {
        playerPowerup.PlayerHit();
    }

    protected virtual void EnemyStomped(Collider2D collision)
    {
        time = 0;
        PlayerBounce(collision);
        SpawnComboPoints();
        EnemyStompedBehaviour();
    }

    protected virtual void EnemyStompedBehaviour()
    {
        Destroy(gameObject);
    }

    protected void PlayerBounce(Collider2D collision)
    {
        Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
        playerRB.velocity = new Vector2(playerRB.velocity.x, bounceHeight);
    }

    protected void DisableObject(bool kinema, bool animation)
    {
        enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = kinema;
        if (anim != null) anim.enabled = animation;
        objectCollider.enabled = false;
        triggerCollider.enabled = false;
    }

}
