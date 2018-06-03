using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private static MarioLevelEnum marioLevel = MarioLevelEnum.notSet;

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

    public void PrepareLevel()
    {
        if (marioLevel != MarioLevelEnum.notSet) playerPowerup.ChangeAppearanceToLevel(marioLevel);
    }

}
