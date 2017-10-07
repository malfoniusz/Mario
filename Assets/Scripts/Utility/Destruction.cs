using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour
{
    public static IEnumerator DelayedDestroy(float sec, GameObject obj)
    {
        yield return new WaitForSeconds(sec);
        Destroy(obj);
    }
}
