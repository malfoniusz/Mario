using UnityEngine;

public class CoinFromBlock : MonoBehaviour
{
    public int points = 200;
    public float jumpForce = 400;

    private bool extraLife;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private float destroyJumpSwitch;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        extraLife = UICoins.AddCoin();
        rb.velocity = new Vector2(0, jumpForce);
        destroyJumpSwitch = -jumpForce * ((float)4/5);
    }

    void Update()
    {
        if (rb.velocity.y < destroyJumpSwitch)
        {
            if (spriteRenderer.enabled)
            {
                SpawnPointsFloating.Points(transform.position, points, extraLife);
                spriteRenderer.enabled = false;
            }

            if (audioSource.isPlaying == false)
            {
                Destroy(gameObject);
            }
        }
    }

}
