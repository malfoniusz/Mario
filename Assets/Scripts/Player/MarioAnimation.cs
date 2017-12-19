using UnityEngine;

public class MarioAnimation : MonoBehaviour
{
    public BoxCollider2D marioCollider;
    public BoxCollider2D bigMarioCollider;
    private float colliderDifference;

    private GameController gameController;
    private GameObject player;
    private MarioModel marioModel;

    private void Awake()
    {
        gameController = TagNames.GetGameController().GetComponent<GameController>();
        player = TagNames.GetPlayer();
        marioModel = player.GetComponent<MarioModel>();
    }

    private void Start()
    {
        colliderDifference = (bigMarioCollider.size.y - marioCollider.size.y) / 2;
    }

    void StopGame_Event()
    {
        gameController.StopGame(false);
    }

    void ResumeAndUpdateModel_Event()
    {
        gameController.ResumeGame(false);
        marioModel.UpdateModel();
    }

    void PowerupPositionUp_Event()
    {
        player.transform.position += Vector3.up * colliderDifference;
    }

    void PowerupPositionDown_Event()
    {
        player.transform.position += Vector3.down * colliderDifference;
    }

}
