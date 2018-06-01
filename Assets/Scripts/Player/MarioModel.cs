﻿using UnityEngine;

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
        player = TagNames.GetPlayer();
        playerPowerup = player.GetComponent<PlayerPowerup>();

        marioCollider = marioPrefab.GetComponent<BoxCollider2D>();
        marioGroundChecks = new Transform[2];
        marioGroundChecks[0] = marioPrefab.transform.Find(PrefabNames.marioGroundCheck1);
        marioGroundChecks[1] = marioPrefab.transform.Find(PrefabNames.marioGroundCheck2);
        marioTopChecks = new Transform[2];
        marioTopChecks[0] = marioPrefab.transform.Find(PrefabNames.marioTopCheck1);
        marioTopChecks[1] = marioPrefab.transform.Find(PrefabNames.marioTopCheck2);

        bigMarioCollider = bigMarioPrefab.GetComponent<BoxCollider2D>();
        bigMarioGroundChecks = new Transform[2];
        bigMarioGroundChecks[0] = bigMarioPrefab.transform.Find(PrefabNames.marioGroundCheck1);
        bigMarioGroundChecks[1] = bigMarioPrefab.transform.Find(PrefabNames.marioGroundCheck2);
        bigMarioTopChecks = new Transform[2];
        bigMarioTopChecks[0] = bigMarioPrefab.transform.Find(PrefabNames.marioTopCheck1);
        bigMarioTopChecks[1] = bigMarioPrefab.transform.Find(PrefabNames.marioTopCheck2);
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

    private void UpdateModelBehaviour(BoxCollider2D boxCollider, Transform[] groundChecks, Transform[] topChecks)
    {
        playerBoxCollider.size = boxCollider.size;

        for (int i = 0; i < playerGroundChecks.Length; i++)
        {
            playerGroundChecks[i].localPosition = groundChecks[i].localPosition;
        }

        for (int i = 0; i < playerTopChecks.Length; i++)
        {
            playerTopChecks[i].localPosition = topChecks[i].localPosition;
        }
    }

}
