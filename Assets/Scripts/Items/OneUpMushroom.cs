using UnityEngine;

public class OneUpMushroom : Mushroom
{
    protected override void CollisionBehaviour()
    {
        PointsSpawn(true);
        Destroy(parent);
    }

}
