using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float deacceleration;
    public float maxSpeed;
    public float changeDirectionMultiplier;
    public float jumpSpeed;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private int direction = 0;
    private int groundMask;
    private bool jump = false;
    private bool grounded = false;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.NameToLayer("Ground");
	}

    void Update()
    {
        direction = (int) Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate ()
    {
        HorizontalMovement();
        Jump();
    }

    void HorizontalMovement()
    {
        if (direction != 0)
        {
            Accelerate();
        }
        else if (direction == 0)
        {
            Deaccelerate();
        }
    }

    void Accelerate()
    {
        float moveForce = acceleration * direction * Time.deltaTime;
        if (Mathf.Sign(moveForce) != Mathf.Sign(rb.velocity.x))
        {
            moveForce *= changeDirectionMultiplier;
        }

        rb.AddForce(new Vector2(moveForce, 0));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
    }

    void Deaccelerate()
    {
        float speed = rb.velocity.x;
        speed = Mathf.MoveTowards(speed, 0, deacceleration * Time.deltaTime);
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Jump()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << groundMask);

        if (Input.GetKey("up") && grounded == true)
        {
            float jumpForce = jumpSpeed * Time.deltaTime;
            rb.AddForce(new Vector2(0, jumpForce));
            grounded = false;
        }
    }
}
