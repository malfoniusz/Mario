using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    static public bool stop = false; 

    public AudioSource environmentMusic;
    public AudioClip hurryMusic;
    public float countdownSpeed;
    public int time;

    private Text text;
    private float nextTimeInc;
    private float deltaTime = 0;
    private bool lowTime = false;

    void Awake()
    {
        text = GetComponent<Text>();
        nextTimeInc = 1 / countdownSpeed;
    }

    void Start()
    {
        SetText(time);
    }

    void Update()
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
            environmentMusic.clip = hurryMusic;
            environmentMusic.Play();
        }
    }

    void SetText(int time)
    {
        text.text = time.ToString();
    }

}
