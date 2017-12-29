using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool quickStart = false;
    public bool playerInvincible = false;

    private GameObject player;
    private PlayerInvincibility invincibility;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        invincibility = player.GetComponent<PlayerInvincibility>();
    }

    private void Start()
    {
        GameController.quickStart = quickStart;
        invincibility.SetInvincible(playerInvincible);
    }

}
