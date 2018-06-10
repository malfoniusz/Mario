using UnityEngine;

public class PlayerPrefsNames : MonoBehaviour
{
    public static string highscore = "Highscore";

    public static void SaveHighscore(int score)
    {
        PlayerPrefs.SetInt(highscore, score);
    }

    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt(highscore, 0);
    }

}
