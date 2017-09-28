using UnityEngine;
using System.Collections;

public class BrickBroken : MonoBehaviour
{
    public GameObject[] pieces;
    public float rotationValue = 60f;

    private Rigidbody2D[] rbPieces;

    private void Awake()
    {
        rbPieces = new Rigidbody2D[pieces.Length];

        for (int i = 0; i < pieces.Length; i++)
        {
            rbPieces[i] = pieces[i].GetComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        PushPieces();
    }

    private void FixedUpdate()
    {
        RotatePieces();
    }

    private void PushPieces()
    {
        const float FORCE_X = 4000f;
        const float FORCE_Y = 13000f;

        const float FORCE_X_LOWER = FORCE_X * ((float)2 / 3);
        const float FORCE_Y_HIGHER = FORCE_Y * ((float)4 / 3);

        FlyAway(rbPieces[0], new Vector2(-FORCE_X, FORCE_Y_HIGHER));
        FlyAway(rbPieces[1], new Vector2(FORCE_X, FORCE_Y_HIGHER));
        FlyAway(rbPieces[2], new Vector2(-FORCE_X_LOWER, FORCE_Y));
        FlyAway(rbPieces[3], new Vector2(FORCE_X_LOWER, FORCE_Y));

        StartCoroutine(WaitForDestroy(2));
    }

    private void FlyAway(Rigidbody2D rb, Vector2 dir)
    {
        rb.AddForce(dir);
    }

    IEnumerator WaitForDestroy(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }

    private void RotatePieces()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            rbPieces[i].transform.Rotate(new Vector3(0, 0, rotationValue));
        }
    }

}
