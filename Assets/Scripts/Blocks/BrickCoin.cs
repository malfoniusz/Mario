using UnityEngine;

public class BrickCoin : BlockAnimated
{
    public GameObject solidBlock;
    public Transform coinSpawn;
    public GameObject coinFromBlock;

    public int coinNumber = 10;
    public float hitDelay = 0.1f;

    private float hitTime = 0;

    protected override void Update()
    {
        base.Update();
        CoinsFromBlock();
    }

    private void CoinsFromBlock()
    {
        hitTime += Time.deltaTime;

        if (playerHit && hitTime > hitDelay)
        {
            hitTime = 0;

            QuestionBlock.SpawnCoin(coinSpawn, coinFromBlock);
            coinNumber--;

            if (coinNumber == 0)
            {
                SolidIfHit.CreateSolidBlock(true, transform.position, solidBlock);
                Destroy(gameObject);
            }
        }
    }

}
