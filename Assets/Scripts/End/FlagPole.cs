using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Collider2D[] flagPoleColliders;
    public int[] pointsForCollider;
    public GameObject flagPoleBase;
    public Transform spawnPointsPos;
    public float pointsRiseDistance = 100;
    public float pointsRiseTimeInSeconds = 1;

    private bool flagTouched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (flagTouched == false && collision.gameObject.tag == TagNames.player)
        {
            flagTouched = true;
            SpawnPoints(collision);
        }
    }

    private void SpawnPoints(Collider2D collision)
    {
        int touchedColliderIndex = TouchedColliderIndex(collision);
        bool extraLife = (touchedColliderIndex == 0);

        SpawnPointsFloating.FlagPole(spawnPointsPos.position, pointsForCollider[touchedColliderIndex], extraLife, pointsRiseDistance, pointsRiseTimeInSeconds);
    }

    private int TouchedColliderIndex(Collider2D collision)
    {
        for (int i = 0; i < flagPoleColliders.Length; i++)
        {
            if (flagPoleColliders[i].IsTouching(collision)) 
            {
                return i;
            }
        }

        throw new System.Exception("Touched collider on the flag pole couldn't be found.");
    }

}
