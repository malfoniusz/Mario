using UnityEngine;

public class MoveObject
{
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 curPos;

    private float curLerpTime = 0f;
    private const float MAX_LERP_TIME = 1f;
    private float moveTimeInSeconds;
    private float moveSpeed; // Move X piksels in 1 second
    private float distance;

    private MoveObject() {}

    private MoveObject(Vector2 startPos, Vector2 curPos, Vector2 endPos, float moveTimeInSeconds, float moveSpeed)
    {
        this.startPos = startPos;
        this.curPos = curPos;
        this.endPos = endPos;
        this.moveTimeInSeconds = moveTimeInSeconds;
        this.moveSpeed = moveSpeed;
        this.distance = Vector2.Distance(startPos, endPos);
    }

    public static MoveObject CreateMoveObject1(Vector2 startPos, Vector2 moveDistance, float moveTimeInSeconds)
    {
        Vector2 endPos = startPos + moveDistance;
        return new MoveObject(startPos, startPos, endPos, moveTimeInSeconds, 0);
    }

    public static MoveObject CreateMoveObject2(Vector2 startPos, Vector2 endPos, float moveTimeInSeconds)
    {
        return new MoveObject(startPos, startPos, endPos, moveTimeInSeconds, 0);
    }

    public static MoveObject CreateMoveObject3(Vector2 startPos, Vector2 endPos, float moveSpeed)
    {
        return new MoveObject(startPos, startPos, endPos, 0, moveSpeed);
    }

    public Vector2 NextPosition()
    {
        CalcNextPosition();
        return curPos;
    }

    private void CalcNextPosition()
    {
        IncLerp();

        float percentage = curLerpTime / MAX_LERP_TIME;
        Vector2 newPos = Vector2.Lerp(startPos, endPos, percentage);

        this.curPos = newPos;
    }

    private void IncLerp()
    {
        if (moveTimeInSeconds != 0)
        {
            curLerpTime += (Time.deltaTime / moveTimeInSeconds);
        }
        else if (moveSpeed != 0)
        {
            float moveTime = distance / moveSpeed;
            curLerpTime += (Time.deltaTime / moveTime);
        }
        else
        {
            throw new System.Exception("moveTime i moveSpeed są równe 0.");
        }

        if (curLerpTime > MAX_LERP_TIME) curLerpTime = MAX_LERP_TIME;
    }

    public bool ReachedEnd()
    {
        if (curPos == endPos) return true;

        return false;
    }

}
