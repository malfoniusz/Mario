using UnityEngine;
using UnityEngine.UI;

public class UICoins : MonoBehaviour
{
    private static Text coinText;
    private static int coins = 0;
    private const int EXTRA_LIFE_COINS = 100;

    void Awake()
    {
        coinText = GetComponent<Text>();
        UpdateText();
    }

    static void UpdateText()
    {
        if (coins > 9)
        {
            coinText.text = coins.ToString();
        }
        else
        {
            coinText.text = "0" + coins.ToString();
        }
    }

    public static bool AddCoin()
    {
        coins++;
        bool extraLife = ExtraLife();
        UpdateText();

        return extraLife;
    }

    private static bool ExtraLife()
    {
        if (coins >= EXTRA_LIFE_COINS)
        {
            coins -= EXTRA_LIFE_COINS;
            UILives.AddLife();
            return true;
        }

        return false;
    }

    public static void ResetCoins()
    {
        coins = 0;
    }

}
