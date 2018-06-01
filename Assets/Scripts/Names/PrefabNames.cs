using UnityEngine;

public class PrefabNames : MonoBehaviour
{
    public static string[] marioGroundChecks = { "GroundCheck1", "GroundCheck2" };
    public static string[] marioTopChecks = { "TopCheck1", "TopCheck2" };

    public static Transform[] GetChecks(GameObject obj, string[] stringChecks)
    {
        Transform[] checks = new Transform[stringChecks.Length];

        for (int i = 0; i < checks.Length; i++)
        {
            checks[i] = obj.transform.Find(stringChecks[i]);
        }

        return checks;
    }

}
