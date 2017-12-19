using UnityEngine;

public class TagNames : MonoBehaviour
{
    public static string player = "Player";
    public static string environment = "Environment";
    public static string startLevelScreen = "StartLevelScreen";
    public static string gameOverScreen = "GameOverScreen";
    public static string solidBlockContainer = "SolidBlockContainer";
    public static string gameController = "GameController";

    public static string enemy = "Enemy";

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

}
