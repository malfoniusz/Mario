using UnityEngine;

public class Contact : MonoBehaviour
{
    public static bool ContactPoints(Transform[] contactChecks)
    {
        return OverlapPoint(contactChecks, ~0);
    }

    public static bool ContactPoints(Transform[] contactChecks, int layer)
    {
        int layerMask = (1 << layer);
        return OverlapPoint(contactChecks, layerMask);
    }

    public static bool ContactPoints(Transform[] contactChecks, int[] layers)
    {
        int combinedMask = CombineMask(layers);
        return OverlapPoint(contactChecks, combinedMask);
    }

    public static bool ContactPointsIgnore(Transform[] contactChecks, int layer)
    {
        int ignoredLayerMask = ~(1 << layer);
        return OverlapPoint(contactChecks, ignoredLayerMask);
    }

    public static bool ContactPointsIgnore(Transform[] contactChecks, int[] layers)
    {
        int combinedMask = CombineMask(layers);
        int ignoredCombinedMask = ~combinedMask;

        return OverlapPoint(contactChecks, ignoredCombinedMask);
    }

    private static int CombineMask(int[] layers)
    {
        int[] layerMasks = new int[layers.Length];
        for (int i = 0; i < layerMasks.Length; i++)
        {
            layerMasks[i] = 1 << layers[i];
        }

        int combinedMask = 0;
        foreach (int layerMask in layerMasks)
        {
            combinedMask |= layerMask;
        }

        return combinedMask;
    }

    private static bool OverlapPoint(Transform[] contactChecks, int layerMask)
    {
        for (int i = 0; i < contactChecks.Length; i++)
        {
            bool contact = Physics2D.OverlapPoint(contactChecks[i].position, layerMask);

            if (contact) return true;
        }

        return false;
    }

    public static bool ContactLines(Vector2 objPos, Transform[] contactChecks)
    {
        return LineCast(objPos, contactChecks, ~0);
    }

    public static bool ContactLines(Vector2 objPos, Transform[] contactChecks, int layer)
    {
        int layerMask = (1 << layer);
        return LineCast(objPos, contactChecks, layerMask);
    }

    public static bool ContactLines(Vector2 objPos, Transform[] contactChecks, int[] layers)
    {
        int combinedMask = CombineMask(layers);
        return LineCast(objPos, contactChecks, combinedMask);
    }

    private static bool LineCast(Vector2 objPos, Transform[] contactChecks, int layerMask)
    {
        for (int i = 0; i < contactChecks.Length; i++)
        {
            bool contact = Physics2D.Linecast(objPos, contactChecks[i].position, layerMask);

            if (contact) return true;
        }

        return false;
    }

}
