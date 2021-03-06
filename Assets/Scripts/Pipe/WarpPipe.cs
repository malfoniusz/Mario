﻿using UnityEngine;
using System.Collections;

public class WarpPipe : MonoBehaviour
{
    public Transform[] entrances;
    public AudioSource audioPipeEnter;
    public Direction enterDirection;
    public float enterDistanceValue = 40f;
    public Transform newPlayerPos;
    public bool newCameraPosOnPlayer = true;
    public Transform newCameraPos;
    public ColorEnum exitBackground;
    public MusicEnum exitMusic;
    public bool staticCamOnExit;
    public bool exitPipeAnim;
    public Direction exitDirection;

    private GameObject player;
    private PlayerMovement playerMov;
    private GameObject cam;
    private TransferScreen transferScreen;
    private ActiveObjects activeObjects;
    private MoveObject moveObject;
    private float ENTER_PIPE_TIME_IN_SECONDS = 1f;
    private float EXIT_PIPE_DISTANCE = 100f;
    private float EXIT_PIPE_TIME_IN_SECONDS = 2f;
    private Vector2 enterDistance;
    private Vector2 exitDistance;
    private bool enteringPipe = false;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        playerMov = player.GetComponent<PlayerMovement>();
        cam = TagNames.GetMainCamera();
        transferScreen = cam.GetComponent<TransferScreen>();
        activeObjects = cam.GetComponent<ActiveObjects>();
        enterDistance = CalcDistance(enterDirection, enterDistanceValue);
        exitDistance = CalcDistance(exitDirection, EXIT_PIPE_DISTANCE);
    }

    private Vector2 CalcDistance(Direction direction, float distance)
    {
        Vector2 enterDirection = Vector2.zero;

        if (direction == Direction.Up)
        {
            enterDirection = new Vector2(0, distance);
        }
        else if (direction == Direction.Down)
        {
            enterDirection = new Vector2(0, -distance);
        }
        else if (direction == Direction.Left)
        {
            enterDirection = new Vector2(-distance, 0);
        }
        else if (direction == Direction.Right)
        {
            enterDirection = new Vector2(distance, 0);
        }

        return enterDirection;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enteringPipe == false && collision.gameObject.tag == TagNames.player)
        {
            bool playerOnEntrance = Contact.ContactPoints(entrances, LayerNames.GetPlayer());
            bool enterButtonHold =
                (enterDirection == Direction.Up && ButtonNames.UpHeld()) ||
                (enterDirection == Direction.Down && ButtonNames.DownHeld()) ||
                (enterDirection == Direction.Left && ButtonNames.LeftHeld()) ||
                (enterDirection == Direction.Right && ButtonNames.RightHeld());

            if (playerOnEntrance && enterButtonHold)
            {
                EnterPipe();
            }
        }
    }

    private void EnterPipe()
    {
        enteringPipe = true;
        audioPipeEnter.Play();
        moveObject = MoveObject.CreateMoveObject1(player.transform.position, enterDistance, ENTER_PIPE_TIME_IN_SECONDS);

        playerMov.DisablePlayer(true, true);

        StartCoroutine(EnterPipeAnim());
    }

    private IEnumerator EnterPipeAnim()
    {
        while (true)
        {
            player.transform.position = moveObject.NextPosition();

            if (moveObject.ReachedEnd())
            {
                ExitingPipe();
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    private void ExitingPipe()
    {
        activeObjects.SetStopDisabling(true);

        if (newCameraPosOnPlayer)   transferScreen.Transfer(exitMusic, newPlayerPos.position, ColorNames.GetColor(exitBackground), staticCamOnExit);
        else                        transferScreen.Transfer(exitMusic, newPlayerPos.position, newCameraPos.position, ColorNames.GetColor(exitBackground), staticCamOnExit);

        if (exitPipeAnim)
        {
            StartCoroutine(ExitPipeAnim());
            return;
        }

        FinishExit();
    }

    private IEnumerator ExitPipeAnim()
    {
        moveObject = MoveObject.CreateMoveObject1(player.transform.position, exitDistance, EXIT_PIPE_TIME_IN_SECONDS);
        Transform[] playerGroundChecks = player.GetComponent<PlayerMovement>().groundChecks;

        while (true)
        {
            player.transform.position = moveObject.NextPosition();
            bool standingInPipe = Contact.ContactPoints(playerGroundChecks, LayerNames.GetPipe());

            if (!standingInPipe)
            {
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        FinishExit();
    }

    private void FinishExit()
    {
        enteringPipe = false;
        playerMov.DisablePlayer(false, false);
        activeObjects.SetStopDisabling(false);
    }

}
