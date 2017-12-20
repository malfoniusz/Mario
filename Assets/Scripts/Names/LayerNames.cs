using UnityEngine;

public class LayerNames : MonoBehaviour
{
    public static string ground = "Ground";
    public static string coin = "Coin";
    public static string player = "Player";
    public static string enemy = "Enemy";
    public static string fireball = "Fireball";
    public static string item = "Item";
    public static string pipe = "Pipe";

    public static int GetGround()
    {
        return LayerMask.NameToLayer(ground);
    }

    public static int GetCoin()
    {
        return LayerMask.NameToLayer(coin);
    }

    public static int GetPlayer()
    {
        return LayerMask.NameToLayer(player);
    }

    public static int GetEnemy()
    {
        return LayerMask.NameToLayer(enemy);
    }

    public static int GetFireball()
    {
        return LayerMask.NameToLayer(fireball);
    }

    public static int GetItem()
    {
        return LayerMask.NameToLayer(item);
    }

    public static int GetPipe()
    {
        return LayerMask.NameToLayer(pipe);
    }

}
