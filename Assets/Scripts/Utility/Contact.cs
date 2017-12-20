using UnityEngine;

public class Contact : MonoBehaviour
{
    public static bool ContactPoints(Transform[] contactChecks)
    {
        bool contact = OverlapPoint(contactChecks, ~0);

        return contact;
    }

    public static bool ContactPoints(Transform[] contactChecks, int layer)
    {
        int layerMask = (1 << layer);

        bool contact = OverlapPoint(contactChecks, layerMask);

        return contact;
    }

    public static bool ContactPoints(Transform[] contactChecks, int[] layers)
    {
        int combinedMask = CombineMask(layers);

        bool contact = OverlapPoint(contactChecks, combinedMask);

        return contact;
    }

    public static bool ContactPointsIgnore(Transform[] contactChecks, int layer)
    {
        int ignoredLayerMask = ~(1 << layer);

        bool contact = OverlapPoint(contactChecks, ignoredLayerMask);

        return contact;
    }

    public static bool ContactPointsIgnore(Transform[] contactChecks, int[] layers)
    {
        int combinedMask = CombineMask(layers);
        int ignoredCombinedMask = ~combinedMask;

        bool contact = OverlapPoint(contactChecks, ignoredCombinedMask);

        return contact;
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

}
