﻿using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform spawnPointsPos;
    public GameObject flagpoleBase;
    public Transform flagpoleBaseTopContact;
    public GameObject pole;
    public GameObject flag;
    public Castle castle;
    public AudioSource audioFlagpole;
    public AudioSource audioTimeToPoints;
    public float pointsRiseDistance = 135;
    public float pointsRiseTimeInSeconds = 1;
    public float marioSlideSpeed = 150;
    public float flagSlideSpeed = 130;
    public float playerCastleMovSpeed = 70f;
    public float intervalTimeToPointsInSeconds = 0.01f;

    private GameController gameController;
    private MusicController musicController;
    private GameObject player;
    private PlayerMovement playerMove;
    private MarioAnimator mAnim;
    private UITime uiTime;
    private bool enteredCastle = false;

    private void Awake()
    {
        gameController = TagNames.GetGameController().GetComponent<GameController>();
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        player = TagNames.GetPlayer();
        playerMove = player.GetComponent<PlayerMovement>();
        mAnim = player.GetComponent<MarioAnimator>();
        uiTime = TagNames.GetUITime();
    }

    public void ClearStage(int flagpolePoints, bool extraLife)
    {
        StartCoroutine(ClearStageCoroutine(flagpolePoints, extraLife));
    }

    private IEnumerator ClearStageCoroutine(int flagpolePoints, bool extraLife)
    {
        FlagpoleTouched(flagpolePoints, extraLife);
        yield return new WaitForSeconds(audioFlagpole.clip.length);

        GoingToCastle();
        yield return null;
    }

    private void FlagpoleTouched(int flagpolePoints, bool extraLife)
    {
        SpawnPointsFloating.Flagpole(spawnPointsPos.position, flagpolePoints, extraLife, pointsRiseDistance, pointsRiseTimeInSeconds);

        musicController.PauseCurrentMusic();
        audioFlagpole.Play();

        gameController.StopGame(false);
        playerMove.ResetVelocity();
        mAnim.SetIsGrabbing(true);

        StartCoroutine(SlideDown(player, marioSlideSpeed, true));
        StartCoroutine(SlideDown(flag, flagSlideSpeed, false));
    }

    private IEnumerator SlideDown(GameObject movingObject, float moveSpeed, bool isPlayer)
    {
        Vector2 startPos = movingObject.transform.position;
        Vector2 endPos = new Vector2(movingObject.transform.position.x, flagpoleBase.transform.position.y);

        MoveObject moveObj = MoveObject.CreateMoveObject4(startPos, endPos, moveSpeed);
        Collider2D objCollider = movingObject.GetComponent<Collider2D>();

        while (objCollider.bounds.Contains(flagpoleBaseTopContact.position) == false)
        {
            movingObject.transform.position = moveObj.NextPosition();
            yield return new WaitForSeconds(0.01f);
        }

        if (isPlayer)
        {
            MoveToOtherSideOfPole(movingObject);
        }

        yield return null;
    }

    private void MoveToOtherSideOfPole(GameObject movingObject)
    {
        float xDifference = pole.transform.position.x - movingObject.transform.position.x;
        float xMirror = 2 * xDifference;

        movingObject.transform.position += Vector3.right * xMirror;
        movingObject.transform.localScale = new Vector3(-1 * movingObject.transform.localScale.x, 1, 1);

        CameraFollow camFollow = TagNames.GetCameraObject().GetComponent<CameraFollow>();
        camFollow.UpdateMinDistanceToCurrent();
    }

    private void GoingToCastle()
    {
        musicController.Play(MusicEnum.stageCleared, true);

        mAnim.SetIsGrabbing(false);
        player.transform.localScale = new Vector3(-1 * player.transform.localScale.x, 1, 1);

        playerMove.IsKinematic(false);
        playerMove.Stop(false);
        playerMove.SetConstantMove(true, Direction.Right, playerCastleMovSpeed);

        StartCoroutine(TimeToPoints());
    }

    public void EnteredCastle()
    {
        player.SetActive(false);
        enteredCastle = true;
    }

    private IEnumerator TimeToPoints()
    {
        yield return new WaitForSeconds(1f);

        audioTimeToPoints.Play();

        while (uiTime.GetTime() > 0)
        {
            uiTime.PointsForTimeUnit();
            yield return new WaitForSeconds(intervalTimeToPointsInSeconds);
        }

        audioTimeToPoints.Stop();

        while (!(enteredCastle && !musicController.IsPlaying(MusicEnum.stageCleared)))
        {
            yield return new WaitForSeconds(0.01f);
        }

        castle.ShowCastleFlag();

        yield return null;
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    private IEnumerator NextLevelCoroutine()
    {
        yield return new WaitForSeconds(1f);

        gameController.NextLevel();

        yield return null;
    }

}
