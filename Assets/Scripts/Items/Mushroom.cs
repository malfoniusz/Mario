using UnityEngine;

public class Mushroom : Moving
{
    private PlayerPowerup playerPowerup;
    private bool activated = false;

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

    protected override void CollisionEnter(Collider2D collision)
    {
        if (activated)
        {
            playerPowerup.Powerup();
            PointsSpawn();
            Destroy(parent);
        }
    }

}
