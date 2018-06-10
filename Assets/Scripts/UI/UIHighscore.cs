using UnityEngine;
using UnityEngine.UI;

public class UIHighscore : MonoBehaviour
{
    private Text highscoreText;
    private const int TEXT_MAX_LENGTH = 6;

    private void Awake()
    {
        highscoreText = GetComponent<Text>();
    }

    private void Start()
    {
        highscoreText.text = PlayerPrefsNames.GetHighscore().ToString();
        highscoreText.text = StringUtility.AddZerosToBeginning(TEXT_MAX_LENGTH, highscoreText.text);
    }

}
