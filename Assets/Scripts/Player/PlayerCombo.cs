using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    private PlayerMovement playerMovement;
    static private int combo = 0;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (combo != 0)
        {
            bool grounded = playerMovement.CheckContact(playerMovement.groundChecks, playerMovement.floorMask);
            if (grounded == true)
            {
                combo = 0;
            }
        }
    }

    static public int Combo(int points)
    {
        int comboPoints = (int) (points * Mathf.Pow(2, combo));
        combo++;
        return comboPoints;
    }

}
