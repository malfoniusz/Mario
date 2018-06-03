using System.Collections;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public GameObject objFinish;
    public GameObject objCastleFlag;
    public GameObject objCastleFlagEndPos;
    public float castleFlagRiseSpeed = 30f;

    private Finish finish;

    private void Awake()
    {
        finish = objFinish.GetComponent<Finish>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagNames.player)
        {
            finish.EnteredCastle();
        }
    }

    public void ShowCastleFlag()
    {
        StartCoroutine(ShowCastleFlagCoroutine());
    }

    private IEnumerator ShowCastleFlagCoroutine()
    {
        Vector2 startPos = objCastleFlag.transform.position;
        Vector2 endPos = objCastleFlagEndPos.transform.position;

        MoveObject moveObjFlag = MoveObject.CreateMoveObject4(startPos, endPos, castleFlagRiseSpeed);

        while (moveObjFlag.ReachedEnd() == false)
        {
            objCastleFlag.transform.position = moveObjFlag.NextPosition();
            yield return new WaitForSeconds(0.01f);
        }

        finish.NextLevel();

        yield return null;
    }

}
