using UnityEngine;

public class TransferScreen : MonoBehaviour
{
    private GameObject player;
    private Environment environment;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        environment = TagNames.GetEnvironment().GetComponent<Environment>();
    }

    public void Transfer(string newMusicName, Vector2 newPlayerPos, Vector2 newCameraPos, bool staticCam)
    {
        environment.Play(newMusicName, true);

        // pozycja gracza
        // pozycja kamery
        // unieruchomienie kamery (jeśli potrzebne)
    }
}
