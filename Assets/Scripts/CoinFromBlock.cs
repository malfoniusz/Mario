using UnityEngine;

public class CoinFromBlock : Coin
{
    public GameObject pointsFloating;

    private Animator anim;
    private bool extraLife;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        extraLife = UICoins.AddCoin();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject pointsObject = Instantiate(pointsFloating);
            pointsObject.transform.GetChild(0).position = transform.GetChild(0).position;
            pointsObject.GetComponent<PointsFloating>().SetPoints(points, extraLife);

            Destroy(gameObject);
        }
    }

}
