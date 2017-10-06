﻿using UnityEngine;

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
            ChangeDirection();
        }
    }

    protected override void CollisionEnter(Collider2D collision)
    {
        audioKick.Play();

        if (Mathf.Abs(rb.velocity.x) < MINIMAL_VELOCITY)
        {
            int direction = (int) Mathf.Sign(transform.position.x - player.transform.position.x);
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

            moving = true;
            base.direction = direction;
            time = 0;
        }
        else
        {
            base.CollisionEnter(collision);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && rb.velocity.x > MINIMAL_VELOCITY && !stopMultipleTriggers)
        {
            stopMultipleTriggers = true;

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            float hitDirection = Mathf.Sign(collision.gameObject.transform.position.y - transform.position.y);
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
