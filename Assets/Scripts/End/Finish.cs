using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform spawnPointsPos;
    public AudioSource audioFlagpole;
    public float pointsRiseDistance = 135;
    public float pointsRiseTimeInSeconds = 1;

    private GameController gameController;
    private MusicController musicController;
    private GameObject player;
    private MarioAnimator mAnim;

    private void Awake()
    {
        gameController = TagNames.GetGameController().GetComponent<GameController>();
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        player = TagNames.GetPlayer();
        mAnim = player.GetComponent<MarioAnimator>();
    }

    public IEnumerator ClearStage(int flagpolePoints, bool extraLife)
    {
        FlagpoleTouched(flagpolePoints, extraLife);
        yield return new WaitForSeconds(audioFlagpole.clip.length);

        GoingToCastle();
        yield return null;

        // gameController.ResumeGame(true);  // W razie problemów po wczytaniu kolejnego poziomu odkomentuj to
    }

    private void FlagpoleTouched(int flagpolePoints, bool extraLife)
    {
        SpawnPointsFloating.Flagpole(spawnPointsPos.position, flagpolePoints, extraLife, pointsRiseDistance, pointsRiseTimeInSeconds);

        musicController.PauseCurrentMusic();
        audioFlagpole.Play();

        gameController.StopGame(false);
        mAnim.SetIsGrabbing(true);
    }

    private void GoingToCastle()
    {
        musicController.Play(MusicEnum.stageCleared, true);

        mAnim.SetIsGrabbing(false);
    }

}
