using UnityEngine;

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
    private Vector2 MOVE_DISTANCE = new Vector2(0f, -32f);
    private float MOVE_SPEED_MULTIPLIER = 1f;
    private bool moveIntoPipe = false;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        playerMov = player.GetComponent<PlayerMovement>();
        transferScreen = TagNames.GetMainCamera().GetComponent<TransferScreen>();
    }

    private void Update()
    {
        if (moveIntoPipe) EnterPipeAnim();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (moveIntoPipe == false && collision.gameObject.tag == TagNames.player)
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
        moveIntoPipe = true;
        audioPipeEnter.Play();
        moveObject = new MoveObject(player.transform.position, MOVE_DISTANCE, MOVE_SPEED_MULTIPLIER);

        playerMov.DisablePlayer(true);
    }

    private void EnterPipeAnim()
    {
        player.transform.position = moveObject.NextPosition();

        if (moveObject.ReachedEnd())
        {
            moveIntoPipe = false;
            transferScreen.Transfer(MusicNames.underground, playerNewPos.position, cameraNewPos.position, ColorNames.underground, staticCamOnExit);
            ExitPipeAnim();
            playerMov.DisablePlayer(false);
        }
    }

    private void ExitPipeAnim()
    {
        // TODO: dwa warianty: zrzucenie gracza lub wyjście z rury (mały/duży mario) (transferScreen już umieści gracza w środku rury)
    }

}
