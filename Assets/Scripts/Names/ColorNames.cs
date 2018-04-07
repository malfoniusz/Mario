using UnityEngine;

public class ColorNames : MonoBehaviour
{
    public enum Colors
    {
        normalBackground,
        undergroundBackground,
    }

    static public Color GetColor(Colors color)
    {
        switch (color)
        {
            case Colors.normalBackground:
                return normalBackground;
            case Colors.undergroundBackground:
                return undergroundBackground;
            default:
                throw new System.Exception("Chosen color dosen't exist.");
        }
    }

    static public Color normalBackground = new Color32(139, 158, 251, 0);
    static public Color undergroundBackground = Color.black;

}
