using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    public AudioSource audioPowerup;
    public float invDuration = 12f;

    private Environment environment;
    private bool invincible = false;

    private void Awake()
    {
        environment = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
    }

    public void Invincibility()
    {
        StartCoroutine(InvincibilityMain(invDuration));
    }

    private IEnumerator InvincibilityMain(float seconds)
    {
        StartInvincibility();
        yield return new WaitForSeconds(seconds);
        EndInvincibility();
    }

    private void StartInvincibility()
    {
        audioPowerup.Play();
        environment.PlayInvincibility(true);
        invincible = true;
    }

    private void EndInvincibility()
    {
        environment.PlayMain(true);
        invincible = false;
    }

    public bool GetInvincible()
    {
        return invincible;
    }

}
