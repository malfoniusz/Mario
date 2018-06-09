using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    public Image[] optionsImages;

    private int activeOptionIndex = 0;
    private int newActiveOptionIndex;

    private void Start()
    {
        newActiveOptionIndex = activeOptionIndex;
        optionsImages[activeOptionIndex].enabled = true;
    }

    private void Update()
    {
        PlayerChangeActiveOption();
        if (ButtonNames.SpacePressed() || ButtonNames.EnterPressed()) OptionSelected();
    }

    private void PlayerChangeActiveOption()
    {
        if      (ButtonNames.UpPressed())   newActiveOptionIndex--;
        else if (ButtonNames.DownPressed()) newActiveOptionIndex++;

        if      (newActiveOptionIndex < 0)                     newActiveOptionIndex = optionsImages.Length - 1;
        else if (newActiveOptionIndex >= optionsImages.Length) newActiveOptionIndex = 0;

        if (activeOptionIndex != newActiveOptionIndex)
        {
            optionsImages[activeOptionIndex].enabled = false;
            optionsImages[newActiveOptionIndex].enabled = true;
            activeOptionIndex = newActiveOptionIndex;
        }
    }

    private void OptionSelected()
    {
        if      (activeOptionIndex == 0) StartGame();
        else if (activeOptionIndex == 1) ExitGame();
    }

    private void StartGame()
    {
        SceneNames.LoadLevel1_1();
    }

    private void ExitGame()
    {
        Application.Quit();
    }

}
