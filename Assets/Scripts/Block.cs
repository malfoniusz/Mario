using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform block;
    public Transform[] bottomHitChecks;

    private AudioSource hitAudio;
    private Animator anim;
    private int playerMask;
    private bool animFinished = true;

    void Awake()
    {
        hitAudio = GameObject.FindGameObjectWithTag("BlockHitAudio").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playerMask = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        bool playerHit = false;
        for (int i = 0; i < bottomHitChecks.Length; i++)
        {
            playerHit = Physics2D.Linecast(block.position, bottomHitChecks[i].position, 1 << playerMask);
            if (playerHit && animFinished)
            {
                PlayAnimation();
                break;
            }
        }
    }

    void PlayAnimation()
    {
        animFinished = false;
        anim.SetTrigger("IsHit");

        if (hitAudio.isPlaying == false)
        {
            hitAudio.Play();
        }
    }

    void Event_AnimationFinished()
    {
        animFinished = true;
    }

}
