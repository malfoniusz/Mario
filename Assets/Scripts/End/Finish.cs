using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform spawnPointsPos;
    public GameObject flagpoleBase;
    public Transform flagpoleBaseTopContact;
    public GameObject flag;
    public AudioSource audioFlagpole;
    public float pointsRiseDistance = 135;
    public float pointsRiseTimeInSeconds = 1;
    public float marioSlideSpeed = 150;
    public float flagSlideSpeed = 150;

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

        StartCoroutine(SlideDown(player, marioSlideSpeed));
        StartCoroutine(SlideDown(flag, flagSlideSpeed));
    }

    private IEnumerator SlideDown(GameObject movingObject, float moveSpeed)
    {
        Vector2 startPos = movingObject.transform.position;
        Vector2 endPos = new Vector2(movingObject.transform.position.x, flagpoleBase.transform.position.y);

        MoveObject moveObj = MoveObject.CreateMoveObject3(startPos, endPos, moveSpeed);
        Collider2D objCollider = movingObject.GetComponent<Collider2D>();

        while (objCollider.bounds.Contains(flagpoleBaseTopContact.position) == false)
        {
            movingObject.transform.position = moveObj.NextPosition();
            yield return Time.deltaTime;
        }

        yield return null;
    }

    private void GoingToCastle()
    {
        musicController.Play(MusicEnum.stageCleared, true);

        mAnim.SetIsGrabbing(false);
    }

}
