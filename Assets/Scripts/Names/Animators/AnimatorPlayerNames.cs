using UnityEngine;

public class AnimatorPlayerNames : MonoBehaviour
{
    public static string isJumping = "IsJumping";
    public static string isTurning = "IsTurning";
    public static string isWalking = "IsWalking";
    public static string isGrabbing = "IsGrabbing";
    public static string isDead = "IsDead";
    public static string walkSpeedMultiplier = "WalkSpeedMultiplier";
    public static string powerup = "Powerup";
    public static string powerdown = "Powerdown";
    public static string fireballShot = "FireballShot";
    public static string invincibility = "Invincibility";
    public static string invincibilityExpire = "InvincibilityExpire";
    public static string smallMarioIdle = "SmallMarioIdle";
    public static string bigMarioIdle = "BigMarioIdle";
    public static string fireMarioIdle = "FireMarioIdle";

    public static int GetInvincibilityLayer(Animator anim)
    {
        return anim.GetLayerIndex(invincibility);
    }

    public static int GetInvincibilityExpireLayer(Animator anim)
    {
        return anim.GetLayerIndex(invincibilityExpire);
    }

}
