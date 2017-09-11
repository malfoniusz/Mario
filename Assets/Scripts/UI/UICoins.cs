using UnityEngine;
using UnityEngine.UI;

public class UICoins : MonoBehaviour
{
    private static Text coinText;
    private static AudioSource audioSource;
    public static int coinValue = 99;
    private const int EXTRA_LIFE_COINS = 100;

    void Awake()
    {
        coinText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
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

    public static bool AddCoin()
    {
        coinValue++;
        bool extraLife = ExtraLife();
        UpdateText();

        return extraLife;
    }

    private static bool ExtraLife()
    {
        if (coinValue >= EXTRA_LIFE_COINS)
        {
            coinValue -= EXTRA_LIFE_COINS;
            UILives.lives++;
            audioSource.Play();
            return true;
        }

        return false;
    }

}
