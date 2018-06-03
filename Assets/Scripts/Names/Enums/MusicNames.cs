using UnityEngine;

public class MusicNames : MonoBehaviour
{
    public static int GetMusicIndex(MusicEnum musicName)
    {
        switch (musicName)
        {
            case MusicEnum.main:
                return 0;
            case MusicEnum.hurry:
                return 1;
            case MusicEnum.invincibility:
                return 2;
            case MusicEnum.death:
                return 3;
            case MusicEnum.underground:
                return 4;
            case MusicEnum.stageCleared:
                return 5;
            case MusicEnum.gameOver:
                return 6;
            default:
                throw new System.Exception("No such music exists.");
        }
    }

}
