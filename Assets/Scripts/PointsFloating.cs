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

        UpdateText();
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

    void UpdateText()
    {
        text.text = points.ToString();
    }

    public void SetPoints(int points)
    {
        this.points = points;
        UpdateText();
    }
}
