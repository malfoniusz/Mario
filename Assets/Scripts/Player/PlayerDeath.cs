using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public AudioClip deathClip;

    private MusicController musicController;
    private GameController gameController;
    private GameObject parent;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        gameController = TagNames.GetGameController().GetComponent<GameController>();
        parent = transform.parent.gameObject;
        anim = parent.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Die()
    {
        gameController.StopGame(false);
        rb.isKinematic = true;
        boxCollider.enabled = false;

        anim.SetBool(AnimatorNames.playerIsJumping, false);
        anim.SetTrigger(AnimatorNames.playerIsDead);
        musicController.Play(MusicNames.death, true);

        StartCoroutine(endGame(deathClip.length));
    }

    private IEnumerator endGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameController.PlayerDied();
    }

}
