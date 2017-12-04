using UnityEngine;

public class QuestionPowerup : SolidIfHit
{
    public GameObject mushroom;
    public GameObject fireFlower;

    private PlayerPowerup playerPowerup;

    protected override void Awake()
    {
        base.Awake();
        playerPowerup = player.GetComponent<PlayerPowerup>();
    }

    protected override void HitBehaviour(bool hitOnAwake)
    {
        SpawnPowerup();
        base.HitBehaviour(false);
    }

    private void SpawnPowerup()
    {
        if (playerPowerup.level == 1) Instantiate(mushroom, transform.position, Quaternion.identity);
        else Instantiate(fireFlower, transform.position, Quaternion.identity);
    }

}
