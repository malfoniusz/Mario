using UnityEngine;

public class MoveObject
{
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 curPos;

    private float curLerpTime = 0f;
    private const float MAX_LERP_TIME = 1f;
    private float moveSpeedMultiplier;

    public MoveObject(Vector2 startPos, Vector2 moveDistance, float moveSpeedMultiplier)
    {
        Vector2 endPos = startPos + moveDistance;
        SetMoveObject(startPos, endPos, moveSpeedMultiplier);
    }

    public MoveObject(float moveSpeedMultiplier, Vector2 startPos, Vector2 endPos)
    {
        SetMoveObject(startPos, endPos, moveSpeedMultiplier);
    }

    private void SetMoveObject(Vector2 startPos, Vector2 endPos, float moveSpeedMultiplier)
    {
        this.startPos = startPos;
        this.curPos = startPos;
        this.endPos = endPos;
        this.moveSpeedMultiplier = moveSpeedMultiplier;
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
        curLerpTime += (Time.deltaTime * moveSpeedMultiplier);
        if (curLerpTime > MAX_LERP_TIME) curLerpTime = MAX_LERP_TIME;
    }

    public bool ReachedEnd()
    {
        if (curPos == endPos) return true;

        return false;
    }

}
