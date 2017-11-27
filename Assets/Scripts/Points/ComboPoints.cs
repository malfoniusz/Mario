using UnityEngine;

public class ComboPoints : MonoBehaviour
{
    static private int combo = 0;
    static private float time = 0;

    private const float COMBO_DURATION = 0.75f;

    void Update()
    {
        time += Time.deltaTime;

        if (combo != 0 && time > COMBO_DURATION)
        {
            combo = 0;
        }
    }

    static public int Combo(int points)
    {
        int comboPoints = (int) (points * Mathf.Pow(2, combo));
        combo++;
        time = 0;
        return comboPoints;
    }

}
