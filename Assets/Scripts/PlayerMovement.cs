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
    public Transform[] groundChecks;

    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 0;
    private int groundMask;
    private bool grounded = false;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        WalkingAnimation();
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

    void WalkingAnimation()
    {
        bool isWalking = rb.velocity.x != 0 ? true : false;
        anim.SetBool("IsWalking", isWalking);

        AnimationSpeed();
    }

    void AnimationSpeed()
    {
        bool isRight = rb.velocity.x >= 0 ? true : false;
        float maxSpeedDir = isRight ? maxSpeed : -maxSpeed;
        float animSpeed = 1 + Mathf.InverseLerp(0, maxSpeedDir, rb.velocity.x);
        anim.speed = animSpeed;
    }

    void Deaccelerate()
    {
        float speed = rb.velocity.x;
        speed = Mathf.MoveTowards(speed, 0, deacceleration * Time.deltaTime);
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Jump()
    {
        for (int i = 0; i < groundChecks.Length; i++)
        {
            grounded = Physics2D.Linecast(transform.position, groundChecks[i].position, 1 << groundMask);
            if (grounded == true)
            {
                break;
            }
        }

        if (Input.GetKey("up") && grounded == true)
        {
            float jumpForce = jumpSpeed * Time.deltaTime;
            rb.AddForce(new Vector2(0, jumpForce));
            grounded = false;
        }
    }
}
