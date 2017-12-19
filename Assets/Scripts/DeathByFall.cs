using UnityEngine;

public class DeathByFall : MonoBehaviour
{
    private PlayerDeath playerDeath;

    private void Awake()
    {
        playerDeath = TagNames.GetPlayer().GetComponent<PlayerDeath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagNames.player)
        {
            playerDeath.Die();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

}
