using UnityEngine;
using System.Collections;

public class PlayerPowerup : MonoBehaviour
{
    public Animator anim;
    public AudioClip jumpClip;
    public AudioClip bigJumpClip;
    public AudioSource jumpAudio;
    public AudioSource powerupAudio;
    public AudioSource powerdownAudio;

    private int level = 1;   // 1 - mario, 2 - bigMario, 3 - fireMario
    private PlayerDeath playerDeath;
    private float powerdownInvDur = 2.5f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerDeath = GetComponent<PlayerDeath>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void MushroomPowerup()
    {
        powerupAudio.Play();
        if (level == 1)
        {
            PowerupBehaviour(2);
        }
    }

    public void FireFlowerPowerup()
    {
        powerupAudio.Play();
        if (level == 1)
        {
            PowerupBehaviour(2);
        }
        else if (level == 2)
        {
            PowerupBehaviour(3);
        }
    }

    private void PowerupBehaviour(int newLevel)
    {
        level = newLevel;
        anim.SetTrigger("Powerup");
        jumpAudio.clip = bigJumpClip;
    }

    public void PlayerHit()
    {
        if (level == 1)
        {
            playerDeath.Die();
        }
        else
        {
            Powerdown();
        }
    }

    private void Powerdown()
    {
        level--;
        anim.SetTrigger("Powerdown");
        powerdownAudio.Play();

        if (level == 1)
        {
            jumpAudio.clip = jumpClip;
        }

        StartCoroutine(PowerdownInvincibility(powerdownInvDur));
    }

    private IEnumerator PowerdownInvincibility(float sec)
    {
        Color trans = spriteRenderer.color;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
        trans.a = 0.5f;
        spriteRenderer.color = trans;

        yield return new WaitForSeconds(sec);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        trans.a = 1f;
        spriteRenderer.color = trans;
    }

    public int GetLevel()
    {
        return level;
    }

}
