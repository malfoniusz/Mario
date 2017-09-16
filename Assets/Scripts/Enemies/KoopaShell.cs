using UnityEngine;

public class KoopaShell : Enemy
{
    public AudioSource audioKick;
    public float overrideSpeed = 120;

    private Transform player;
    private const float MINIMAL_VELOCITY = 1f;
    private bool moving = false;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = overrideSpeed;
    }

    protected override void MovingBehaviour()
    {
        if (moving)
        {
            ChangeDirection();
        }
    }

    protected override void EnemyCollision(Collider2D collision)
    {
        audioKick.Play();

        if (Mathf.Abs(rb.velocity.x) < MINIMAL_VELOCITY)
        {
            int direction = (int) Mathf.Sign(transform.position.x - player.position.x);
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

            moving = true;
            base.direction = direction;
            time = 0;
        }
        else
        {
            base.EnemyCollision(collision);
        }
    }

    protected override void EnemyStomped(Collider2D collision)
    {
        time = 0;
        PlayerBounce(collision);
        EnemyStompedBehaviour();
    }

    protected override void EnemyStompedBehaviour()
    {
        moving = false;
        rb.velocity = Vector2.zero;
    }

    protected override void EnemyKillingPlayer()
    {
        audioKick.Stop();
        playerDeath.Die();
    }

}
