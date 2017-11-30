using UnityEngine;

public class QuestionBlock : BlockTurnSolid
{
    public Transform coinSpawn;
    public GameObject coinFromBlock;

    protected override void HitBehaviour()
    {
        SpawnCoin();
        base.HitBehaviour();
    }

    protected void SpawnCoin()
    {
        Instantiate(coinFromBlock, coinSpawn.position, Quaternion.identity);
    }

}
