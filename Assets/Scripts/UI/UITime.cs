using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    public static bool stop = false; 

    public float countdownSpeed = 3;
    public int time = 400;

    private AudioSource audioEnvironment;
    private AudioSource audioHurry;
    private PlayerDeath playerDeath;
    private Text text;
    private float nextTimeInc;
    private float deltaTime = 0;
    private bool lowTime = false;

    private void Awake()
    {
        audioEnvironment = GameObject.FindGameObjectWithTag("Environment").GetComponent<AudioSource>();
        audioHurry = GetComponent<AudioSource>();
        playerDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
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
        if (time <= 100 && lowTime == false)
        {
            lowTime = true;
            audioEnvironment.Stop();
            audioHurry.Play();
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
