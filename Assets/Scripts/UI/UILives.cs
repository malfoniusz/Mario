using UnityEngine;
using UnityEngine.UI;

public class UILives : MonoBehaviour
{
    public const int STARTING_LIVES = 3;
    public static int lives = STARTING_LIVES;

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

}
