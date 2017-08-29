using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject block;
    public Transform[] bottomHitChecks;
    public float soundDelay = 0.1f;

    protected AudioSource audioSource;
    protected bool playerHit;

    private int playerMask;
    private float soundTime = 0;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerMask = LayerMask.NameToLayer("Player");
    }

    protected virtual void Update()
    {
        Sound();
    }

    protected void Sound()
    {
        soundTime += Time.deltaTime;

        playerHit = PlayerHit();
        if (playerHit && soundTime > soundDelay)
        {
            soundTime = 0;
            audioSource.Play();
        }
    }

    bool PlayerHit()
    {
        bool playerHit = false;
        for (int i = 0; i < bottomHitChecks.Length; i++)
        {
            playerHit = Physics2D.Linecast(block.transform.position, bottomHitChecks[i].position, 1 << playerMask);
            if (playerHit)
            {
                break;
            }
        }

        return playerHit;
    }

}
