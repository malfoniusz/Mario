using UnityEngine;

public class KoopaShell : Enemy
{
    public AudioSource audioKick;

    private const float MINIMAL_VELOCITY = 1f;
    private bool moving = false;
    private bool stopMultipleTriggers = false;
    private float killDelay = 0;
    private const float NEXT_ENEMY_KILL_DELAY = 0.00001f;

    protected override void Update()
    {
        base.Update();
        stopMultipleTriggers = false;
        killDelay += Time.deltaTime;
    }

    protected override void MovingBehaviour()
    {
        if (moving)
        {
            base.MovingBehaviour();
        }
    }

    protected override void ChangeDirection()
    {
        int[] ignoredLayers = { LayerNames.GetPlayer(), LayerNames.GetEnemy() };

        bool leftContact = Contact.ContactPointsIgnore(leftChecks, ignoredLayers);
        bool rightContact = Contact.ContactPointsIgnore(rightChecks, ignoredLayers);

        if (leftContact || rightContact) direction *= -1;
    }

    protected override void CollisionEnter(Collider2D collision)
    {
        audioKick.Play();

        bool notMoving = (Mathf.Abs(rb.velocity.x) < MINIMAL_VELOCITY);
        if (notMoving)
        {
            int direction = (int) Mathf.Sign(transform.position.x - player.transform.position.x);
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

            moving = true;
            base.direction = direction;
            time = 0;
        }
        else
        {
            base.CollisionEnter(collision); // Shell stomped
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        EnemyHitByShell(collision);
    }

    private void EnemyHitByShell(Collider2D collision)
    {
        bool shellHitsEnemy = (collision.gameObject.layer == LayerNames.GetEnemy() && rb.velocity.x > MINIMAL_VELOCITY);
        if (shellHitsEnemy && !stopMultipleTriggers)
        {
            stopMultipleTriggers = true;

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            Transform playerT = collision.gameObject.transform;
            float hitDirection = Direction.HitDirection(playerT, transform);
            enemy.HitByFireball(hitDirection);
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

    protected override void EnemyHittingPlayer()
    {
        audioKick.Stop();
        playerPowerup.PlayerHit();
    }

}
