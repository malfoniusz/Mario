using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioPowerup;
    public float invDuration = 10f;
    public float invCloseToEndTime = 2f;

    private Environment environment;
    private int animInvLayerIndex;
    private bool invincible = false;

    private void Awake()
    {
        environment = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
        animInvLayerIndex = anim.GetLayerIndex("Invincibility");
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

        yield return new WaitForSeconds(invDuration - invCloseToEndTime);

        environment.PlayMain(true);
        // Zwolnienie animacji

        yield return new WaitForSeconds(invCloseToEndTime);

        anim.SetLayerWeight(animInvLayerIndex, 0);
        invincible = false;
    }

    public bool GetInvincible()
    {
        return invincible;
    }

}
