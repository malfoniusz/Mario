using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform[] entrances;
    public AudioSource audioPipeEnter;
    public Transform playerNewPos;
    public Transform cameraNewPos;

    private GameObject player;
    private PlayerMovement playerMov;
    private TransferScreen transferScreen;
    private MoveObject moveObject;
    private Vector2 MOVE_DISTANCE = new Vector2(0f, -32f);
    private float MOVE_SPEED_MULTIPLIER = 1f;
    private bool enteringPipe = false;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        playerMov = player.GetComponent<PlayerMovement>();
        transferScreen = TagNames.GetMainCamera().GetComponent<TransferScreen>();
    }

    private void Update()
    {
        if (enteringPipe) EnterPipe();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enteringPipe == false && collision.gameObject.tag == TagNames.player)
        {
            bool playerOnEntrance = Contact.ContactPoints(entrances, LayerNames.GetPlayer());
            if (playerOnEntrance && ButtonNames.DownPressed())
            {
                AwakeEnterPipe();
            }
        }
    }

    private void AwakeEnterPipe()
    {
        enteringPipe = true;
        audioPipeEnter.Play();
        moveObject = new MoveObject(player.transform.position, MOVE_DISTANCE, MOVE_SPEED_MULTIPLIER);

        playerMov.DisablePlayer(true);
    }

    private void EnterPipe()
    {
        player.transform.position = moveObject.NextPosition();

        if (moveObject.ReachedEnd())
        {
            enteringPipe = false;
            transferScreen.Transfer(MusicNames.underground, playerNewPos.position, cameraNewPos.position, true);
        }
    }

}
