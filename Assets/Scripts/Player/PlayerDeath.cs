using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public AudioClip deathClip;

    private GameObject parent;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private PlayerMovement playerMovement;
    private bool playerDied = false;

    void Awake()
    {
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
            GameController.PlayerDied();
        }
    }

    public void Die()
    {
        if (playerDied == false)
        {
            playerDied = true;

            GameController.StopGame();
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
