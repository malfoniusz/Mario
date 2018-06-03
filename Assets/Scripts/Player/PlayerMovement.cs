﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource jumpAudio;
    public float walkSpeed;
    public float slowdownSpeed;
    public float maxWalkSpeed;
    public float runSpeed;
    public float maxRunSpeed;
    public float changeDirectionMultiplier;
    public float jumpSpeed;
    public float jumpSlowdown;
    public Transform[] groundChecks;
    public Transform[] topChecks;
    public float minimalVelocity;

    private GameObject parent;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private int walkKey = 0;
    private bool jumpKeyHeld = false;
    private bool jumpKeyPressed = false;
    private bool runKey = false;
    private bool playerIsJumping = false;
    private float jumpForce = 0;
    private float prevSpeed = 0;
    private StopMovement stopMovement;
    private bool stop = false;
    private int[] jumpableLayers;

    private float maxOffsetX;

    private bool constantMove = false;
    private int constantDirection;
    private float constantSpeed;

    void Awake()
    {
        parent = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = parent.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        stopMovement = new StopMovement();
        jumpableLayers = new int[] { LayerNames.GetGround(), LayerNames.GetPipe() };
    }

    void Start()
    {
        maxOffsetX = MaxOffsetX();
    }

    void Update()
    {
        if (!stop)
        {
            walkKey = (int) ButtonNames.GetRawHorizontal();
            jumpKeyHeld = ButtonNames.JumpHeld();
            jumpKeyPressed = (jumpKeyPressed || ButtonNames.JumpPressed());
            runKey = ButtonNames.RunHeld();
        }
    }

    void FixedUpdate ()
    {
        stopMovement.StopAndRestore(rb, stop);

        if (!stop)
        {
            Walk();
            Turn();
            Jump();

            LeftCameraBoundary();
        }
    }

    void LateUpdate()
    {
        if (constantMove)
        {
            rb.velocity = new Vector2(constantDirection * constantSpeed, rb.velocity.y);
        }
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

        anim.SetBool(AnimatorPlayerNames.isTurning, isTurning);
        prevSpeed = rb.velocity.x;
    }

    void Jump()
    {
        CheckJump();
        MakeAJump();
        anim.SetBool(AnimatorPlayerNames.isJumping, playerIsJumping);
    }

    void CheckJump()
    {
        bool grounded = Contact.ContactPoints(groundChecks, jumpableLayers);
        if (jumpKeyPressed && jumpKeyHeld && grounded)  // Skok gdy: jumpKey+ground || jumpKeyPressedAndHeldWhileInAir
        {
            jumpKeyPressed = false;
            playerIsJumping = true;
            grounded = false;

            jumpForce = jumpSpeed * Time.deltaTime;
            jumpAudio.Play();
        }
        else if (playerIsJumping && grounded)
        {
            playerIsJumping = false;
        }
    }

    void MakeAJump()
    {
        bool topContact = Contact.ContactPoints(topChecks, jumpableLayers);
        if (!jumpKeyHeld || topContact)
        {
            jumpForce = 0;
        }
        else if (jumpForce != 0 && jumpKeyHeld)
        {
            jumpForce = Mathf.Lerp(jumpForce, 0, jumpSlowdown);
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void WalkAnimation()
    {
        bool isWalking = Mathf.Abs(rb.velocity.x) > minimalVelocity ? true : false;
        anim.SetBool(AnimatorPlayerNames.isWalking, isWalking);

        WalkAnimationSpeed();
    }

    void WalkAnimationSpeed()
    {
        float curSpeed = Mathf.Abs(rb.velocity.x);
        float walkMultiplier = 1;
        walkMultiplier += (Mathf.InverseLerp(0, maxRunSpeed, curSpeed) * 2);

        anim.SetFloat(AnimatorPlayerNames.walkSpeedMultiplier, walkMultiplier);
    }

    void LeftCameraBoundary()
    {
        float maxLeft = TagNames.GetCamera().transform.position.x - maxOffsetX;
        float new_x = Mathf.Clamp(transform.position.x, maxLeft, Mathf.Infinity);

        if (transform.position.x != new_x)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.position = new Vector3(new_x, transform.position.y, transform.position.z);
        }
    }

    float MaxOffsetX()
    {
        float playerWidth = boxCollider.size.x;

        Camera cam = TagNames.GetCamera();
        float height = cam.orthographicSize * 2;
        float camWidth = cam.aspect * height;

        float maxOffsetX = camWidth / 2 - playerWidth / 2;
        return maxOffsetX;
    }

    public void DisablePlayer(bool value, bool resetVelocity)
    {
        DisableInput(value);
        Stop(value);
        IsKinematic(value);
        if (resetVelocity) ResetVelocity();
    }

    public void SetConstantMove(bool activate, Direction direction, float speed)
    {
        constantMove = activate;

        if (constantMove)
        {
            if (direction == Direction.Right) constantDirection = 1;
            else if (direction == Direction.Left) constantDirection = -1;
            else throw new System.Exception("You can't move in this direction.");

            constantSpeed = speed;
        }
    }

    public void IsKinematic(bool value)
    {
        rb.isKinematic = value;
    }

    public void DisableInput(bool value)
    {
        ButtonNames.disableInput = value;
    }

    public void Stop(bool value)
    {
        stop = value;
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
    }

}
