using UnityEngine;

public class TransferScreen : MonoBehaviour
{
    private GameObject player;
    private Environment environment;
    private ActiveObjects activeObjects;
    private CameraFollow camFollow;
    private Camera cam;
    private float camDefaultXRightFromPlayer;
    private float camDefaultY;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        environment = TagNames.GetEnvironment().GetComponent<Environment>();
        activeObjects = GetComponent<ActiveObjects>();
        camFollow = GetComponent<CameraFollow>();
        cam = GetComponent<Camera>();
        camDefaultXRightFromPlayer = transform.position.x - player.transform.position.x;
        camDefaultY = transform.position.y;
    }

    public void Transfer(string newMusicName, Vector2 newPlayerPos, Color backgroundColor, bool staticCam)
    {
        Vector2 newCameraPos = new Vector2(newPlayerPos.x + camDefaultXRightFromPlayer, camDefaultY);

        Transfer(newMusicName, newPlayerPos, newCameraPos, backgroundColor, staticCam);
    }

    public void Transfer(string newMusicName, Vector2 newPlayerPos, Vector2 newCameraPos, Color backgroundColor, bool staticCam)
    {
        environment.Play(newMusicName, true);
        player.transform.position = newPlayerPos;

        transform.position = new Vector3(newCameraPos.x, newCameraPos.y, transform.position.z);

        activeObjects.Refresh();
        cam.backgroundColor = backgroundColor;

        camFollow.Pause(staticCam);
    }

}
