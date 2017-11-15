using UnityEngine;

public class DeathByFall : MonoBehaviour
{
    private PlayerDeath playerDeath;

    private void Awake()
    {
        playerDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDeath.Die();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

}
