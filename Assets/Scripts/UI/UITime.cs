using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    public static bool stop = false;

    public float countdownSpeed = 3;
    public int time = 400;
    public int hurryTime = 100;

    private MusicController musicController;
    private PlayerDeath playerDeath;
    private Text text;
    private float nextTimeInc;
    private float deltaTime = 0;
    private bool hurryTimeSwitch = false;

    private void Awake()
    {
        musicController = TagNames.GetMusicController().GetComponent<MusicController>();
        playerDeath = TagNames.GetPlayer().GetComponent<PlayerDeath>();
        text = GetComponent<Text>();
        nextTimeInc = 1 / countdownSpeed;
    }

    private void Start()
    {
        SetText(time);
    }

    private void Update()
    {
        if (!stop)
        {
            CountTime();
        }
    }

    void CountTime()
    {
        TimeCounting();
        HurryMusic();
        DeathByTime();
    }

    void TimeCounting()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > nextTimeInc)
        {
            time -= 1;
            deltaTime = 0;
            SetText(time);
        }
    }

    void HurryMusic()
    {
        if (time <= hurryTime && hurryTimeSwitch == false)
        {
            hurryTimeSwitch = true;
            musicController.Play(MusicEnum.hurry, true);
        }
    }

    void DeathByTime()
    {
        if (time <= 0)
        {
            playerDeath.Die();
        }
    }

    void SetText(int time)
    {
        text.text = time.ToString();
    }

}
