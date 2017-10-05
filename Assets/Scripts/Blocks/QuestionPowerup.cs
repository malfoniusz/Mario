using UnityEngine;

public class QuestionPowerup : BlockTurnSolid
{
    public GameObject mushroom;
    public GameObject fireFlower;

    private GameObject player;
    private PlayerPowerup playerPowerup;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerPowerup = player.GetComponent<PlayerPowerup>();
    }

    protected override void HitBehaviour()
    {
        SpawnPowerup();
        CreateSolidBlock(false);
        Destroy(parent);
    }

    private void SpawnPowerup()
    {
        GameObject powerupObject;
        powerupObject = (playerPowerup.level == 1) ? Instantiate(mushroom) : Instantiate(fireFlower);
        powerupObject.transform.GetChild(0).transform.localPosition = transform.position;
    }

}
