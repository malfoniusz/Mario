using UnityEngine;

public class Contact : MonoBehaviour
{
    private static int groundMask = LayerMask.NameToLayer("Ground");

    public static bool CheckContactGround(Vector3 position, Transform[] contactChecks)
    {
        bool contact = CheckContact(position, contactChecks, groundMask);
        return contact;
    }

    public static bool CheckContact(Vector3 position, Transform[] contactChecks, int layerMask)
    {
        bool contact = false;
        for (int i = 0; i < contactChecks.Length; i++)
        {
            contact = Physics2D.Linecast(position, contactChecks[i].position, 1 << layerMask);
            if (contact == true)
            {
                break;
            }
        }

        return contact;
    }

}
