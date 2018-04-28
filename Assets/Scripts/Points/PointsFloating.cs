using UnityEngine;
using UnityEngine.UI;

public class PointsFloating : MonoBehaviour
{
    public int points = 0;
    public bool extraLife = false;
    public float upDistance = 15;
    public float upSpeedMultiplier = 1;
    public AudioSource audioSource;
    public Text text;

    private MoveObject moveObject;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        text = transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        if (extraLife) audioSource.Play();
        UIPoints.AddPoints(points);
        moveObject = new MoveObject(transform.position, Vector3.up * upDistance, upSpeedMultiplier);
    }

    void FixedUpdate()
    {
        transform.position = moveObject.NextPosition();
        if (moveObject.ReachedEnd())
        {
            Destroy(gameObject);
        }
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

    public void SetUpDistance(float upDistance)
    {
        this.upDistance = upDistance;
    }

    public float GetUpDistance()
    {
        return upDistance;
    }

    public void SetUpSpeedMultiplier(float upSpeedMultiplier)
    {
        this.upSpeedMultiplier = upSpeedMultiplier;
    }

    public float GetUpSpeedMultiplier()
    {
        return upSpeedMultiplier;
    }

}
