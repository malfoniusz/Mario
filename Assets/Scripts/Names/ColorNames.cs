using UnityEngine;

public class ColorNames : MonoBehaviour
{
    static public Color GetColor(ColorEnum color)
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

    static public Color normalBackground = new Color32(139, 158, 251, 0);
    static public Color undergroundBackground = Color.black;

}
