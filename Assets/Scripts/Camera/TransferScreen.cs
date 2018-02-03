using UnityEngine;

public class TransferScreen : MonoBehaviour
{
    private GameObject player;
    private Environment environment;
    private ActiveObjects activeObjects;
    private CameraFollow camFollow;
    private Camera cam;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        environment = TagNames.GetEnvironment().GetComponent<Environment>();
        activeObjects = GetComponent<ActiveObjects>();
        camFollow = GetComponent<CameraFollow>();
        cam = GetComponent<Camera>();
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
