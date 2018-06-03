using UnityEngine;
using UnityEngine.UI;

public class UIWorld : MonoBehaviour
{
    public static int world = 1;
    public static int level = 1;

    private Text levelText;

    void Awake()
    {
        levelText = GetComponent<Text>();
    }

    void Start()
    {
        levelText.text = world + "-" + level;
    }

}
