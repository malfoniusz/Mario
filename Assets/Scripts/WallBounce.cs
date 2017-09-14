using UnityEngine;

public class WallBounce
{
    private const float MINIMAL_VELOCITY = 1f;
    private const float DIRECTION_DELAY = 0.1f;
    private float directionTime = 0;

    public bool Bounce(Rigidbody2D rb)
    {
        directionTime += Time.deltaTime;

        if (Mathf.Abs(rb.velocity.x) < MINIMAL_VELOCITY && directionTime > DIRECTION_DELAY)
        {
            directionTime = 0;
            return true;
        }

        return false;
    }

}
