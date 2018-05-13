using UnityEngine;

public class TagNames : MonoBehaviour
{
    public static string player = "Player";
    public static string musicController = "MusicController";
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
    public static string environment = "Environment";
    public static string flagpole = "Flagpole";
    public static string castle = "Castle";
    public static string uiTime = "UITime";
    public static string uiPoints = "UIPoints";

    public static GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag(player);
    }

    public static GameObject GetMusicController()
    {
        return GameObject.FindGameObjectWithTag(musicController);
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

    public static GameObject GetEnvironment()
    {
        return GameObject.FindGameObjectWithTag(environment);
    }

    public static GameObject GetFlagpole()
    {
        return GameObject.FindGameObjectWithTag(flagpole);
    }

    public static GameObject[] GetFlagpoles()
    {
        return GameObject.FindGameObjectsWithTag(flagpole);
    }

    public static GameObject GetCastle()
    {
        return GameObject.FindGameObjectWithTag(castle);
    }

    public static GameObject[] GetCastles()
    {
        return GameObject.FindGameObjectsWithTag(castle);
    }

    public static Camera GetCamera()
    {
        return Camera.main;
    }

    public static GameObject GetCameraObject()
    {
        return Camera.main.gameObject;
    }

    public static GameObject GetUITime()
    {
        return GameObject.FindGameObjectWithTag(uiTime);
    }

    public static GameObject GetUIPoints()
    {
        return GameObject.FindGameObjectWithTag(uiPoints);
    }

}
