using UnityEngine;

public class ButtonNames : MonoBehaviour
{
    public static bool disableInput = false;
    public static string horizontal = "Horizontal";
    public static string jump = "Jump";
    public static string run = "Run";
    public static string up = "Up";
    public static string down = "Down";
    public static string left = "Left";
    public static string right = "Right";

    public static float GetRawHorizontal()
    {
        if (disableInput) return 0;
        return Input.GetAxisRaw(horizontal);
    }

    public static bool JumpHeld()
    {
        if (disableInput) return false;
        return Input.GetButton(jump);
    }

    public static bool JumpPressed()
    {
        if (disableInput) return false;
        return Input.GetButtonDown(jump);
    }

    public static bool RunHeld()
    {
        if (disableInput) return false;
        return Input.GetButton(run);
    }

    public static bool RunPressed()
    {
        if (disableInput) return false;
        return Input.GetButtonDown(run);
    }

    public static bool UpHeld()
    {
        if (disableInput) return false;
        return Input.GetButton(up);
    }

    public static bool UpPressed()
    {
        if (disableInput) return false;
        return Input.GetButtonDown(up);
    }

    public static bool DownHeld()
    {
        if (disableInput) return false;
        return Input.GetButton(down);
    }

    public static bool DownPressed()
    {
        if (disableInput) return false;
        return Input.GetButtonDown(down);
    }

    public static bool LeftHeld()
    {
        if (disableInput) return false;
        return Input.GetButton(left);
    }

    public static bool LeftPressed()
    {
        if (disableInput) return false;
        return Input.GetButtonDown(left);
    }

    public static bool RightHeld()
    {
        if (disableInput) return false;
        return Input.GetButton(right);
    }

    public static bool RightPressed()
    {
        if (disableInput) return false;
        return Input.GetButtonDown(right);
    }

}
