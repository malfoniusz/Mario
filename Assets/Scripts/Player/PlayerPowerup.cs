using UnityEngine;
using System.Collections;

public class PlayerPowerup : MonoBehaviour
{
    private GameController gameController;
    private PlayerDeath playerDeath;

    public Animator anim;
    public Animator animBigMario;
    public Animator animFireMario;
    public AudioClip powerupClip;
    public int level = 1;   // 1 - mario, 2 - bigMario, 3 - fireMario

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerDeath = GetComponent<PlayerDeath>();
    }

    public void Powerup()
    {
        if (level == 1 || level == 2)
        {
            PowerupBehaviour();
        }
    }

    private void PowerupBehaviour()
    {
        gameController.StopGame(false);

        if (level == 1)
        {
            // podmienic animatora na BigMario - nie bawic sie z zachowanie animacji np. skoku
            // uruchomic Powerup sound on Mario
        }
        else if (level == 2)
        {
            // FireMario
        }

        StartCoroutine(ResumeGame());
    }

    IEnumerator ResumeGame()
    {
        // Sprawdzic czy konkretna animacja sie skonczyla, np. poprzez sprawdzenie jej nazwy
        while (false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        gameController.ResumeGame(false);
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
        // po uderzeniu: animacja + zatrzymanie przeciwnikow + niezniszczalnosc (migotanie)

        if (level == 2)
        {

        }
        else if (level == 3)
        {

        }
    }
    
}
