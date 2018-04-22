using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public BoxCollider2D[] flagPoleColliders;
    public int[] pointsForCollider;
    public GameObject flagPoleBase;
    public Transform spawnPointsPos;

    private bool flagTouched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (flagTouched == false && collision.gameObject.tag == TagNames.player)
        {
            flagTouched = true;
            SpawnPoints();
        }
    }

    private void SpawnPoints()
    {
        //SpawnPointsFloating.Points(spawnPointsPos.position, 100);
    }

}
