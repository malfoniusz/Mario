using UnityEngine;

public class Moving : MonoBehaviour
{
    static public bool stop = false;

    public BoxCollider2D objectCollider;
    public BoxCollider2D triggerCollider;
    public Animator anim;
    public GameObject pointsFloating;
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

    protected virtual void Start()
    {
        Physics2D.IgnoreCollision(objectCollider, player.GetComponent<BoxCollider2D>());
        UpdateVelocity();
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
        bool leftContact = Contact.CheckContactPointIgnore(leftChecks, LayerNames.GetPlayer());
        bool rightContact = Contact.CheckContactPointIgnore(rightChecks, LayerNames.GetPlayer());

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
        GameObject pointsObject = CreatePointsFloating();
        pointsObject.GetComponent<PointsFloating>().SetPoints(points);
    }

    protected void SpawnComboPoints()
    {
        int comboPoints = ComboPoints.Combo(points);

        GameObject pointsObject = CreatePointsFloating();
        pointsObject.GetComponent<PointsFloating>().SetPoints(comboPoints);
    }

    protected void SpawnExtraLife()
    {
        GameObject pointsObject = CreatePointsFloating();
        pointsObject.GetComponent<PointsFloating>().SetExtraLife(true);
    }

    protected void SpawnPointsAndExtraLife()
    {
        GameObject pointsObject = CreatePointsFloating();
        pointsObject.GetComponent<PointsFloating>().SetPointsAndExtraLife(points, true);
    }

    private GameObject CreatePointsFloating()
    {
        GameObject pointsObject = Instantiate(pointsFloating);
        pointsObject.transform.GetChild(0).position = transform.position;
        return pointsObject;
    }

}
