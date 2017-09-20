using UnityEngine;

public class Moving : MonoBehaviour
{
    static public bool stop = false;

    public GameObject parent;
    public BoxCollider2D objectCollider;
    public BoxCollider2D triggerCollider;
    public Animator anim;
    public GameObject pointsFloating;
    public int points = 100;
    public float speed = 30;
    
    protected Rigidbody2D rb;
    protected int direction = -1;

    private Vector2 savedVelocity = Vector2.zero;
    private WallBounce wallBounce = new WallBounce();

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        UpdateVelocity();
    }

    private void FixedUpdate()
    {
        StopAndResume();

        if (!stop)
        {
            MovingBehaviour();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollisionEnter(collision);
        }
    }

    protected virtual void CollisionEnter(Collider2D collision)
    {
        PointsSpawn();
        Destroy(parent);
    }

    protected void PointsSpawn()
    {
        GameObject pointsObject = Instantiate(pointsFloating);
        pointsObject.transform.GetChild(0).position = transform.position;
        pointsObject.GetComponent<PointsFloating>().SetPoints(ComboPoints.Combo(points), false);
    }

    void StopAndResume()
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
    }

    protected virtual void MovingBehaviour()
    {
        ChangeDirection();
    }

    protected void ChangeDirection()
    {
        bool bounce = wallBounce.Bounce(rb, speed);
        if (bounce)
        {
            ChangeDirectionBehaviour();
        }
    }

    protected virtual void ChangeDirectionBehaviour()
    {
        direction *= -1;
        UpdateVelocity();
    }

    protected void UpdateVelocity()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

}
