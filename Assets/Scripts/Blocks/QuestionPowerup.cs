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
        GameObject powerupObject;
        powerupObject = (playerPowerup.level == 1) ? Instantiate(mushroom) : Instantiate(fireFlower);
        powerupObject.transform.GetChild(0).transform.localPosition = transform.position;
    }

}
