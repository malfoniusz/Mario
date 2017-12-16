﻿using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioPowerup;
    public float invDuration = 10f;
    public float invExpireTime = 2f;

    private Environment environment;
    private int animInvLayerIndex;
    private int animInvExpiereLayerIndex;
    private bool invincible = false;

    private void Awake()
    {
        environment = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
        animInvLayerIndex = anim.GetLayerIndex("Invincibility");
        animInvExpiereLayerIndex = anim.GetLayerIndex("InvincibilityExpire");
    }

    public void Invincibility()
    {
        StartCoroutine(InvincibilityMain());
    }

    private IEnumerator InvincibilityMain()
    {
        audioPowerup.Play();
        environment.PlayInvincibility(true);
        anim.SetLayerWeight(animInvLayerIndex, 1);
        invincible = true;

        yield return new WaitForSeconds(invDuration - invExpireTime);

        environment.PlayMain(true);
        anim.SetLayerWeight(animInvLayerIndex, 0);
        anim.SetLayerWeight(animInvExpiereLayerIndex, 1);

        yield return new WaitForSeconds(invExpireTime);

        anim.SetLayerWeight(animInvExpiereLayerIndex, 0);
        invincible = false;
    }

    public bool GetInvincible()
    {
        return invincible;
    }

}
