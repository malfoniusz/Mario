using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int lives = 3;

    public static void PlayerDied()
    {
        lives--;
        // Reset level
    }
}
