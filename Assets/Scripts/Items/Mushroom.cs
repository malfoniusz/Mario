using UnityEngine;

public class Mushroom : Moving
{
    protected PlayerPowerup playerPowerup;
    protected bool appearAnim = true;
    protected bool stayKinematic = false;

    private MoveObject moveObject;
    private Vector2 moveDistance = new Vector2(0f, 16.1f);  // 0.1f aby grzyb po wyjsciu nie zahaczyl o podloze kolejnego bloku
    private const float MOVE_SPEED_MULTIPLIER = 1f;

    protected override void Awake()
    {
        base.Awake();
        this.playerPowerup = player.GetComponent<PlayerPowerup>();

        Vector2 startPos = transform.position;
        this.moveObject = new MoveObject(startPos, moveDistance, MOVE_SPEED_MULTIPLIER);
    }

    private void Start()
    {
        direction = 1;
        Physics2D.IgnoreCollision(objectCollider, player.GetComponent<BoxCollider2D>());
    }

    protected override void MovingBehaviour()
    {
        if (appearAnim) PowerupAppear();
        else            base.MovingBehaviour();
    }

    private void PowerupAppear()
    {
        transform.position = moveObject.NextPosition();

        if (moveObject.ReachedEnd())
        {
            appearAnim = false;
            rb.isKinematic = stayKinematic;
            UpdateVelocity();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagNames.player)
        {
            CollisionEnter(collision);
        }
    }

    protected override void CollisionEnter(Collider2D collision)
    {
        if (appearAnim == false)
        {
            CollisionBehaviour();
        }
    }

    protected virtual void CollisionBehaviour()
    {
        playerPowerup.MushroomPowerup();
        SpawnPoints();
        Destroy(gameObject);
    }

}
