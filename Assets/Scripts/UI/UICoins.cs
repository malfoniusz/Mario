using UnityEngine;
using UnityEngine.UI;

public class UICoins : MonoBehaviour
{
    private static Text coinText;
    private static int coinValue = 0;

    void Awake()
    {
        coinText = GetComponent<Text>();
        UpdateText();
    }

    static void UpdateText()
    {
        if (coinValue > 9)
        {
            coinText.text = coinValue.ToString();
        }
        else
        {
            coinText.text = "0" + coinValue.ToString();
        }
    }

    public static void AddCoins(int amount)
    {
        coinValue += amount;
        UpdateText();
    }

}
