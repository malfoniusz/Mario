using UnityEngine;

public class StarPowerup : Mushroom
{
    protected override void CollisionBehaviour()
    {
        GrantInvincibility();

        SpawnPoints();
        Destroy(gameObject);
    }

    private void GrantInvincibility()
    {

    }

}
