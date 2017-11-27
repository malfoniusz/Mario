﻿using UnityEngine;

public class Moving : MonoBehaviour
{
    static public bool stop = false;

    public GameObject parent;
    public BoxCollider2D objectCollider;
    public BoxCollider2D triggerCollider;
    public Animator anim;
    public GameObject pointsFloating;
    public int points = 100;
    public float speed = 30;

    protected GameObject player;
    protected Rigidbody2D rb;
    protected int direction = -1;

    private WallBounce wallBounce;
    private StopMovement stopMovement;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        wallBounce = new WallBounce();
        stopMovement = new StopMovement();
    }

    protected virtual void Start()
    {
        Physics2D.IgnoreCollision(objectCollider, player.GetComponent<BoxCollider2D>());
        UpdateVelocity();
    }

    private void FixedUpdate()
    {
        stopMovement.StopAndRestore(rb, stop, anim);

        if (!stop)
        {
            MovingBehaviour();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollisionEnter(collision);
        }
    }

    protected virtual void CollisionEnter(Collider2D collision)
    {
        PointsSpawn(true);
        Destroy(parent);
    }

    protected void PointsSpawn(bool combo)
    {
        int pointsValue = (combo == false ? points : ComboPoints.Combo(points));

        GameObject pointsObject = Instantiate(pointsFloating);
        pointsObject.transform.GetChild(0).position = transform.position;
        pointsObject.GetComponent<PointsFloating>().SetPoints(pointsValue, false);
    }

    protected virtual void MovingBehaviour()
    {
        ChangeDirection();
    }

    protected void ChangeDirection()
    {
        bool bounce = wallBounce.Bounce(rb, speed);
        if (bounce)
        {
            ChangeDirectionBehaviour();
        }
    }

    protected virtual void ChangeDirectionBehaviour()
    {
        direction *= -1;
        UpdateVelocity();
    }

    protected void UpdateVelocity()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

}
