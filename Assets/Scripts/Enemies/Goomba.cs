using UnityEngine;

public class Goomba : Enemy
{
    protected override void EnemyStompedBehaviour()
    {
        anim.SetTrigger("IsDead");
        audioSource.Play();
        DisableObject();
    }

}
