using UnityEngine;

public class SolidBlock : BlockAnimated
{
    private Animator animBlock;
    private bool hasAwaken = true;

    protected override void Update()
    {
        Sound();    // SoundDelay prevents sound from going off when awoken

        if (hasAwaken)
        {
            hasAwaken = false;
            PlayAnimation();
        }
    }

}
