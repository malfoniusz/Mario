using UnityEngine;

public class Brick : BlockAnimated
{
    public GameObject brickBroken;

    private GameObject player;
    private PlayerPowerup playerPowerup;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindGameObjectWithTag("Player");
        playerPowerup = player.GetComponent<PlayerPowerup>();
    }

    protected override void Update()
    {
        base.Update();

        if (playerHit && playerPowerup.level > 1)
        {
            Instantiate(brickBroken, transform.localPosition, Quaternion.identity);
            Destroy(parent);
        }
    }

}
