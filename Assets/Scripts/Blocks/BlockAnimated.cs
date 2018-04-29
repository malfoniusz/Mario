using UnityEngine;

public class BlockAnimated : Block
{
    private bool moveBlockUp = false;
    private bool moveBlockDown = false;

    private MoveObject moveObject;
    private Vector2 moveDistance = new Vector2(0f, 8f);
    private const float MOVE_TIME_IN_SECONDS = 5f;

    protected override void Awake()
    {
        base.Awake();

        Vector2 startPos = transform.position;
        this.moveObject = new MoveObject(startPos, moveDistance, MOVE_TIME_IN_SECONDS);
    }

    protected override void Update()
    {
        base.Update();
        if (playerHit && !moveBlockUp && !moveBlockDown) moveBlockUp = true;
    }

    private void FixedUpdate()
    {
        BlockHit();
    }

    private void BlockHit()
    {
        if (moveBlockUp)
        {
            transform.position = moveObject.NextPosition();

            if (moveObject.ReachedEnd())
            {
                moveBlockUp = false;
                moveBlockDown = true;

                Vector2 endPos = transform.position;
                moveObject = new MoveObject(endPos, -moveDistance, MOVE_TIME_IN_SECONDS);
            }
        }
        else if (moveBlockDown)
        {
            transform.position = moveObject.NextPosition();

            if (moveObject.ReachedEnd())
            {
                moveBlockDown = false;

                Vector2 startPos = transform.position;
                moveObject = new MoveObject(startPos, moveDistance, MOVE_TIME_IN_SECONDS);
            }
        }
    }

    protected void SetMoveBlockUp(bool moveBlockUp)
    {
        this.moveBlockUp = moveBlockUp;
    }

    protected void SetMoveBlockDown(bool moveBlockDown)
    {
        this.moveBlockDown = moveBlockDown;
    }

}
