using UnityEngine;

public class FireFlower : Mushroom
{
    protected override void MovingBehaviour()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && activated == false)
        {
            activated = true;
        }
    }

    protected override void CollisionEnter(Collider2D collision)
    {
        if (activated)
        {
            playerPowerup.FireFlowerPowerup();
            SpawnPoints();
            Destroy(parent);
        }
    }

}
