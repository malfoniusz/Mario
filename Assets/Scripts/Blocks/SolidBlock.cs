using UnityEngine;

public class SolidBlock : BlockAnimated
{
    public bool hitOnAwake = true;

    private void Start()
    {
        if (hitOnAwake) PlayAnimation();
    }

    protected override void Update()
    {
        playerHit = PlayerHit();
        Sound(playerHit);
    }

}
