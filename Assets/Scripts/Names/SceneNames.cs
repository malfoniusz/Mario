using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNames : MonoBehaviour
{
    public static string startMenu = "StartMenu";
    public static string level1_1 = "Level 1-1";

    public static void EndGameLoadStartMenu(int score)
    {
        SaveHighscore(score);

        ButtonNames.disableInput = false;
        UIPoints.ResetPoints();
        UICoins.ResetCoins();
        UILives.ResetLives();

        SceneManager.LoadScene(startMenu);
    }

    private static void SaveHighscore(int score)
    {
        if (score > PlayerPrefsNames.GetHighscore())
        {
            PlayerPrefsNames.SaveHighscore(score);
        }
    }

    public static void LoadLevel1_1()
    {
        SceneManager.LoadScene(level1_1);
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
