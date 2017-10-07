using UnityEngine;
using System.Collections;

public class Goomba : Enemy
{
    private const float DESTROY_DELAY = 0.5f;

    protected override void EnemyStompedBehaviour()
    {
        anim.SetTrigger("IsDead");
        audioSource.Play();
        DisableObject(true);

        StartCoroutine(Destruction.DelayedDestroy(DESTROY_DELAY, parent));
    }

}
