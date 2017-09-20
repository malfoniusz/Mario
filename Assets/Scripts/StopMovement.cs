using UnityEngine;

public class StopMovement
{
    private Vector2 savedVelocity;

    private void Awake()
    {
        savedVelocity = Vector2.zero;
    }

    public bool StopAndRestore(Rigidbody2D rb, bool stop, Animator anim = null)
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

        return stop;
    }

}
