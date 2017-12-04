using UnityEngine;

public class OneUpBlock : SolidIfHit
{
    public GameObject oneUpMushroom;

    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.enabled = false;
    }

    protected override void HitBehaviour(bool hitOnAwake)
    {
        Spawn1Up();
        base.HitBehaviour(false);
    }

    private void Spawn1Up()
    {
        Instantiate(oneUpMushroom, transform.position, Quaternion.identity);
    }

}
