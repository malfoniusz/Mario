using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool showFPS = false;
    public bool quickStart = false;
    public bool playerInvincible = false;
    public bool stopDisablingObjects = false;

    private GameObject player;
    private PlayerInvincibility invincibility;
    private GameController gameController;
    private GameObject cam;
    private ActiveObjects activeObjects;
    private GameObject fpsCounter;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        invincibility = player.GetComponent<PlayerInvincibility>();
        gameController = GetComponent<GameController>();
        if (gameController != null) gameController.SetQuickStart(quickStart); // If run in Start it doesn't set GameController.quickStart
        cam = TagNames.GetMainCamera();
        activeObjects = cam.GetComponent<ActiveObjects>();
        fpsCounter = TagNames.GetFPSCounter();
    }

    private void Start()
    {
        fpsCounter.SetActive(showFPS);
        invincibility.SetInvincible(playerInvincible);
        activeObjects.SetStopDisabling(stopDisablingObjects);
    }

}
