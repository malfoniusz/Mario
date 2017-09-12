using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    private static Text pointText;
    private static int points = 0;
    private const int MAX_TEXT_LENGTH = 6;

    void Awake()
    {
        pointText = GetComponent<Text>();
        UpdateText();
    }

    static void UpdateText()
    {
        int zeros = MAX_TEXT_LENGTH - points.ToString().Length;
        string text = "";
        for (int i = 0; i < zeros; i++)
        {
            text += "0";
        }

        text += points.ToString();
        pointText.text = text;
    }

    public static void AddPoints(int amount)
    {
        points += amount;
        UpdateText();
    }

    public static void ResetPoints()
    {
        points = 0;
    }

}
