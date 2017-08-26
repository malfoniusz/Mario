using UnityEngine;

public class QuestionBlock : BlockAnimated
{
    public Transform coinSpawn;
    public GameObject coinFromBlock;
    public Sprite solidBlock;
    public AudioSource coinAudio;
    public AudioSource blockHitAudio;

    private SpriteRenderer spriteRenderer;
    private Animator animBlock;
    private bool wasHit = false;
    private bool animStooped = false;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = block.GetComponent<SpriteRenderer>();
        animBlock = block.GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (playerHit && wasHit == false)
        {
            wasHit = true;

            animBlock.enabled = false;  // Color change animation
            spriteRenderer.sprite = solidBlock;

            audioSource = blockHitAudio;
            coinAudio.Play();

            GameObject coin = Instantiate(coinFromBlock);
            coin.transform.GetChild(0).transform.localPosition = coinSpawn.position;
        }

        StopAnimation();
    }

    void StopAnimation()
    {
        if (animStooped == false && wasHit == true && animFinished)
        {
            anim.enabled = false;   // Block hit animation
            animStooped = true;
        }
    }

}
