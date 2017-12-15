using UnityEngine;

public class MarioModel : MonoBehaviour
{
    public GameObject marioPrefab;
    public GameObject bigMarioPrefab;
    public BoxCollider2D playerBoxCollider;
    public Transform[] playerGroundChecks;
    public Transform[] playerTopChecks;

    private GameObject player;
    private PlayerPowerup playerPowerup;

    private BoxCollider2D marioCollider;
    private Transform[] marioGroundChecks;
    private Transform[] marioTopChecks;
    private BoxCollider2D bigMarioCollider;
    private Transform[] bigMarioGroundChecks;
    private Transform[] bigMarioTopChecks;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPowerup = player.GetComponent<PlayerPowerup>();

        marioCollider = marioPrefab.GetComponent<BoxCollider2D>();
        marioGroundChecks = new Transform[2];
        marioGroundChecks[0] = marioPrefab.transform.Find("GroundCheck1");
        marioGroundChecks[1] = marioPrefab.transform.Find("GroundCheck2");
        marioTopChecks = new Transform[2];
        marioTopChecks[0] = marioPrefab.transform.Find("TopCheck1");
        marioTopChecks[1] = marioPrefab.transform.Find("TopCheck2");

        bigMarioCollider = bigMarioPrefab.GetComponent<BoxCollider2D>();
        bigMarioGroundChecks = new Transform[2];
        bigMarioGroundChecks[0] = bigMarioPrefab.transform.Find("GroundCheck1");
        bigMarioGroundChecks[1] = bigMarioPrefab.transform.Find("GroundCheck2");
        bigMarioTopChecks = new Transform[2];
        bigMarioTopChecks[0] = bigMarioPrefab.transform.Find("TopCheck1");
        bigMarioTopChecks[1] = bigMarioPrefab.transform.Find("TopCheck2");
    }

    public void UpdateModel()
    {
        int level = playerPowerup.GetLevel();
        if (level == 1)
        {
            UpdateModelBehaviour(marioCollider, marioGroundChecks, marioTopChecks);
        }
        else if (level == 2 || level == 3)
        {
            UpdateModelBehaviour(bigMarioCollider, bigMarioGroundChecks, bigMarioTopChecks);
        }
    }

    public void UpdateModelBehaviour(BoxCollider2D bc, Transform[] gc, Transform[] tc)
    {
        playerBoxCollider.size = bc.size;

        for (int i = 0; i < playerGroundChecks.Length; i++)
        {
            playerGroundChecks[i].localPosition = gc[i].localPosition;
        }

        for (int i = 0; i < playerTopChecks.Length; i++)
        {
            playerTopChecks[i].localPosition = tc[i].localPosition;
        }
    }

}
