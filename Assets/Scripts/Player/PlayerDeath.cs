﻿using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public AudioClip deathClip;

    private GameController gameController;
    private GameObject parent;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private PlayerMovement playerMovement;
    private bool playerDied = false;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        parent = transform.parent.gameObject;
        anim = parent.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerDied && audioSource.isPlaying == false)
        {
            gameController.PlayerDied();
        }
    }

    public void Die()
    {
        if (playerDied == false)
        {
            playerDied = true;

            gameController.StopGame();
            playerMovement.playerDead = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            boxCollider.enabled = false;

            anim.speed = 1;
            anim.SetBool("IsJumping", false);
            anim.SetTrigger("IsDead");
            audioSource.clip = deathClip;
            audioSource.Play();
        }
    }

}
