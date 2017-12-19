using UnityEngine;

public class SolidIfHit : Block
{
    public GameObject solidBlock;

    protected override void Update()
    {
        playerHit = PlayerHit();
        if (playerHit) HitBehaviour(true);
    }

    protected virtual void HitBehaviour(bool hitOnAwake)
    {
        CreateSolidBlock(hitOnAwake, transform.position, solidBlock);
        Destroy(gameObject);
    }

    static public void CreateSolidBlock(bool hitOnAwake, Vector3 position, GameObject solidBlock)
    {
        GameObject solidBlockContainer = TagNames.GetSolidBlockContainer();
        GameObject solid = Instantiate(solidBlock, position, Quaternion.identity, solidBlockContainer.transform);
        solid.GetComponent<SolidBlock>().SetHitOnAwake(hitOnAwake);
    }

}
