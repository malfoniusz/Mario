using UnityEngine;

public class Brick : BlockAnimated
{
    public GameObject brickBroken;

    private PlayerPowerup playerPowerup;

    protected override void Awake()
    {
        base.Awake();
        playerPowerup = player.GetComponent<PlayerPowerup>();
    }

    protected override void Update()
    {
        base.Update();

        if (playerHit && playerPowerup.GetLevel() > 1)
        {
            Instantiate(brickBroken, transform.localPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
