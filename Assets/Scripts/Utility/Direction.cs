using UnityEngine;

public class Direction : MonoBehaviour
{
    public static float HitDirection(Transform object1, Transform object2)
    {
        float hitDirection = Mathf.Sign(object1.position.y - object2.position.y);
        return hitDirection;
    }
}
