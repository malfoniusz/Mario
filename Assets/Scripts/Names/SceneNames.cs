﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNames : MonoBehaviour
{
    public static string startMenu = "StartMenu";
    public static string level1_1 = "Level 1-1";

    public static void LoadStartMenu()
    {
        SceneManager.LoadScene(startMenu);
    }

    public static void LoadLevel1_1()
    {
        SceneManager.LoadScene(level1_1);
    }

}
