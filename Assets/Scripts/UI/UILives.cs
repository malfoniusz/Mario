using UnityEngine;
using UnityEngine.UI;

public class UILives : MonoBehaviour
{
    public static int lives = 3;

    private Text livesText;

    private void Awake()
    {
        livesText = GetComponent<Text>();
    }

    private void Start()
    {
        livesText.text = lives.ToString();
    }

}
