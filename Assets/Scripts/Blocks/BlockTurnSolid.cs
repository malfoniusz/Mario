using UnityEngine;

public class BlockTurnSolid : BlockAnimated
{
    public GameObject solidBlock;

    protected GameObject solidContainer;

    protected override void Awake()
    {
        base.Awake();
        solidContainer = GameObject.FindWithTag("SolidBlockContainer");
    }

    protected override void Update()
    {
        PlayerHit();
        Sound();

        if (playerHit)
        {
            HitBehaviour();
        }
    }

    protected virtual void HitBehaviour()
    {
        CreateSolidBlock(true);
        Destroy(parent);
    }

    protected void CreateSolidBlock(bool hitOnAwake)
    {
        GameObject solid = Instantiate(solidBlock);
        solid.transform.parent = solidContainer.transform;

        GameObject solidChild = solid.transform.GetChild(0).gameObject;
        solidChild.transform.localPosition = transform.position;
        solidChild.GetComponent<SolidBlock>().hitOnAwake = hitOnAwake;
    }

}
