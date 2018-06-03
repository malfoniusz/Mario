using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private static int marioPowerupLevel = -1;

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
        marioPowerupLevel = playerPowerup.GetLevel();
    }

    public void PrepareLevel()
    {
        if (marioPowerupLevel != -1) playerPowerup.ChangeAppearanceToLevel(marioPowerupLevel);
    }

}
