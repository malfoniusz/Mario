using UnityEngine;

public class QuestionBlock : BlockAnimated
{
    public Transform coinSpawn;
    public GameObject coinFromBlock;
    public GameObject solidBlock;

    protected GameObject solidContainer;

    protected override void Awake()
    {
        base.Awake();
        solidContainer = GameObject.FindWithTag("SolidBlockContainer");
    }

    protected override void Update()
    {
        PlayerHit();
        Sound();

        if (playerHit)
        {
            SpawnCoin();
            CreateSolidBlock();
            Destroy(parent);
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

}
