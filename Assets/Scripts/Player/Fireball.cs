using UnityEngine;

public class Fireball : MonoBehaviour
{
    public static int numberOfFireballs = 0;
    public const int MAX_FIREBALLS = 2;

    public Transform[] groundChecks;
    public Transform[] sideChecks;
    public float speed = 250f;
    public float bounceForce = 250f;

    private GameObject player;
    private Rigidbody2D rb;
    private Camera mainCam;
    private float direction;
    private bool isColliding = false;   // Prevent triggering collider twice on the same frame

    private void Awake()
    {
        player = TagNames.GetPlayer();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        numberOfFireballs++;
        mainCam = Camera.main;
        direction = Mathf.Sign(player.transform.localScale.x);
        rb.velocity = Vector2.right * direction * speed;
    }

    private void Update()
    {
        isColliding = false;

        OutOfViewport();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;

        if (collision.gameObject.tag == TagNames.enemy)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.HitByFireball(direction);
            Destroy(gameObject);
        }
        else if (Contact.CheckContactGround(transform.position, groundChecks))
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        bool sideContact = Contact.CheckContactGround(transform.position, sideChecks);
        if (sideContact)
        {
            Destroy(gameObject);
        }
    }

    private void OutOfViewport()
    {
        Vector3 viewPos = mainCam.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        numberOfFireballs--;
    }

}
