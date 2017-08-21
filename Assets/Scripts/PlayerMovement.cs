using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float slowdownSpeed;
    public float maxWalkSpeed;
    public float runSpeed;
    public float maxRunSpeed;
    public float changeDirectionMultiplier;
    public float jumpSpeed;
    public Transform[] groundChecks;
    public float minimalVelocity;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource jumpAudio;
    private int walkKey = 0;
    private bool jumpKey = false;
    private bool runKey = false;
    private int groundMask;
    private bool jump = false;
    private float prevSpeed = 0;

    private float maxOffsetX;

    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
        groundMask = LayerMask.NameToLayer("Floor");

        maxOffsetX = MaxOffsetX();
    }

    void Update()
    {
        walkKey = (int) Input.GetAxisRaw("Horizontal");
        jumpKey = Input.GetButton("Jump");
        runKey = Input.GetButton("Run");
    }

    void FixedUpdate ()
    {
        Walk();
        Turn();
        Jump();

        LeftCameraBoundary();
    }

    void Walk()
    {
        if (walkKey != 0)
        {
            Accelerate();
        }
        else if (walkKey == 0)
        {
            Deaccelerate();
        }

        WalkAnimation();
    }

    void Accelerate()
    {
        float moveForce = MoveForce();
        rb.AddForce(new Vector2(moveForce, 0));

        SpeedLimit();
    }

    float MoveForce()
    {
        float moveForce = walkKey * walkSpeed * Time.deltaTime;
        if (Mathf.Sign(moveForce) != Mathf.Sign(rb.velocity.x))
        {
            moveForce *= changeDirectionMultiplier;
        }
        else if (runKey)
        {
            moveForce = walkKey * runSpeed * Time.deltaTime;
        }

        return moveForce;
    }

    void SpeedLimit()
    {
        if (runKey)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxRunSpeed, maxRunSpeed), rb.velocity.y);
        }
        else if (Mathf.Abs(rb.velocity.x) > maxWalkSpeed)
        {
            Deaccelerate();
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxWalkSpeed, maxWalkSpeed), rb.velocity.y);
        }
    }

    void Deaccelerate()
    {
        float speed = rb.velocity.x;
        speed = Mathf.MoveTowards(speed, 0, slowdownSpeed * Time.deltaTime);
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Turn()
    {
        if (rb.velocity.x > minimalVelocity)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < -minimalVelocity)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        TurnAnimation();
    }

    void TurnAnimation()
    {
        bool slowing = Mathf.Abs(rb.velocity.x) < Mathf.Abs(prevSpeed);
        bool opposite = (walkKey == -Mathf.Sign(rb.velocity.x));
        bool isTurning = (slowing && opposite) ? true : false;

        anim.SetBool("IsTurning", isTurning);
        prevSpeed = rb.velocity.x;
    }

    void Jump()
    {
        bool grounded = CheckGround();
        if (jumpKey && grounded == true)
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
        bool isWalking = Mathf.Abs(rb.velocity.x) > minimalVelocity ? true : false;
        anim.SetBool("IsWalking", isWalking);

        AnimationSpeed();
    }

    void AnimationSpeed()
    {
        float curSpeed = Mathf.Abs(rb.velocity.x);
        float animSpeed = 1;
        animSpeed += Mathf.InverseLerp(0, maxWalkSpeed, curSpeed);
        animSpeed += Mathf.InverseLerp(maxWalkSpeed, maxRunSpeed, curSpeed);
        anim.speed = animSpeed;
    }

    void JumpAnimation()
    {
        anim.SetBool("IsJumping", jump);
    }

    void LeftCameraBoundary()
    {
        float maxLeft = Camera.main.transform.position.x - maxOffsetX;
        float new_x = Mathf.Clamp(transform.position.x, maxLeft, Mathf.Infinity);

        if (transform.position.x != new_x)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.position = new Vector3(new_x, transform.position.y, transform.position.z);
        }
    }

    float MaxOffsetX()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        float playerWidth = collider.size.x;

        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2;
        float camWidth = cam.aspect * height;

        float maxOffsetX = camWidth / 2 - playerWidth / 2;
        return maxOffsetX;
    }

}
