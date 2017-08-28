using UnityEngine;
using System.Collections;

public class QuestionBlock : BlockAnimated
{
    public Transform coinSpawn;
    public GameObject coinFromBlock;
    public GameObject solidBlock;

    private SpriteRenderer spriteRenderer;
    private GameObject solidContainer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = block.GetComponent<SpriteRenderer>();
        solidContainer = GameObject.FindWithTag("SolidBlockContainer");
    }

    protected override void Update()
    {
        base.Update();

        if (playerHit)
        {
            GameObject coin = Instantiate(coinFromBlock);
            coin.transform.GetChild(0).transform.localPosition = coinSpawn.position;

            GameObject solid = Instantiate(solidBlock);
            solid.transform.GetChild(0).transform.localPosition = block.transform.position;
            solid.transform.parent = solidContainer.transform;

            spriteRenderer.enabled = false;
            enabled = false;

            audioSource.Play();
            StartCoroutine(WaitDestroy(audioSource.clip.length));
        }
    }

    IEnumerator WaitDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
