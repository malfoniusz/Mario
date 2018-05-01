using System.Collections;
using UnityEngine;

public class Flagpole : MonoBehaviour
{
    public Collider2D[] flagpoleColliders;
    public int[] pointsForCollider;
    public GameObject endObject;

    private End end;
    private bool flagTouched = false;

    private void Start()
    {
        end = endObject.GetComponent<End>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (flagTouched == false && collision.gameObject.tag == TagNames.player)
        {
            flagTouched = true;
            PlayerTouchedFlagpole(collision);
        }
    }

    private void PlayerTouchedFlagpole(Collider2D collision)
    {
        int touchedColliderIndex = TouchedColliderIndex(collision);
        bool extraLife = (touchedColliderIndex == 0);

        StartCoroutine(end.ClearStage(pointsForCollider[touchedColliderIndex], extraLife));
    }

    private int TouchedColliderIndex(Collider2D collision)
    {
        for (int i = 0; i < flagpoleColliders.Length; i++)
        {
            if (flagpoleColliders[i].IsTouching(collision)) 
            {
                return i;
            }
        }

        throw new System.Exception("Touched collider on the flag pole couldn't be found.");
    }

}
