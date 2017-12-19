using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public AudioClip deathClip;

    private Environment environment;
    private GameController gameController;
    private GameObject parent;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        environment = TagNames.GetEnvironment().GetComponent<Environment>();
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
        environment.PlayDeath(true);

        StartCoroutine(endGame(deathClip.length));
    }

    private IEnumerator endGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameController.PlayerDied();
    }

}
