using UnityEngine;

public class SolidBlock : BlockAnimated
{
    private bool hitOnAwake;

    private void Start()
    {
        SetMoveBlockUp(hitOnAwake);
    }

    protected override void Update()
    {
        playerHit = PlayerHit();
        Sound(playerHit);
    }

    public void SetHitOnAwake(bool hitOnAwake)
    {
        this.hitOnAwake = hitOnAwake;
    }

}
