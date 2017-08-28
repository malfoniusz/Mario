﻿using UnityEngine;

public class BlockAnimated : Block
{
    protected Animator anim;
    protected bool animFinished = true;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        Sound();
        Animation();
    }

    protected void Animation()
    {
        if (playerHit && animFinished)
        {
            PlayAnimation();
        }
    }

    protected void PlayAnimation()
    {
        animFinished = false;
        anim.SetTrigger("IsHit");
    }

    public void Event_AnimFinished()
    {
        animFinished = true;
    }

}
