using UnityEngine;

public class CoinFromBlock : Coin
{
    public Animator animCoinJump;
    public GameObject pointsFloating;

    private GameObject parent;
    private bool extraLife;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    void Awake()
    {
        parent = transform.parent.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        extraLife = UICoins.AddCoin();
    }

    void Update()
    {
        if (animCoinJump.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (spriteRenderer.enabled)
            {
                GameObject pointsObject = Instantiate(pointsFloating);
                pointsObject.transform.GetChild(0).position = transform.position;
                pointsObject.GetComponent<PointsFloating>().SetPoints(points, extraLife);
                spriteRenderer.enabled = false;
            }

            if (audioSource.isPlaying == false)
            {
                Destroy(parent);
            }
        }
    }

}
