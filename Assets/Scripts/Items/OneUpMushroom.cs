using UnityEngine;

public class OneUpMushroom : Mushroom
{
    protected override void CollisionBehaviour()
    {
        SpawnPointsAndExtraLife();
        Destroy(gameObject);
    }

}
