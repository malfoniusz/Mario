using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    public static bool stop = false;

    public AudioSource audioTimeToPoints;
    public float countdownSpeed = 3;
    public int time = 400;
    public int hurryTime = 100;
    public int pointsForTimeUnit = 50;
    public float intervalTimeToPointsInSeconds = 0.01f;

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

    private void CountTime()
    {
        TimeCounting();
        HurryMusic();
        DeathByTime();
    }

    private void TimeCounting()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > nextTimeInc)
        {
            time -= 1;
            deltaTime = 0;
            SetText(time);
        }
    }

    private void HurryMusic()
    {
        if (time <= hurryTime && hurryTimeSwitch == false)
        {
            hurryTimeSwitch = true;
            musicController.Play(MusicEnum.hurry, true);
        }
    }

    private void DeathByTime()
    {
        if (time <= 0)
        {
            playerDeath.Die();
        }
    }

    private void SetText(int time)
    {
        text.text = time.ToString();
    }

    public void TimeToPoints()
    {
        StartCoroutine(TimeToPointCoroutine());
    }

    IEnumerator TimeToPointCoroutine()
    {
        audioTimeToPoints.Play();

        while (time > 0)
        {
            UIPoints.AddPoints(pointsForTimeUnit);
            time--;
            SetText(time);
            yield return new WaitForSeconds(intervalTimeToPointsInSeconds);
        }

        audioTimeToPoints.Stop();

        yield return null;
    }

}
