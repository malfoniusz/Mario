using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform spawnPointsPos;
    public AudioSource audioFlagpole;
    public float pointsRiseDistance = 135;
    public float pointsRiseTimeInSeconds = 1;

    private MusicController musicController;

    private void Start()
    {
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
    }

    public IEnumerator ClearStage(int flagpolePoints, bool extraLife)
    {
        SpawnPointsFloating.Flagpole(spawnPointsPos.position, flagpolePoints, extraLife, pointsRiseDistance, pointsRiseTimeInSeconds);

        musicController.PauseCurrentMusic();
        audioFlagpole.Play();

        yield return new WaitForSeconds(audioFlagpole.clip.length);

        musicController.Play(MusicNames.stageCleared, true);

        yield return null;
    }

}
