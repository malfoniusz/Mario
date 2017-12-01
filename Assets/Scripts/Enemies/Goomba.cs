using UnityEngine;

public class Goomba : Enemy
{
    private const float DESTROY_DELAY = 0.5f;

    protected override void EnemyStompedBehaviour()
    {
        anim.SetTrigger("IsDead");
        audioSource.Play();
        DisableObject(true, true);

        StartCoroutine(Destruction.DelayedDestroy(DESTROY_DELAY, parent));
    }

}
