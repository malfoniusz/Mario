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

    private void Start()
    {
        // TESTING
        //playerPowerup.ChangeAppearanceToLevel(marioPowerupLevel);
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

    public void LoadArguments()
    {
        if (marioPowerupLevel != -1) playerPowerup.ChangeAppearanceToLevel(marioPowerupLevel);
    }

}
