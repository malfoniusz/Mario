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
        Instantiate(coinFromBlock, coinSpawn.position, Quaternion.identity);
    }

}
