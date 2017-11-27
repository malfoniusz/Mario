using UnityEngine;

public class Mushroom : Moving
{
    protected PlayerPowerup playerPowerup;
    protected bool activated = false;

    protected override void Awake()
    {
        base.Awake();
        playerPowerup = player.GetComponent<PlayerPowerup>();
    }

    protected override void Start()
    {
        direction = 1;
        Physics2D.IgnoreCollision(objectCollider, player.GetComponent<BoxCollider2D>());
    }

    protected override void MovingBehaviour()
    {
        if (activated)
        {
            ChangeDirection();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            activated = true;
            rb.isKinematic = false;
            UpdateVelocity();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollisionEnter(collision);
        }
    }

    protected override void CollisionEnter(Collider2D collision)
    {
        if (activated)
        {
            playerPowerup.MushroomPowerup();
            PointsSpawn(false);
            Destroy(parent);
        }
    }

}
