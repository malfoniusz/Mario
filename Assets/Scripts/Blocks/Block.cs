using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform[] bottomHitChecks;

    protected GameObject parent;
    protected AudioSource audioSource;
    protected bool playerHit;

    private int playerMask;
    private float soundDelay = 0.1f;
    private float soundTime = 0;

    protected virtual void Awake()
    {
        parent = transform.parent.gameObject;
        audioSource = GetComponent<AudioSource>();
        playerMask = LayerMask.NameToLayer("Player");
    }

    protected virtual void Update()
    {
        PlayerHit();
        Sound();
    }

    protected void Sound()
    {
        soundTime += Time.deltaTime;
        if (playerHit && soundTime > soundDelay)
        {
            soundTime = 0;
            audioSource.Play();
        }
    }

    protected void PlayerHit()
    {
        playerHit = false;
        for (int i = 0; i < bottomHitChecks.Length; i++)
        {
            playerHit = Physics2D.Linecast(transform.position, bottomHitChecks[i].position, 1 << playerMask);
            if (playerHit)
            {
                break;
            }
        }
    }

}
