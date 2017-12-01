using UnityEngine;

public class Contact : MonoBehaviour
{
    private static int groundMask = LayerMask.NameToLayer("Ground");
    private static int playerMask = LayerMask.NameToLayer("Player");

    public static bool CheckContactGround(Vector3 playerPos, Transform[] contactChecks)
    {
        bool contact = CheckContact(playerPos, contactChecks, groundMask);
        return contact;
    }

    public static bool CheckEnemyStomped(Vector3 enemyPos, Transform[] enemyStompedChecks)
    {
        bool contact = CheckContact(enemyPos, enemyStompedChecks, playerMask);
        return contact;
    }

    private static bool CheckContact(Vector3 position, Transform[] contactChecks, int layerMask)
    {
        for (int i = 0; i < contactChecks.Length; i++)
        {
            bool contact = Physics2D.Linecast(position, contactChecks[i].position, 1 << layerMask);
            if (contact)
            {
                return true;
            }
        }

        return false;
    }

}
