using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    // Remember to consider adding new arguments to ResetArgumentsAfterLaod() so that they won't carry e.g. after players death
    private static MarioLevelEnum marioLevelDefault = MarioLevelEnum.notSet;
    private static MarioLevelEnum marioLevel = marioLevelDefault;

    private GameObject player;
    private PlayerPowerup playerPowerup;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        playerPowerup = player.GetComponent<PlayerPowerup>();
    }

    public void NextLevel()
    {
        SaveArguments();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SaveArguments()
    {
        marioLevel = playerPowerup.GetLevel();
    }

    public void LoadArguments()
    {
        if (marioLevel != MarioLevelEnum.notSet) playerPowerup.ChangeAppearanceToLevel(marioLevel);

        ResetArgumentsAfterLaod();
    }

    private void ResetArgumentsAfterLaod()
    {
        marioLevel = marioLevelDefault;
    }

}
