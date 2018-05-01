using UnityEngine;

public class TransferScreen : MonoBehaviour
{
    private GameObject player;
    private MusicController musicController;
    private ActiveObjects activeObjects;
    private CameraFollow camFollow;
    private Camera cam;
    private float camDefaultY;
    private float CAM_DEFAULT_X_RIGHT_FROM_PLAYER = 110;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        activeObjects = GetComponent<ActiveObjects>();
        camFollow = GetComponent<CameraFollow>();
        cam = GetComponent<Camera>();
        camDefaultY = transform.position.y;
    }

    private void Start()
    {
        // Starting position of camera in relation to player starting position
        transform.position = new Vector3(player.transform.position.x + CAM_DEFAULT_X_RIGHT_FROM_PLAYER, camDefaultY, transform.position.z);
    }

    public void Transfer(MusicEnum newMusicName, Vector2 newPlayerPos, Color backgroundColor, bool staticCam)
    {
        Vector2 newCameraPos = new Vector2(newPlayerPos.x + CAM_DEFAULT_X_RIGHT_FROM_PLAYER, camDefaultY);

        Transfer(newMusicName, newPlayerPos, newCameraPos, backgroundColor, staticCam);
    }

    public void Transfer(MusicEnum newMusicName, Vector2 newPlayerPos, Vector2 newCameraPos, Color backgroundColor, bool staticCam)
    {
        musicController.Play(newMusicName, true);
        player.transform.position = newPlayerPos;

        transform.position = new Vector3(newCameraPos.x, newCameraPos.y, transform.position.z);

        activeObjects.Refresh();
        cam.backgroundColor = backgroundColor;

        camFollow.Pause(staticCam);
    }

}
