using UnityEngine;

public class Savepoint : MonoBehaviour
{
    public Transform savepointTrans;

    private SceneTransfer sceneTransfer;

    private void Awake()
    {
        sceneTransfer = TagNames.GetSceneTransfer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagNames.player)
        {
            sceneTransfer.SaveSavepoint(savepointTrans.position);
        }
    }

}
