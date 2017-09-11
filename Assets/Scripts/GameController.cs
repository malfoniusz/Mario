using UnityEngine;

public class GameController : MonoBehaviour
{
    public static AudioSource environmentAudio;
    public static int lives = 3;

    void Awake()
    {
        environmentAudio = GameObject.FindGameObjectWithTag("Environment").GetComponent<AudioSource>();
    }

    public static void PlayerDied()
    {
        lives--;
        // Reset level
    }

    public static void StopGame()
    {
        Goomba.stop = true;
        UITime.stop = true;
        environmentAudio.Stop();
    }

}
