using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour
{
    public Transform[] entrances;
    public AudioSource audioPipeEnter;
    public Transform playerNewPos;
    public Transform cameraNewPos;
    public bool staticCamOnExit;

    public enum ButtonDir { Up, Down, Left, Right };
    public ButtonDir enterButton;

    private GameObject player;
    private PlayerMovement playerMov;
    private TransferScreen transferScreen;
    private MoveObject moveObject;
    private float MOVE_DISTANCE = 32f;
    private float MOVE_SPEED_MULTIPLIER = 1f;
    private Vector2 enterDirection;
    private bool enteringPipe = false;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        playerMov = player.GetComponent<PlayerMovement>();
        transferScreen = TagNames.GetMainCamera().GetComponent<TransferScreen>();
        enterDirection = CalcEnterDirection();
    }

    private Vector2 CalcEnterDirection()
    {
        Vector2 enterDirection = Vector2.zero;

        if (enterButton == ButtonDir.Up)
        {
            enterDirection = new Vector2(0, MOVE_DISTANCE);
        }
        else if (enterButton == ButtonDir.Down)
        {
            enterDirection = new Vector2(0, -MOVE_DISTANCE);
        }
        else if (enterButton == ButtonDir.Left)
        {
            enterDirection = new Vector2(-MOVE_DISTANCE, 0);
        }
        else if (enterButton == ButtonDir.Right)
        {
            enterDirection = new Vector2(MOVE_DISTANCE, 0);
        }

        return enterDirection;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enteringPipe == false && collision.gameObject.tag == TagNames.player)
        {
            bool playerOnEntrance = Contact.ContactPoints(entrances, LayerNames.GetPlayer());
            bool enterButtonHold =
                (enterButton == ButtonDir.Up && ButtonNames.UpHeld()) ||
                (enterButton == ButtonDir.Down && ButtonNames.DownHeld()) ||
                (enterButton == ButtonDir.Left && ButtonNames.LeftHeld()) ||
                (enterButton == ButtonDir.Right && ButtonNames.RightHeld());

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
        moveObject = new MoveObject(player.transform.position, enterDirection, MOVE_SPEED_MULTIPLIER);

        playerMov.DisablePlayer(true);

        StartCoroutine(EnterPipeAnim());
    }

    private IEnumerator EnterPipeAnim()
    {
        while (true)
        {
            player.transform.position = moveObject.NextPosition();

            if (moveObject.ReachedEnd())
            {
                PipedEntered();
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void PipedEntered()
    {
        enteringPipe = false;
        // Zatrzymać automatyczne odświeżanie ActiveObjects
        transferScreen.Transfer(MusicNames.underground, playerNewPos.position, cameraNewPos.position, ColorNames.underground, staticCamOnExit);
        // Windowanie gracza
        playerMov.DisablePlayer(false);
        // Wznowić automatyczne odświeżanie ActiveObjects

        // Problem z animacją wyjścia: po zmianie kamery obiekt przestaje być aktywny i reszta skryptu nie może być wykonana
        // Rozwiązanie: zatrzymać automatyczne odświeżanie ActiveObejcts przed wejściem i wznowić je po skończeniu skryptu
        //              po przeniesieniu gracza zacząć go windować w górę do momentu, aż cały sprite będzie widoczny
    }

}
