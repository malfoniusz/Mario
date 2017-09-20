using UnityEngine;

public class Koopa : Enemy
{
    public GameObject koopaShell;

    private const float SHELL_OFFSET = 4.5f;

    protected override void EnemyStompedBehaviour()
    {
        Vector2 shellPos = new Vector2(transform.position.x, transform.position.y - SHELL_OFFSET);
        GameObject shellObject = Instantiate(koopaShell);
        shellObject.transform.GetChild(0).position = shellPos;
        Destroy(parent);
    }

    protected override void ChangeDirectionBehaviour()
    {
        base.ChangeDirectionBehaviour();
        spriteRenderer.flipX = (Mathf.Sign(rb.velocity.x) == 1);
    }

}
