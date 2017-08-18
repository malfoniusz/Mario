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
    private AudioSource jumpAudio;
    private int direction = 0;
    private int groundMask;
    private bool jump = false;
    private float prevSpeed = 0;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
        groundMask = LayerMask.NameToLayer("Ground");
	}

    void Update()
    {
        direction = (int) Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate ()
    {
        Walk();
        Turn();
        Jump();
    }

    void Walk()
    {
        if (direction != 0)
        {
            Accelerate();
        }
        else if (direction == 0)
        {
            Deaccelerate();
        }

        WalkAnimation();
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

    void Turn()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        TurnAnimation();
    }

    void TurnAnimation()
    {
        bool slowing = Mathf.Abs(rb.velocity.x) < Mathf.Abs(prevSpeed);
        bool opposite = direction == -Mathf.Sign(rb.velocity.x);
        bool isTurning = (slowing && opposite) ? true : false;

        anim.SetBool("IsTurning", isTurning);
        prevSpeed = rb.velocity.x;
    }

    void Jump()
    {
        bool grounded = CheckGround();
        if (Input.GetKey("up") && grounded == true)
        {
            float jumpForce = jumpSpeed * Time.deltaTime;
            rb.AddForce(new Vector2(0, jumpForce));
            jump = true;
            grounded = false;
            jumpAudio.Play();
        }
        if (grounded)
        {
            jump = false;
        }

        JumpAnimation();
    }

    bool CheckGround()
    {
        bool grounded = false;
        for (int i = 0; i < groundChecks.Length; i++)
        {
            grounded = Physics2D.Linecast(transform.position, groundChecks[i].position, 1 << groundMask);
            if (grounded == true)
            {
                break;
            }
        }

        return grounded;
    }

    void WalkAnimation()
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

    void JumpAnimation()
    {
        anim.SetBool("IsJumping", jump);
    }

    bool Right()
    {
        bool right = (transform.localScale.x == 1) ? true : false;
        return right;
    }
}
