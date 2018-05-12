using UnityEngine;

public class Castle : MonoBehaviour
{
    public GameObject objFinish;

    private Finish finish;

    private void Awake()
    {
        finish = objFinish.GetComponent<Finish>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        finish.EnteredCastle();
    }

}
