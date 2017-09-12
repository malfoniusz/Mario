using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool quickStart = false;

    private GameObject environment;
    private AudioSource environmentAudio;
    private AudioSource audioGameOver;
    private GameObject player;
    private GameObject startLevelScreen;
    private GameObject gameOverScreen;
    private const float START_DELAY = 2;

    private void Awake()
    {
        environment = GameObject.FindGameObjectWithTag("Environment");
        environmentAudio = environment.GetComponent<AudioSource>();
        audioGameOver = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        environment.SetActive(true);
        player.SetActive(true);
    }

    void HideLevel()
    {
        environment.SetActive(false);
        player.SetActive(false);
    }

    void ShowStartLevel()
    {
        startLevelScreen.SetActive(true);
        HideLevel();
        StopGame();
    }

    void HideStartLevel()
    {
        startLevelScreen.SetActive(false);
        ShowLevel();
        ResumeGame();
    }

    void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        HideLevel();
        StopGame();
        audioGameOver.Play();
    }

    void HideGameOver()
    {
        gameOverScreen.SetActive(false);
        ShowLevel();
        ResumeGame();
    }

    public void StopGame()
    {
        Goomba.stop = true;
        UITime.stop = true;
        environmentAudio.Stop();
    }

    void ResumeGame()
    {
        Goomba.stop = false;
        UITime.stop = false;
        environmentAudio.Play();
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
