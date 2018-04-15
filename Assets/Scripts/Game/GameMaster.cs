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
    }

    private void Start()
    {
        if (!showFPS) FPSCounter.SetActive(false);
        gameController.quickStart = quickStart;
        invincibility.SetInvincible(playerInvincible);
    }

}
