using UnityEngine;
using UnityEngine.UI;

public class PointsFloating : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;
    private Text text;
    private int points = 0;
    private bool extraLife = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        text = transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        if (extraLife) audioSource.Play();
        UIPoints.AddPoints(points);
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
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

}
