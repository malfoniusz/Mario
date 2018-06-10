using UnityEngine;

public class SceneTransfer : MonoBehaviour
{
    // Remember to add new arguments to proper functions: NextLevel(), ResetArgumentsAtGameOver(), etc.

    private static MarioLevelEnum marioLevelDefault = MarioLevelEnum.notSet;
    private static MarioLevelEnum marioLevel = marioLevelDefault;

    private static Vector2 savepointDefault = Vector2.zero;
    private static Vector2 savepoint = savepointDefault;

    private GameObject player;
    private PlayerPowerup playerPowerup;
    private CameraFollow cameraFollow;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        playerPowerup = player.GetComponent<PlayerPowerup>();
        cameraFollow = TagNames.GetCameraFollow();
    }

    public void NextLevel()
    {
        SaveArguments();
        ResetArgumentsBeforeNextLevel();
        SceneNames.LoadStartMenu();
    }

    private void SaveArguments()
    {
        marioLevel = playerPowerup.GetLevel();
    }

    // Arguments are loaded after every Scene reload
    public void LoadArguments()
    {
        if (marioLevel != marioLevelDefault) playerPowerup.ChangeAppearanceToLevel(marioLevel);
        if (savepoint != savepointDefault)
        {
            player.transform.position = savepoint;
            cameraFollow.Refresh();
        }
    }

    public void SaveSavepoint(Vector2 value)
    {
        savepoint = value;
    }

    public void ResetArgumentsAtPlayerDeath()
    {
        marioLevel = marioLevelDefault;
    }

    private void ResetArgumentsBeforeNextLevel()
    {
        savepoint = savepointDefault;
    }

    public void ResetArgumentsAtGameOver()
    {
        marioLevel = marioLevelDefault;
        savepoint = savepointDefault;
    }

}
