using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioPowerup;
    public float invDuration = 10f;
    public float invExpireTime = 2f;

    private MusicController musicController;
    private int animInvLayerIndex;
    private int animInvExpiereLayerIndex;
    private bool invincible = false;

    private void Awake()
    {
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        animInvLayerIndex = AnimatorNames.GetPlayerInvincibilityLayer(anim);
        animInvExpiereLayerIndex = AnimatorNames.GetPlayerInvincibilityExpireLayer(anim);
    }

    public void Invincibility()
    {
        StartCoroutine(InvincibilityMain());
    }

    private IEnumerator InvincibilityMain()
    {
        audioPowerup.Play();
        musicController.Play(MusicEnum.invincibility, true);
        anim.SetLayerWeight(animInvLayerIndex, 1);
        invincible = true;

        yield return new WaitForSeconds(invDuration - invExpireTime);

        musicController.Play(MusicEnum.main, true);
        anim.SetLayerWeight(animInvLayerIndex, 0);
        anim.SetLayerWeight(animInvExpiereLayerIndex, 1);

        yield return new WaitForSeconds(invExpireTime);

        anim.SetLayerWeight(animInvExpiereLayerIndex, 0);
        invincible = false;
    }

    public void SetInvincible(bool inv)
    {
        invincible = inv;
    }

    public bool GetInvincible()
    {
        return invincible;
    }

}
