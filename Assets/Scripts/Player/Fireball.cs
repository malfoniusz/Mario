using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform[] groundChecks;
    public float speed = 250f;
    public float bounceForce = 250f;

    private GameObject player;
    private Rigidbody2D rb;
    private float direction;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fireball"));
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        direction = Mathf.Sign(player.transform.localScale.x);
        rb.velocity = Vector2.right * direction * speed;
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
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

}
