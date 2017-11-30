using UnityEngine; 

public class WallBounce
{
    private const float TIME_DELAY = 0.1f;
    private float time = 0;
    private const float SPEED_ERROR = 5f;

    public bool Bounce(Rigidbody2D rb, float maxSpeed)
    {
        time += Time.deltaTime;
        float curSpeed = Mathf.Abs(rb.velocity.x);
        float absMaxSpeed = Mathf.Abs(maxSpeed);
        float minimalSpeed = absMaxSpeed - SPEED_ERROR;

        if (curSpeed < minimalSpeed && time > TIME_DELAY)
        {
            time = 0;
            return true;
        }

        return false;
    }

}
