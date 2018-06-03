using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static bool firstRun = true;

    public bool showFPS = false;
    public bool quickStart = false;
    public bool playerInvincible = false;
    public bool stopDisablingObjects = false;
    public MarioLevelEnum marioStartingLevel = MarioLevelEnum.notSet;
    public int newStartingTime = -1;

    private GameObject player;
    private PlayerInvincibility invincibility;
    private PlayerPowerup playerPowerup;
    private GameController gameController;
    private GameObject cam;
    private ActiveObjects activeObjects;
    private GameObject fpsCounter;
    private UITime uiTime;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        invincibility = player.GetComponent<PlayerInvincibility>();
        playerPowerup = player.GetComponent<PlayerPowerup>();
        gameController = GetComponent<GameController>();
        if (gameController != null) gameController.SetQuickStart(quickStart); // If run in Start it doesn't set GameController.quickStart
        cam = TagNames.GetMainCamera();
        activeObjects = cam.GetComponent<ActiveObjects>();
        fpsCounter = TagNames.GetFPSCounter();
        uiTime = TagNames.GetUITime();
    }

    private void Start()
    {
        LoadOnEveryLevel();
        if (firstRun)
        {
            LoadOnce();
            firstRun = false;
        }
    }

    private void LoadOnEveryLevel()
    {
        fpsCounter.SetActive(showFPS);
        invincibility.SetInvincible(playerInvincible);
        activeObjects.SetStopDisabling(stopDisablingObjects);
    }

    private void LoadOnce()
    {
        if (marioStartingLevel != MarioLevelEnum.notSet) playerPowerup.ChangeAppearanceToLevel(marioStartingLevel);
        if (newStartingTime >= 0) uiTime.SetTime(newStartingTime);
    }

}
