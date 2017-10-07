using UnityEngine;
using UnityEngine.UI;

public class PointsFloating : MonoBehaviour
{
    private Animator anim;
    private Text text;
    private int points = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        text = transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        UIPoints.AddPoints(points);
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetPoints(int points, bool extraLife)
    {
        this.points = points;

        if (extraLife)
        {
            SetText("1UP");
        }
        else
        {
            SetText(points.ToString());
        }
    }

    void SetText(string str)
    {
        text.text = str;
    }

}
