using UnityEngine;

public class BlockAnimated : Block
{
    protected Animator anim;
    protected bool animFinished = true;

    private float animDelay = 0.1f;
    private float animTime = 0;

    protected override void Awake()
    {
        base.Awake();
        anim = parent.GetComponent<Animator>();
    }

    protected override void Update()
    {
        Sound();
        Animation();
    }

    protected void Animation()
    {
        animFinished = anim.GetCurrentAnimatorStateInfo(0).IsName("Empty");
        animTime += Time.deltaTime;

        if (playerHit && animFinished && animTime > animDelay)
        {
            animTime = 0;
            PlayAnimation();
        }
    }

    protected void PlayAnimation()
    {
        animFinished = false;
        anim.SetTrigger("IsHit");
    }

}
