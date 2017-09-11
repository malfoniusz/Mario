using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private static GameObject environment;
    private static AudioSource environmentAudio;
    private static GameObject player;
    private static GameObject startLevelScreen;
    private const float START_DELAY = 2;

    private void Awake()
    {
        environment = GameObject.FindGameObjectWithTag("Environment");
        environmentAudio = environment.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        startLevelScreen = GameObject.FindGameObjectWithTag("StartLevelScreen");
    }

    private void Start()
    {
        HideLevel();
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(START_DELAY);
        ShowLevel();
    }

    public static void PlayerDied()
    {
        UILives.lives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    static void HideLevel()
    {
        environment.SetActive(false);
        player.SetActive(false);
        startLevelScreen.SetActive(true);

        StopGame();
    }

    static void ShowLevel()
    {
        environment.SetActive(true);
        player.SetActive(true);
        startLevelScreen.SetActive(false);

        ResumeGame();
    }

    public static void StopGame()
    {
        Goomba.stop = true;
        UITime.stop = true;
        environmentAudio.Stop();
    }

    static void ResumeGame()
    {
        Goomba.stop = false;
        UITime.stop = false;
        environmentAudio.Play();
    }

}
