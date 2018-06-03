using UnityEngine;

public class BlockAnimated : Block
{
    private bool moveBlockUp = false;
    private bool moveBlockDown = false;

    private MoveObject moveObject;
    private Vector2 moveDistance = new Vector2(0f, 8f);
    private const float MOVE_SPEED = 50f;

    protected override void Awake()
    {
        base.Awake();

        Vector2 startPos = transform.position;
        moveObject = MoveObject.CreateMoveObject3(startPos, moveDistance, MOVE_SPEED);
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
                moveObject = MoveObject.CreateMoveObject3(endPos, -moveDistance, MOVE_SPEED);
            }
        }
        else if (moveBlockDown)
        {
            transform.position = moveObject.NextPosition();

            if (moveObject.ReachedEnd())
            {
                moveBlockDown = false;

                Vector2 startPos = transform.position;
                moveObject = MoveObject.CreateMoveObject3(startPos, moveDistance, MOVE_SPEED);
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
