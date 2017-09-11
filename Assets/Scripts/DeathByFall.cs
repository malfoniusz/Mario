using UnityEngine;

public class DeathByFall : MonoBehaviour
{
    public PlayerDeath playerDeath;

    void Awake()
    {
        playerDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDeath.Die();
        }
    }

}
