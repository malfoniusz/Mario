using UnityEngine;

public class AnimatorNames : MonoBehaviour
{
    public static string goombaIsDead = "IsDead";

    public static string playerIsJumping = "IsJumping";
    public static string playerIsTurning = "IsTurning";
    public static string playerIsWalking = "IsWalking";
    public static string playerIsGrabbing = "IsGrabbing";
    public static string playerIsDead = "IsDead";
    public static string playerWalkSpeedMultiplier = "WalkSpeedMultiplier";
    public static string playerPowerup = "Powerup";
    public static string playerPowerdown = "Powerdown";
    public static string playerFireballShot = "FireballShot";

    public static string playerInvincibility = "Invincibility";
    public static string playerInvincibilityExpire = "InvincibilityExpire";

    public static int GetPlayerInvincibilityLayer(Animator anim)
    {
        return anim.GetLayerIndex(playerInvincibility);
    }

    public static int GetPlayerInvincibilityExpireLayer(Animator anim)
    {
        return anim.GetLayerIndex(playerInvincibilityExpire);
    }

}
