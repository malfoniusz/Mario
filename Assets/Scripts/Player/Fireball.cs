using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform[] groundChecks;
    public Transform[] sideChecks;
    public float speed = 250f;
    public float bounceForce = 250f;

    private GameObject player;
    private Rigidbody2D rb;
    private Camera mainCam;
    private float direction;
    private const float FALL_SPEED_SWITCH = 400f;
    private const float STAY_DELAY = 0.1f;
    private float time = 0;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fireball"));
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCam = Camera.main;
        direction = Mathf.Sign(player.transform.localScale.x);
        rb.velocity = Vector2.right * direction * speed;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (Mathf.Abs(rb.velocity.y) >= FALL_SPEED_SWITCH) time = 0;

        OutOfViewport();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
        if (Contact.CheckContactGround(transform.position, sideChecks) && time > STAY_DELAY)
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

}
