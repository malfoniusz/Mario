using UnityEngine;

public class ColorNames : MonoBehaviour
{
    public enum Colors {
        normalBackground,
        undergroundBackground,
    }

    static public Color GetColor(Colors color)
    {
        switch (color)
        {
            case Colors.normalBackground:
                return new Color32(139, 158, 251, 0);
            case Colors.undergroundBackground:
                return Color.black;
            default:
                throw new System.Exception("Color dosen't exist.");
        }
    }

}
