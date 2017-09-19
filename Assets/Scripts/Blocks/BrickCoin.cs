using UnityEngine;

public class BrickCoin : QuestionBlock
{
    public int coinNumber = 10;
    public float hitDelay = 0.1f;

    private float hitTime = 0;

    protected override void Update()
    {
        PlayerHit();
        Animation();

        hitTime += Time.deltaTime;

        if (playerHit && hitTime > hitDelay)
        {
            hitTime = 0;

            SpawnCoin();
            coinNumber--;
            
            if (coinNumber == 0)
            {
                CreateSolidBlock(true);
                Destroy(parent);
            }
        }
    }

}
