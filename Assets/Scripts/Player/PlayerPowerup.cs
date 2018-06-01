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
    private GameObject player;
    private PowerupAnimation powerupAnimation;
    private PlayerDeath playerDeath;
    private float powerdownInvDur = 2.5f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = TagNames.GetPlayer();
        powerupAnimation = player.GetComponent<PowerupAnimation>();
        playerDeath = player.GetComponent<PlayerDeath>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    public void MushroomPowerup()
    {
        powerupAudio.Play();
        if (level == 1)
        {
            PowerupBehaviour();
        }
    }

    public void FireFlowerPowerup()
    {
        powerupAudio.Play();
        if (level == 1)
        {
            PowerupBehaviour();
        }
        else if (level == 2)
        {
            PowerupBehaviour();
        }
    }

    private void PowerupBehaviour()
    {
        level++;
        anim.SetTrigger(AnimatorNames.playerPowerup);
        powerupAnimation.StartAnimation(true, level);
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
        anim.SetTrigger(AnimatorNames.playerPowerdown);
        powerupAnimation.StartAnimation(false, level);
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

        Physics2D.IgnoreLayerCollision(LayerNames.GetPlayer(), LayerNames.GetEnemy());
        trans.a = 0.5f;
        spriteRenderer.color = trans;

        yield return new WaitForSeconds(sec);

        Physics2D.IgnoreLayerCollision(LayerNames.GetPlayer(), LayerNames.GetEnemy(), false);
        trans.a = 1f;
        spriteRenderer.color = trans;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int value)
    {
        level = value;
    }

    public void ChangeAppearanceToLevel(int value)
    {
        level = value;
        // TODO: uzupełnić
        //PowerupBehaviour(3);
    }

}
