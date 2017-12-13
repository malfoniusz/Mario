using UnityEngine;

public class StarBlock : SolidIfHit
{
    public GameObject star;

    protected override void HitBehaviour(bool hitOnAwake)
    {
        SpawnStar();
        base.HitBehaviour(false);
    }

    private void SpawnStar()
    {
        Instantiate(star, transform.position, Quaternion.identity);
    }
}
