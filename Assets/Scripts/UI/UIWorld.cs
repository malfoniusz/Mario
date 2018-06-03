using UnityEngine;
using UnityEngine.UI;

public class UIWorld : MonoBehaviour
{
    public Text startScreenText;

    private int world = -1;
    private int level = -1;
    private Text levelText;

    private void Awake()
    {
        levelText = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateText();
    }

    public int GetWorld()
    {
        return world;
    }

    public void SetWorld(int newWorld)
    {
        world = newWorld;
        UpdateText();
    }

    public int Getlevel()
    {
        return level;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        UpdateText();
    }

    private void UpdateText()
    {
        string textString = world + "-" + level;
        levelText.text = textString;
        startScreenText.text = textString;
    }

}
