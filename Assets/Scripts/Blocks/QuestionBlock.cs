using UnityEngine;
using System.Collections;

public class QuestionBlock : BlockAnimated
{
    public Transform coinSpawn;
    public GameObject coinFromBlock;
    public GameObject solidBlock;

    protected SpriteRenderer spriteRenderer;
    protected GameObject solidContainer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        solidContainer = GameObject.FindWithTag("SolidBlockContainer");
    }

    protected override void Update()
    {
        Sound();

        if (playerHit)
        {
            SpawnCoin();
            CreateSolidBlock();
            Hide();

            audioSource.Play();
            StartCoroutine(WaitDestroy(audioSource.clip.length));
        }
    }

    protected void SpawnCoin()
    {
        GameObject coin = Instantiate(coinFromBlock);
        coin.transform.GetChild(0).transform.localPosition = coinSpawn.position;
    }

    protected void CreateSolidBlock()
    {
        GameObject solid = Instantiate(solidBlock);
        solid.transform.GetChild(0).transform.localPosition = transform.position;
        solid.transform.parent = solidContainer.transform;
    }

    protected void Hide()
    {
        spriteRenderer.enabled = false;
        enabled = false;
    }

    protected IEnumerator WaitDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
