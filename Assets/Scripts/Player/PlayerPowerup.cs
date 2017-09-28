﻿using UnityEngine;
using System.Collections;

public class PlayerPowerup : MonoBehaviour
{
    public Animator anim;
    public AudioClip jumpClip;
    public AudioClip bigJumpClip;
    public AudioSource jumpAudio;
    public AudioSource powerupAudio;
    public AudioSource powerdownAudio;
    public int level = 1;   // 1 - mario, 2 - bigMario, 3 - fireMario

    private PlayerDeath playerDeath;
    private const float INVINCIBILITY_DURATION = 2.5f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerDeath = GetComponent<PlayerDeath>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void MushroomPowerup()
    {
        if (level == 1)
        {
            PowerupBehaviour(2);
        }
    }

    public void FireFlowerPowerup()
    {
        if (level == 1 || level == 2)
        {
            PowerupBehaviour(3);
        }
    }

    private void PowerupBehaviour(int newLevel)
    {
        level = newLevel;
        anim.SetTrigger("Powerup");
        anim.speed = 1;
        powerupAudio.Play();
        jumpAudio.clip = bigJumpClip;
    }

    public void PlayerHit()
    {
        if (level == 1)
        {
            playerDeath.Die();
        }
        else
        {
            Powerdown();
        }
    }

    private void Powerdown()
    {
        level--;
        anim.SetTrigger("Powerdown");
        anim.speed = 1;
        powerdownAudio.Play();

        if (level == 1)
        {
            jumpAudio.clip = jumpClip;
        }

        StartCoroutine(Invincibility(INVINCIBILITY_DURATION));
    }
    
    private IEnumerator Invincibility(float sec)
    {
        Color trans = spriteRenderer.color;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
        trans.a = 0.5f;
        spriteRenderer.color = trans;

        yield return new WaitForSeconds(sec);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        trans.a = 1f;
        spriteRenderer.color = trans;
    }

}
