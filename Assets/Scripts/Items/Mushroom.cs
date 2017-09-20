using UnityEngine;

public class Mushroom : Moving
{
    private bool activated = false;

    protected override void Start()
    {
        direction = 1;
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
            PointsSpawn();
            Destroy(parent);
        }
    }

}
