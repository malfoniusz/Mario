using UnityEngine;
using UnityEngine.UI;

public class UIWorld : MonoBehaviour
{
    public static int world = 1;
    public static int level = 1;

    private Text child;

    void Awake()
    {
        child = transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    void Start()
    {
        child.text = world + "-" + level;
    }

}
