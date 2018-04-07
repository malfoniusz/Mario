using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour
{
    public enum ButtonDir { Up, Down, Left, Right };

    public Transform[] entrances;
    public AudioSource audioPipeEnter;
    public ButtonDir enterButton;
    public Transform newPlayerPos;
    public bool defaultCamPos = false;
    public Transform newCameraPos;
    public ColorNames.Colors exitBackground;
    public bool staticCamOnExit;
    public bool noExitAnim;
    public ButtonDir exitDirection;

    private GameObject player;
    private PlayerMovement playerMov;
    private GameObject cam;
    private TransferScreen transferScreen;
    private ActiveObjects activeObjects;
    private MoveObject moveObject;
    private float MOVE_DISTANCE = 32f;
    private float MOVE_SPEED_MULTIPLIER = 1f;
    private Vector2 enterDirection;
    private Vector2 exitDirectionValue;
    private bool enteringPipe = false;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        playerMov = player.GetComponent<PlayerMovement>();
        cam = TagNames.GetMainCamera();
        transferScreen = cam.GetComponent<TransferScreen>();
        activeObjects = cam.GetComponent<ActiveObjects>();
        enterDirection = CalcEnterDirection(enterButton);
        exitDirectionValue = CalcEnterDirection(exitDirection);
    }

    private Vector2 CalcEnterDirection(ButtonDir btnDir)
    {
        Vector2 enterDirection = Vector2.zero;

        if (btnDir == ButtonDir.Up)
        {
            enterDirection = new Vector2(0, MOVE_DISTANCE);
        }
        else if (btnDir == ButtonDir.Down)
        {
            enterDirection = new Vector2(0, -MOVE_DISTANCE);
        }
        else if (btnDir == ButtonDir.Left)
        {
            enterDirection = new Vector2(-MOVE_DISTANCE, 0);
        }
        else if (btnDir == ButtonDir.Right)
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
                ExitingPipe();
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void ExitingPipe()
    {
        enteringPipe = false;

        // Potrzebny enum do wyboru muzyki i koloru backgroundu
        if (defaultCamPos == false) transferScreen.Transfer(MusicNames.underground, newPlayerPos.position, newCameraPos.position, ColorNames.GetColor(exitBackground), staticCamOnExit);
        else                        transferScreen.Transfer(MusicNames.underground, newPlayerPos.position, ColorNames.GetColor(exitBackground), staticCamOnExit);

        activeObjects.SetStopDisabling(true);

        // Coroutine musi się wykonać przed wykonaniem reszty kodu za if'em
        if (noExitAnim == false)
        {
            BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();

            moveObject = new MoveObject(player.transform.position, exitDirectionValue, MOVE_SPEED_MULTIPLIER);
            // Zmienić nazwę EnterPipeAnim
            StartCoroutine(EnterPipeAnim());

            // Windowanie gracza
        }

        playerMov.DisablePlayer(false);

        activeObjects.SetStopDisabling(false);



        // Problem z animacją wyjścia: po zmianie kamery obiekt przestaje być aktywny i reszta skryptu nie może być wykonana
        // Rozwiązanie: zatrzymać automatyczne odświeżanie ActiveObejcts przed wejściem i wznowić je po skończeniu skryptu
        // 				lub dodać dodatkowy skrypt do gracza: PlayerExitsPipe
        //              po przeniesieniu gracza zacząć go windować w górę do momentu, aż cały sprite będzie widoczny

    }

}
