using UnityEngine;

public class QuestionBlock : SolidIfHit
{
    public Transform coinSpawn;
    public GameObject coinFromBlock;

    protected override void HitBehaviour(bool hitOnAwake)
    {
        SpawnCoin(coinSpawn, coinFromBlock);
        base.HitBehaviour(hitOnAwake);
    }

    static public void SpawnCoin(Transform coinSpawn, GameObject coinFromBlock)
    {
        Instantiate(coinFromBlock, coinSpawn.position, Quaternion.identity);
    }

}
