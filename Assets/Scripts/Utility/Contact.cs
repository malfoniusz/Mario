using UnityEngine;

public class Contact : MonoBehaviour
{
    private static int groundMask = LayerNames.GetGround();
    private static int playerMask = LayerNames.GetPlayer();

    public static bool CheckContactGround(Vector3 objectPos, Transform[] contactChecks)
    {
        bool contact = CheckContact(objectPos, contactChecks, groundMask);
        return contact;
    }

    public static bool CheckEnemyStomped(Vector3 enemyPos, Transform[] enemyStompedChecks)
    {
        bool contact = CheckContact(enemyPos, enemyStompedChecks, playerMask);
        return contact;
    }

    private static bool CheckContact(Vector3 position, Transform[] contactChecks, int layer)
    {
        int layerMask = 1 << layer;

        for (int i = 0; i < contactChecks.Length; i++)
        {
            bool contact = Physics2D.Linecast(position, contactChecks[i].position, layerMask);

            if (contact) return true;
        }

        return false;
    }

    public static bool CheckContactPoint(Transform[] contactChecks)
    {
        for (int i = 0; i < contactChecks.Length; i++)
        {
            bool contact = Physics2D.OverlapPoint(contactChecks[i].position);

            if (contact) return true;
        }

        return false;
    }

    public static bool CheckContactPointIgnore(Transform[] contactChecks, int layer)
    {
        int ignoredLayerMask = ~(1 << layer);

        for (int i = 0; i < contactChecks.Length; i++)
        {
            bool contact = Physics2D.OverlapPoint(contactChecks[i].position, ignoredLayerMask);

            if (contact) return true;
        }

        return false;
    }

    public static bool CheckContactPointIgnore(Transform[] contactChecks, int[] layers)
    {
        int[] layerMasks = new int[layers.Length];
        for (int i = 0; i < layerMasks.Length; i++)
        {
            layerMasks[i] = 1 << layers[i];
        }

        int combine = 0;
        foreach (int layerMask in layerMasks)
        {
            combine |= layerMask;
        }

        int ignoredCombine = ~combine;

        for (int i = 0; i < contactChecks.Length; i++)
        {
            bool contact = Physics2D.OverlapPoint(contactChecks[i].position, ignoredCombine);

            if (contact) return true;
        }

        return false;
    }

}
