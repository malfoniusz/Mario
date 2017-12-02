using UnityEngine;

public class SolidIfHit : Block
{
    public GameObject solidBlock;

    private const string solidBlockContainerName = "SolidBlockContainer";

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
        GameObject solidContainer = GameObject.FindWithTag(solidBlockContainerName);

        GameObject solid = Instantiate(solidBlock, position, Quaternion.identity, solidContainer.transform);
        solid.GetComponent<SolidBlock>().SetHitOnAwake(hitOnAwake);
    }

}
