using UnityEngine;

public class Koopa : Goomba
{
    public GameObject koopaShell;

    private const float SHELL_OFFSET = 4.5f;

    protected override void Awake()
    {
        base.Awake();
        MINIMAL_HEIGHT_BIG = 13f;
    }

    protected override void EnemyStomped(Collider2D collision)
    {
        Vector2 shellPos = new Vector2(transform.position.x, transform.position.y - SHELL_OFFSET);
        Instantiate(koopaShell, shellPos, transform.rotation);
        Destroy(gameObject);
    }

}
