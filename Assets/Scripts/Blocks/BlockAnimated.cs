using UnityEngine;

public class BlockAnimated : Block
{
    private bool moveBlockUp = false;
    private bool moveBlockDown = false;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 MOVE_DISTANCE = new Vector2(0f, 8f);

    private float curLerpTime = 0f;
    private const float MAX_LERP_TIME = 1f;
    private const float MOVE_SPEED_MULTIPLIER = 5f;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        endPos = startPos + MOVE_DISTANCE;
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
            transform.position = CalcNewPosition(startPos, endPos);

            if (transform.position.Equals(endPos))
            {
                curLerpTime = 0f;
                moveBlockUp = false;
                moveBlockDown = true;
            }
        }
        else if (moveBlockDown)
        {
            transform.position = CalcNewPosition(endPos, startPos);

            if (transform.position.Equals(startPos))
            {
                curLerpTime = 0f;
                moveBlockDown = false;
            }
        }
    }

    private Vector2 CalcNewPosition(Vector2 pos1, Vector2 pos2)
    {
        IncLerp();

        float percentage = curLerpTime / MAX_LERP_TIME;
        Vector2 newPos = Vector2.Lerp(pos1, pos2, percentage);

        return newPos;
    }

    private void IncLerp()
    {
        curLerpTime += (Time.deltaTime * MOVE_SPEED_MULTIPLIER);
        if (curLerpTime > MAX_LERP_TIME) curLerpTime = MAX_LERP_TIME;
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
