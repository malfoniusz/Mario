using UnityEngine;
using System.Collections;

public class Koopa : Enemy
{
    public GameObject koopaShell;

    private const float SHELL_OFFSET = 4.5f;
    private const float SPAWN_SHELL_DELAY = 0.02f;

    protected override void EnemyStompedBehaviour()
    {
        StartCoroutine(DelayedBehaviour(SPAWN_SHELL_DELAY));
    }

    protected override void ChangeDirectionBehaviour()
    {
        base.ChangeDirectionBehaviour();
        spriteRenderer.flipX = (Mathf.Sign(rb.velocity.x) == 1);
    }

    IEnumerator DelayedBehaviour(float sec)
    {
        yield return new WaitForSeconds(sec);

        Vector2 shellPos = new Vector2(transform.position.x, transform.position.y - SHELL_OFFSET);
        Instantiate(koopaShell, shellPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
