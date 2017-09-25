using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    public Animator anim;
    public AudioClip jumpClip;
    public AudioClip bigJumpClip;
    public AudioSource jumpAudio;
    public AudioSource powerupAudio;
    public AudioSource powerdownAudio;
    public int level = 1;   // 1 - mario, 2 - bigMario, 3 - fireMario
    public GameObject marioModel;
    public GameObject bigMarioModel;

    private PlayerMovement playerMovement;
    private PlayerDeath playerDeath;
    private BoxCollider2D marioCollider;
    private Transform[] marioGroundChecks;
    private Transform[] marioTopChecks;
    private BoxCollider2D bigMarioCollider;
    private Transform[] bigMarioGroundChecks;
    private Transform[] bigMarioTopChecks;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerDeath = GetComponent<PlayerDeath>();
        InitModels();
    }

    private void InitModels()
    {
        marioCollider = marioModel.GetComponent<BoxCollider2D>();
        marioGroundChecks = new Transform[2];
        marioGroundChecks[0] = marioModel.transform.Find("GroundCheck1");
        marioGroundChecks[1] = marioModel.transform.Find("GroundCheck2");
        marioTopChecks = new Transform[2];
        marioTopChecks[0] = marioModel.transform.Find("TopCheck1");
        marioTopChecks[1] = marioModel.transform.Find("TopCheck2");

        bigMarioCollider = bigMarioModel.GetComponent<BoxCollider2D>();
        bigMarioGroundChecks = new Transform[2];
        bigMarioGroundChecks[0] = bigMarioModel.transform.Find("GroundCheck1"); 
        bigMarioGroundChecks[1] = bigMarioModel.transform.Find("GroundCheck2");
        bigMarioTopChecks = new Transform[2];
        bigMarioTopChecks[0] = bigMarioModel.transform.Find("TopCheck1");
        bigMarioTopChecks[1] = bigMarioModel.transform.Find("TopCheck2");
    }

    public void MushroomPowerup()
    {
        if (level == 1)
        {
            PowerupBehaviour();
        }
    }

    public void FireFlowerPowerup()
    {
        if (level == 1 || level == 2)
        {
            level = 2;
            PowerupBehaviour();
        }
    }

    private void PowerupBehaviour()
    {
        level++;
        anim.SetTrigger("Powerup");
        anim.speed = 1;
        powerupAudio.Play();
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
        jumpAudio.clip = jumpClip;

        // niezniszczalnosc (migotanie)

        if (level == 2)
        {

        }
        else if (level == 3)
        {

        }
    }
    
    public void UpdateModel()
    {
        if (level == 1)
        {
            playerMovement.UpdateModel(marioCollider, marioGroundChecks, marioTopChecks);
        }
        else if (level == 2 || level == 3)
        {
            playerMovement.UpdateModel(bigMarioCollider, bigMarioGroundChecks, bigMarioTopChecks);
        }
    }

}
