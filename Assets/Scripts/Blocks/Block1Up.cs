using UnityEngine;

public class Block1Up : BlockTurnSolid
{
    public GameObject OneUpMushroom;

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
        // TODO: uzupelnij definicje

        //GameObject powerupObject;
        //powerupObject = (playerPowerup.level == 1) ? Instantiate(mushroom) : Instantiate(fireFlower);
        //powerupObject.transform.GetChild(0).transform.localPosition = transform.position;
    }

}
