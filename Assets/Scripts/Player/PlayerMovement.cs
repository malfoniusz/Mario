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
    [HideInInspector] public int jumpableMask;

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
        jumpableMask = LayerMask.NameToLayer("Jumpable");
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
            walkKey = (int)Input.GetAxisRaw("Horizontal");
            jumpKey = Input.GetButton("Jump");
            jumpKeyDown = (jumpKeyDown || Input.GetButtonDown("Jump"));
            runKey = Input.GetButton("Run");
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

        anim.SetBool("IsTurning", isTurning);
        prevSpeed = rb.velocity.x;
    }

    void Jump()
    {
        CheckJump();
        MakeAJump();
        anim.SetBool("IsJumping", jumping);
    }

    void CheckJump()
    {
        bool grounded = CheckContact(groundChecks, jumpableMask);
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
        bool topContact = CheckContact(topChecks, jumpableMask);
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

    public bool CheckContact(Transform[] contactChecks, int layerMask)
    {
        bool contact = false;
        for (int i = 0; i < contactChecks.Length; i++)
        {
            contact = Physics2D.Linecast(transform.position, contactChecks[i].position, 1 << layerMask);
            if (contact == true)
            {
                break;
            }
        }

        return contact;
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

    public void UpdateModel(BoxCollider2D bc, Transform[] gc, Transform[] tc)
    {
        boxCollider.size = bc.size;

        for (int i = 0; i < groundChecks.Length; i++)
        {
            groundChecks[i].localPosition = gc[i].localPosition;
        }

        for (int i = 0; i < topChecks.Length; i++)
        {
            topChecks[i].localPosition = tc[i].localPosition;
        }
    }

    public void Stop(bool value)
    {
        stop = value;
        rb.isKinematic = value;
    }
}
