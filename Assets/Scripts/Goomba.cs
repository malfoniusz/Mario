using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float speed = 30;
    public float minimalVelocity = 1f;
    public float changeDirectionDelay = 0.1f;

    private Rigidbody2D rb;
    private Vector2 direction = Vector2.left;
    private float time = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        UpdateVelocity();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Mathf.Abs(rb.velocity.x) < minimalVelocity && time > changeDirectionDelay)
        {
            time = 0;
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        direction *= -1;
        UpdateVelocity();
    }

    void UpdateVelocity()
    {
        rb.velocity = direction * speed;
    }

}
