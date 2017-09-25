using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public AudioSource deathAudio;

    private GameController gameController;
    private GameObject parent;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool playerDied = false;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        parent = transform.parent.gameObject;
        anim = parent.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (playerDied && deathAudio.isPlaying == false)
        {
            gameController.PlayerDied();
        }
    }

    public void Die()
    {
        if (playerDied == false)
        {
            playerDied = true;

            gameController.StopGame(true);
            rb.isKinematic = true;
            boxCollider.enabled = false;

            anim.speed = 1;
            anim.SetBool("IsJumping", false);
            anim.SetTrigger("IsDead");
            deathAudio.Play();
        }
    }

}
