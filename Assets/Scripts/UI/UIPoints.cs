using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    private static Text pointText;
    private static int points = 0;
    private const int MAX_TEXT_LENGTH = 6;

    private void Awake()
    {
        pointText = GetComponent<Text>();
        UpdateText();
    }

    private static void UpdateText()
    {
        pointText.text = points.ToString();
        pointText.text = StringUtility.AddZerosToBeginning(MAX_TEXT_LENGTH, pointText.text);
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

    public static int GetPoints()
    {
        return points;
    }

    public static void SetPoints(int value)
    {
        points = value;
    }

}
