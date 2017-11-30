using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform[] bottomHitChecks;

    protected GameObject parent;
    protected GameObject player;
    protected AudioSource audioSource;
    protected bool playerHit = false;

    private Rigidbody2D playerRB;
    private int playerMask;
    private float soundDelay = 0.1f;
    private float soundTime = 0;

    protected virtual void Awake()
    {
        parent = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerRB = player.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        playerMask = LayerMask.NameToLayer("Player");
    }

    protected virtual void Update()
    {
        playerHit = PlayerHit();
        Sound(playerHit);
    }

    protected void Sound(bool playerHit)
    {
        soundTime += Time.deltaTime;
        if (playerHit && soundTime > soundDelay)
        {
            soundTime = 0;
            audioSource.Play();
        }
    }

    protected bool PlayerHit()
    {
        for (int i = 0; i < bottomHitChecks.Length; i++)
        {
            bool playerCollided = Physics2D.Linecast(transform.position, bottomHitChecks[i].position, 1 << playerMask);
            float playerYSpeed = playerRB.velocity.y;

            if (playerCollided && playerYSpeed >= 0)
            {
                return true;
            }
        }

        return false;
    }

}
