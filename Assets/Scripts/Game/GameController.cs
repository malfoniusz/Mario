using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool quickStart = false;

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
        environmentObject = TagNames.GetEnvironment();
        environment = environmentObject.GetComponent<Environment>();
        audioGameOver = GetComponent<AudioSource>();
        player = TagNames.GetPlayer();
        playerMovement = player.GetComponent<PlayerMovement>();
        startLevelScreen = TagNames.GetStartLevelScreen();
        gameOverScreen = TagNames.GetGameOverScreen();
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

    private IEnumerator StartLevel()
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

    private void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        HideLevel();
        StopGame(true);
        audioGameOver.Play();
    }

    private void HideGameOver()
    {
        gameOverScreen.SetActive(false);
        ShowLevel();
        ResumeGame(true);
    }

    private IEnumerator RestartGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIPoints.ResetPoints();
        UICoins.ResetCoins();
        UILives.ResetLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStartLevel()
    {
        startLevelScreen.SetActive(true);
        HideLevel();
        StopGame(true);
    }

    private void HideLevel()
    {
        environmentObject.SetActive(false);
        player.SetActive(false);
    }

    public void StopGame(bool pauseMusic)
    {
        Moving.stop = true;
        UITime.stop = true;
        playerMovement.Stop(true);
        PlayerFireball.Stop(true);
        if (pauseMusic) environment.PauseCurrentMusic();    // environmentObject.SetActive(false); - rowniez wylacza muzyke
    }

    private void HideStartLevel()
    {
        startLevelScreen.SetActive(false);
        ShowLevel();
        ResumeGame(true);
    }

    private void ShowLevel()
    {
        environmentObject.SetActive(true);
        player.SetActive(true);
    }

    public void ResumeGame(bool playMusic)
    {
        Moving.stop = false;
        UITime.stop = false;
        playerMovement.Stop(false);
        PlayerFireball.Stop(false);
        if (playMusic) environment.PlayCurrentMusic();
    }

}
