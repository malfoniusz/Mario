using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int worldNumber = -1;
    public int levelNumber = -1;
    public GameObject startLevelScreen;
    public GameObject gameOverScreen;

    private GameObject environmentObject;
    private MusicController musicController;
    private GameObject player;
    private PlayerMovement playerMovement;
    private const float START_DELAY = 2;
    private bool quickStart;
    private SceneTransfer sceneTransfer;
    private UIWorld uiWorld;
    private const float RESTART_DELAY = 1f;

    private void Awake()
    {
        environmentObject = TagNames.GetEnvironment();
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        player = TagNames.GetPlayer();
        playerMovement = player.GetComponent<PlayerMovement>();
        sceneTransfer = TagNames.GetSceneTransfer();
        uiWorld = TagNames.GetUIWorld();
    }

    private void Start()
    {
        sceneTransfer.LoadArguments();
        uiWorld.SetWorld(worldNumber);
        uiWorld.SetLevel(levelNumber);

        HideStartLevel();
        HideGameOver();

        if (quickStart == false)
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
        UILives.DecreaseLife();

        if (UILives.GetLives() <= 0)
        {
            ShowGameOver();
            StartCoroutine(GameOverReset());
        }
        else
        {
            sceneTransfer.ResetArgumentsAtPlayerDeath();
            SceneNames.ReloadScene();
        }
    }

    private void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        HideLevel();
        StopGame(true);
        musicController.Play(MusicEnum.gameOver, true);
    }

    private void HideGameOver()
    {
        gameOverScreen.SetActive(false);
        ShowLevel();
        ResumeGame(true);
    }

    private void SaveHighscore()
    {
        int points = UIPoints.GetPoints();
        if (points > PlayerPrefsNames.GetHighscore())
        {
            PlayerPrefsNames.SaveHighscore(points);
        }
    }

    private IEnumerator GameOverReset()
    {
        float gameOverMusicLength = musicController.GetMusicLength(MusicEnum.gameOver) + RESTART_DELAY;

        yield return new WaitForSeconds(gameOverMusicLength);
        SaveHighscore();
        UIPoints.ResetPoints();
        UICoins.ResetCoins();
        UILives.ResetLives();
        sceneTransfer.ResetArgumentsAtGameOver();

        SceneNames.LoadStartMenu();
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
        playerMovement.DisablePlayer(true, false);
        PlayerFireball.Stop(true);
        if (pauseMusic) musicController.PauseCurrentMusic();    // environmentObject.SetActive(false); - rowniez wylacza muzyke
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
        playerMovement.DisablePlayer(false, false);
        PlayerFireball.Stop(false);
        if (playMusic) musicController.PlayCurrentMusic();
    }

    public void NextLevel()
    {
        sceneTransfer.NextLevel();
    }

    public bool GetQuickStart()
    {
        return quickStart;
    }

    public void SetQuickStart(bool quickStart)
    {
        this.quickStart = quickStart;
    }

}
