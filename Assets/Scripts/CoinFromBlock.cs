using UnityEngine;

public class CoinFromBlock : Coin
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        UICoins.AddCoins(1);
        UIPoints.AddPoints(points);
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }
    }

}
