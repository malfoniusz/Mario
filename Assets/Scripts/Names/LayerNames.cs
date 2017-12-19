using UnityEngine;

public class LayerNames : MonoBehaviour
{
    public static string player = "Player";
    public static string enemy = "Enemy";
    public static string ground = "Ground";

    public static int GetPlayer()
    {
        return LayerMask.NameToLayer(player);
    }

    public static int GetEnemy()
    {
        return LayerMask.NameToLayer(enemy);
    }

    public static int GetGround()
    {
        return LayerMask.NameToLayer(ground);
    }

}
