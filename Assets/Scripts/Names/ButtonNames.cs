using UnityEngine;

public class ButtonNames : MonoBehaviour
{
    public static string horizontal = "Horizontal";
    public static string jump = "Jump";
    public static string run = "Run";

    public static float GetRawHorizontal()
    {
        return Input.GetAxisRaw(horizontal);
    }

    public static bool GetJump()
    {
        return Input.GetButton(jump);
    }

    public static bool GetJumpDown()
    {
        return Input.GetButtonDown(jump);
    }

    public static bool GetRun()
    {
        return Input.GetButton(run);
    }

    public static bool GetRunDown()
    {
        return Input.GetButtonDown(run);
    }

}
