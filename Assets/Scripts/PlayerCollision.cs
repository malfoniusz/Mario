using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private int playerMask;
    private int enemyMask;
    private int enemyTriggerMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerMask = LayerMask.NameToLayer("Player");
        enemyMask = LayerMask.NameToLayer("Enemy");
        enemyTriggerMask = LayerMask.NameToLayer("EnemyTrigger");
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(playerMask, enemyMask);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyTriggerMask)
        {
            // Player jumped on enemy - check top collision

            //rb.AddForce(Vector2.up * 40000);

            //Debug.Log("TOP");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyTriggerMask)
        {
            // Player touched enemy or stays in enemy - hurt player

            // Debug.Log("NOT TOP");
        }
    }
}
