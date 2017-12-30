using UnityEngine;

public class TagNames : MonoBehaviour
{
    public static string player = "Player";
    public static string environment = "Environment";
    public static string startLevelScreen = "StartLevelScreen";
    public static string gameOverScreen = "GameOverScreen";
    public static string solidBlockContainer = "SolidBlockContainer";
    public static string gameController = "GameController";
    public static string block = "Block";
    public static string enemy = "Enemy";
    public static string item = "Item";
    public static string coin = "Coin";
    public static string pipe = "Pipe";
    public static string background = "Background";
    public static string mainCamera = "MainCamera";

    public static GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag(player);
    }

    public static GameObject GetEnvironment()
    {
        return GameObject.FindGameObjectWithTag(environment);
    }

    public static GameObject GetStartLevelScreen()
    {
        return GameObject.FindGameObjectWithTag(startLevelScreen);
    }

    public static GameObject GetGameOverScreen()
    {
        return GameObject.FindGameObjectWithTag(gameOverScreen);
    }

    public static GameObject GetSolidBlockContainer()
    {
        return GameObject.FindGameObjectWithTag(solidBlockContainer);
    }

    public static GameObject GetGameController()
    {
        return GameObject.FindGameObjectWithTag(gameController);
    }

    public static GameObject GetBlock()
    {
        return GameObject.FindGameObjectWithTag(block);
    }

    public static GameObject[] GetBlocks()
    {
        return GameObject.FindGameObjectsWithTag(block);
    }

    public static GameObject GetEnemy()
    {
        return GameObject.FindGameObjectWithTag(enemy);
    }

    public static GameObject[] GetEnemies()
    {
        return GameObject.FindGameObjectsWithTag(enemy);
    }

    public static GameObject GetItem()
    {
        return GameObject.FindGameObjectWithTag(item);
    }

    public static GameObject[] GetItems()
    {
        return GameObject.FindGameObjectsWithTag(item);
    }

    public static GameObject GetCoin()
    {
        return GameObject.FindGameObjectWithTag(coin);
    }

    public static GameObject[] GetCoins()
    {
        return GameObject.FindGameObjectsWithTag(coin);
    }

    public static GameObject GetPipe()
    {
        return GameObject.FindGameObjectWithTag(pipe);
    }

    public static GameObject[] GetPipes()
    {
        return GameObject.FindGameObjectsWithTag(pipe);
    }

    public static GameObject GetBackground()
    {
        return GameObject.FindGameObjectWithTag(background);
    }

    public static GameObject[] GetBackgrounds()
    {
        return GameObject.FindGameObjectsWithTag(background);
    }

    public static GameObject GetMainCamera()
    {
        return GameObject.FindGameObjectWithTag(mainCamera);
    }

}
