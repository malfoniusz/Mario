using UnityEngine;

public class SolidBlock : BlockAnimated
{
    private Animator animBlock;
    private bool hasAwaken = true;

    protected override void Update()
    {
        PlayerHit();
        Sound();

        if (hasAwaken)
        {
            hasAwaken = false;
            PlayAnimation();
        }
    }

}
