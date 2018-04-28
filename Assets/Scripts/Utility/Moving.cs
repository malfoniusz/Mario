using UnityEngine;

public class Moving : MonoBehaviour
{
    static public bool stop = false;

    public BoxCollider2D objectCollider;
    public BoxCollider2D triggerCollider;
    public Animator anim;
    public Transform[] leftChecks;
    public Transform[] rightChecks;
    public int points = 100;
    public int direction = 1;
    public float speed = 30;

    protected GameObject player;
    protected Rigidbody2D rb;

    private StopMovement stopMovement;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = TagNames.GetPlayer();
        stopMovement = new StopMovement();
    }

    private void OnEnable()
    {
        Physics2D.IgnoreCollision(objectCollider, player.GetComponent<BoxCollider2D>());
    }

    private void FixedUpdate()
    {
        stopMovement.StopAndRestore(rb, stop, anim);

        if (!stop)
        {
            MovingBehaviour();
        }
    }

    protected virtual void MovingBehaviour()
    {
        ChangeDirection();
        UpdateVelocity();
    }

    protected virtual void ChangeDirection()
    {
        bool leftContact = Contact.ContactPointsIgnore(leftChecks, LayerNames.GetPlayer());
        bool rightContact = Contact.ContactPointsIgnore(rightChecks, LayerNames.GetPlayer());

        if (leftContact)        direction = 1;
        else if (rightContact)  direction = -1;
    }

    protected void UpdateVelocity()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagNames.player)
        {
            CollisionEnter(collision);
        }
    }

    protected virtual void CollisionEnter(Collider2D collision)
    {
        SpawnComboPoints();
        Destroy(gameObject);
    }

    protected void SpawnPoints()
    {
        SpawnPointsFloating.Points(transform.position, points);
    }

    protected void SpawnComboPoints()
    {
        int comboPoints = ComboPoints.Combo(points);
        SpawnPointsFloating.Points(transform.position, comboPoints);
    }

    protected void SpawnExtraLife()
    {
        SpawnPointsFloating.ExtraLife(transform.position);
    }

    protected void SpawnPointsAndExtraLife()
    {
        SpawnPointsFloating.PointsAndExtraLife(transform.position, points);
    }

}
