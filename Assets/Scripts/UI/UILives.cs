using UnityEngine;
using UnityEngine.UI;

public class UILives : MonoBehaviour
{
    private const int STARTING_LIVES = 3;
    private static int lives = STARTING_LIVES;

    private Text livesText;

    private void Awake()
    {
        livesText = GetComponent<Text>();
    }

    private void Start()
    {
        livesText.text = lives.ToString();
    }

    public static void ResetLives()
    {
        lives = STARTING_LIVES;
    }

    public static void AddLive()
    {
        lives++;
    }

    public static void DecreaseLive()
    {
        lives--;
    }

    public static void ChangeLivesBy(int value)
    {
        lives += value;
    }

    public static int GetLives()
    {
        return lives;
    }

}
