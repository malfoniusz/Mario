using UnityEngine;
using UnityEngine.UI;

public class PointsFloating : MonoBehaviour
{
    public AudioSource audioSource;
    public Text text;

    private int points = 0;
    private bool extraLife = false;
    private bool extraLifePlaySound = true;
    private float riseDistance = 15;
    private float riseTimeInSeconds = 1;
    private bool deleteAfterReachingEnd = true;
    private bool addPointsAtEnd = false;
    private MoveObject moveObject;
    private bool updateOn = true;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        text = transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        if (extraLife && extraLifePlaySound) audioSource.Play();
        if (addPointsAtEnd == false) UIPoints.AddPoints(points);
        moveObject = MoveObject.CreateMoveObject1(transform.position, Vector3.up * riseDistance, riseTimeInSeconds);
    }

    void FixedUpdate()
    {
        if (updateOn)
        {
            transform.position = moveObject.NextPosition();
            if (moveObject.ReachedEnd())
            {
                AddToUI();
            }
        }
    }

    private void AddToUI()
    {
        if (addPointsAtEnd) UIPoints.AddPoints(points);
        if (deleteAfterReachingEnd) Destroy(gameObject);
        if (extraLife) UILives.AddLive();
        updateOn = false;
    }

    public void SetPointsAndExtraLife(int points, bool extraLife)
    {
        SetPoints(points);
        SetExtraLife(extraLife);
    }

    public void SetPoints(int points)
    {
        this.points = points;
        SetText(points.ToString());
    }

    public void SetExtraLife(bool extraLife)
    {
        if (extraLife)
        {
            this.extraLife = true;
            SetText("1UP");
        }
    }

    private void SetText(string str)
    {
        text.text = str;
    }

    public void SetExtraLifePlaySound(bool extraLifePlaySound)
    {
        this.extraLifePlaySound = extraLifePlaySound;
    }

    public bool GetExtraLifePlaySound()
    {
        return extraLifePlaySound;
    }

    public void SetRiseDistance(float riseDistance)
    {
        this.riseDistance = riseDistance;
    }

    public float GetRiseDistance()
    {
        return riseDistance;
    }

    public void SetRiseTimeInSeconds(float riseTimeInSeconds)
    {
        this.riseTimeInSeconds = riseTimeInSeconds;
    }

    public float GetRiseTimeInSeconds()
    {
        return riseTimeInSeconds;
    }

    public void SetDeleteAfterReachingEnd(bool deleteAfterReachingEnd)
    {
        this.deleteAfterReachingEnd = deleteAfterReachingEnd;
    }

    public bool GetDeleteAfterReachingEnd()
    {
        return deleteAfterReachingEnd;
    }

    public void SetAddPointsAtEnd(bool addPointsAtEnd)
    {
        this.addPointsAtEnd = addPointsAtEnd;
    }

    public bool GetAddPointsAtEnd()
    {
        return addPointsAtEnd;
    }

}
