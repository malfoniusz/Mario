using UnityEngine;

public class BrickCoin : QuestionBlock
{
    public int coinNumber = 10;
    public float hitDelay = 0.1f;

    private float time = 0;

    protected override void Update()
    {
        Sound();
        Animation();

        time += Time.deltaTime;

        if (playerHit && time > hitDelay)
        {
            time = 0;

            SpawnCoin();
            coinNumber--;
            
            if (coinNumber == 0)
            {
                CreateSolidBlock();
                Hide();
                
                audioSource.Play();
                StartCoroutine(WaitDestroy(audioSource.clip.length));
            }
        }
    }

}
