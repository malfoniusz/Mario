using UnityEngine;

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
    private bool jumpKey = false;
    private bool jumpKeyDown = false;
    private bool runKey = false;
    private bool jumping = false;
    private float jumpForce = 0;
    private float prevSpeed = 0;
    private StopMovement stopMovement;
    private bool stop = false;

    private float maxOffsetX;

    void Awake()
    {
        parent = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = parent.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        stopMovement = new StopMovement();
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
            jumpKey = ButtonNames.GetJump();
            jumpKeyDown = (jumpKeyDown || ButtonNames.GetJumpDown());
            runKey = ButtonNames.GetRun();
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

        anim.SetBool(AnimatorNames.playerIsTurning, isTurning);
        prevSpeed = rb.velocity.x;
    }

    void Jump()
    {
        CheckJump();
        MakeAJump();
        anim.SetBool(AnimatorNames.playerIsJumping, jumping);
    }

    void CheckJump()
    {
        bool grounded = Contact.CheckContactGround(transform.position, groundChecks);
        if (jumpKeyDown && grounded)
        {
            jumpKeyDown = false;
            jumping = true;
            grounded = false;

            jumpForce = jumpSpeed * Time.deltaTime;
            jumpAudio.Play();
        }
        else if (jumping && grounded)
        {
            jumping = false;
        }
    }

    void MakeAJump()
    {
        bool topContact = Contact.CheckContactGround(transform.position, topChecks);
        if (!jumpKey || topContact)
        {
            jumpForce = 0;
        }
        else if (jumpForce != 0 && jumpKey)
        {
            jumpForce = Mathf.Lerp(jumpForce, 0, jumpSlowdown);
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void WalkAnimation()
    {
        bool isWalking = Mathf.Abs(rb.velocity.x) > minimalVelocity ? true : false;
        anim.SetBool(AnimatorNames.playerIsWalking, isWalking);

        WalkAnimationSpeed();
    }

    void WalkAnimationSpeed()
    {
        float curSpeed = Mathf.Abs(rb.velocity.x);
        float walkMultiplier = 1;
        walkMultiplier += (Mathf.InverseLerp(0, maxRunSpeed, curSpeed) * 2);

        anim.SetFloat(AnimatorNames.playerWalkSpeedMultiplier, walkMultiplier);
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
        float playerWidth = boxCollider.size.x;

        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2;
        float camWidth = cam.aspect * height;

        float maxOffsetX = camWidth / 2 - playerWidth / 2;
        return maxOffsetX;
    }

    public void Stop(bool value)
    {
        stop = value;
        rb.isKinematic = value;
    }
}
