using UnityEngine;

public class OneUpBlock : BlockTurnSolid
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

    protected override void HitBehaviour()
    {
        Spawn1Up();
        CreateSolidBlock(false);
        Destroy(parent);
    }

    private void Spawn1Up()
    {
        GameObject oneUpObject = Instantiate(oneUpMushroom);
        oneUpObject.transform.GetChild(0).transform.localPosition = transform.position;
    }

}
