using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool quickStart = false;

    private GameObject environmentObject;
    private Environment environment;
    private AudioSource audioGameOver;
    private GameObject player;
    private PlayerMovement playerMovement;
    private GameObject startLevelScreen;
    private GameObject gameOverScreen;
    private const float START_DELAY = 2;

    private void Awake()
    {
        environmentObject = GameObject.FindGameObjectWithTag("Environment");
        environment = environmentObject.GetComponent<Environment>();
        audioGameOver = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        startLevelScreen = GameObject.FindGameObjectWithTag("StartLevelScreen");
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
    }

    private void Start()
    {
        HideStartLevel();
        HideGameOver();

        if (!quickStart)
        {
            ShowStartLevel();
            StartCoroutine(StartLevel());
        }
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(START_DELAY);
        HideStartLevel();
    }

    public void PlayerDied()
    {
        UILives.lives--;

        if (UILives.lives <= 0)
        {
            ShowGameOver();
            StartCoroutine(RestartGame(audioGameOver.clip.length));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void ShowLevel()
    {
        environmentObject.SetActive(true);
        player.SetActive(true);
    }

    void HideLevel()
    {
        environmentObject.SetActive(false);
        player.SetActive(false);
    }

    void ShowStartLevel()
    {
        startLevelScreen.SetActive(true);
        HideLevel();
        StopGame(true);
    }

    void HideStartLevel()
    {
        startLevelScreen.SetActive(false);
        ShowLevel();
        ResumeGame(true);
    }

    void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        HideLevel();
        StopGame(true);
        audioGameOver.Play();
    }

    void HideGameOver()
    {
        gameOverScreen.SetActive(false);
        ShowLevel();
        ResumeGame(true);
    }

    public void StopGame(bool pauseMusic)
    {
        Moving.stop = true;
        UITime.stop = true;
        playerMovement.Stop(true);
        PlayerFireball.Stop(true);
        if (pauseMusic) environment.PauseCurrentMusic();
    }

    public void ResumeGame(bool unPauseMusic)
    {
        Moving.stop = false;
        UITime.stop = false;
        playerMovement.Stop(false);
        PlayerFireball.Stop(false);
        if (unPauseMusic) environment.PlayCurrentMusic();
    }

    IEnumerator RestartGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIPoints.ResetPoints();
        UICoins.ResetCoins();
        UILives.ResetLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
