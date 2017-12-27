using UnityEngine;

public class UndergroundCoin : MonoBehaviour
{
    public int points = 200;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private const float DESTOY_DELAY = 2f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false;

            audioSource.Play();
            UICoins.AddCoin();
            UIPoints.AddPoints(points);

            StartCoroutine(Destruction.DelayedDestroy(DESTOY_DELAY, gameObject));
        }
    }

}
