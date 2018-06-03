using UnityEngine;

public class ColorNames : MonoBehaviour
{
    public static Color GetColor(ColorEnum color)
    {
        switch (color)
        {
            case ColorEnum.normalBackground:
                return normalBackground;
            case ColorEnum.undergroundBackground:
                return undergroundBackground;
            default:
                throw new System.Exception("Chosen color dosen't exist.");
        }
    }

    public static Color normalBackground = new Color32(139, 158, 251, 0);
    public static Color undergroundBackground = Color.black;

}
