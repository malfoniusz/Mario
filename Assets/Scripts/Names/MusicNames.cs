using UnityEngine;

public class MusicNames : MonoBehaviour
{
    public enum Musics
    {
        main,
        hurry,
        invincibility,
        death,
        underground,
    }

    static public string GetMusic(Musics music)
    {
        switch (music)
        {
            case Musics.main:
                return main;
            case Musics.hurry:
                return hurry;
            case Musics.invincibility:
                return invincibility;
            case Musics.death:
                return death;
            case Musics.underground:
                return underground;
            default:
                throw new System.Exception("Chosen music dosen't exist.");
        }
    }

    public static string main = "Main";
    public static string hurry = "Hurry";
    public static string invincibility = "Invincibility";
    public static string death = "Death";
    public static string underground = "Underground";

}
