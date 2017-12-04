using UnityEngine;

public class FireFlower : Mushroom
{
    protected override void Awake()
    {
        base.Awake();
        stayKinematic = true;
    }

    protected override void CollisionBehaviour()
    {
        playerPowerup.FireFlowerPowerup();
        SpawnPoints();
        Destroy(gameObject);
    }

}
