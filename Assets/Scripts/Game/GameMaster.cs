using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool showFPS = false;
    public bool quickStart = false;
    public bool playerInvincible = false;
    public GameObject FPSCounter;

    private GameObject player;
    private PlayerInvincibility invincibility;
    private GameController gameController;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        invincibility = player.GetComponent<PlayerInvincibility>();
        gameController = GetComponent<GameController>();

        if (gameController != null)
        {
            gameController.SetQuickStart(quickStart); // If run in Start it doesn't set GameController.quickStart
        }
    }

    private void Start()
    {
        if (!showFPS) FPSCounter.SetActive(false);
        invincibility.SetInvincible(playerInvincible);
    }

}
