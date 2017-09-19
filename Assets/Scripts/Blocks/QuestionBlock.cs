using UnityEngine;

public class QuestionBlock : BlockTurnSolid
{
    public Transform coinSpawn;
    public GameObject coinFromBlock;

    protected override void HitBehaviour()
    {
        SpawnCoin();
        CreateSolidBlock(true);
        Destroy(parent);
    }

    protected void SpawnCoin()
    {
        GameObject coin = Instantiate(coinFromBlock);
        coin.transform.GetChild(0).transform.localPosition = coinSpawn.position;
    }

}
