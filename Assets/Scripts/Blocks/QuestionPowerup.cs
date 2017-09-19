using UnityEngine;

public class QuestionPowerup : BlockTurnSolid
{
    public GameObject mushroom;

    protected override void HitBehaviour()
    {
        SpawnMushroom();
        CreateSolidBlock(false);
        Destroy(parent);
    }

    private void SpawnMushroom()
    {
        GameObject mushroomObject = Instantiate(mushroom);
        mushroomObject.transform.GetChild(0).transform.localPosition = transform.position;
    }

}
