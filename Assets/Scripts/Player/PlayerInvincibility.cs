using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    public AudioSource audioPowerup;
    public float invDuration = 10f;
    public float invCloseToEndTime = 2f;

    private Environment environment;
    private bool invincible = false;

    private void Awake()
    {
        environment = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
    }

    public void Invincibility()
    {
        StartCoroutine(InvincibilityMain());
    }

    private IEnumerator InvincibilityMain()
    {
        audioPowerup.Play();
        environment.PlayInvincibility(true);
        // Start animation
        invincible = true;

        yield return new WaitForSeconds(invDuration - invCloseToEndTime);

        environment.PlayMain(true);
        // Zwolnienie animacji

        yield return new WaitForSeconds(invCloseToEndTime);

        // End animation
        invincible = false;
    }

    public bool GetInvincible()
    {
        return invincible;
    }

}
