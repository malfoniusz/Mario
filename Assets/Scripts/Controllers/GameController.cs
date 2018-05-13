﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject startLevelScreen;
    public GameObject gameOverScreen;

    private GameObject environmentObject;
    private MusicController musicController;
    private GameObject player;
    private PlayerMovement playerMovement;
    private const float START_DELAY = 2;
    private bool quickStart;

    private void Awake()
    {
        environmentObject = TagNames.GetEnvironment();
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        player = TagNames.GetPlayer();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
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
        UILives.lives--;

        if (UILives.lives <= 0)
        {
            ShowGameOver();
            StartCoroutine(RestartGame());
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
        musicController.Play(MusicEnum.gameOver, true);
    }

    private void HideGameOver()
    {
        gameOverScreen.SetActive(false);
        ShowLevel();
        ResumeGame(true);
    }

    private IEnumerator RestartGame()
    {
        float gameOverMusicLength = musicController.GetMusicLength(MusicEnum.gameOver);

        yield return new WaitForSeconds(gameOverMusicLength);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
