﻿using UnityEngine;

public class StarPowerup : Mushroom
{
    public Transform[] groundChecks;
    public float jumpStrength = 260f;
    public float gravityScale = 0.5f;

    private PlayerInvincibility playerInv;
    private bool feltFromBlock = false;
    private bool grounded = true;

    protected override void Awake()
    {
        base.Awake();

        rb.gravityScale = gravityScale;
        playerInv = player.GetComponent<PlayerInvincibility>();
    }

    private void Update()
    {
        grounded = Contact.ContactPoints(groundChecks);
        SwitchFelt();
    }

    private void SwitchFelt()
    {
        if (feltFromBlock == false && grounded == false)
        {
            feltFromBlock = true;
        }
    }

    protected override void MovingBehaviour()
    {
        base.MovingBehaviour();

        if (appearAnim == false) Jumping();
    }

    private void Jumping()
    {
        if (grounded && feltFromBlock)
        {
            Vector2 jumpVel = new Vector2(rb.velocity.x, jumpStrength);
            rb.velocity = jumpVel;
        }
    }

    protected override void CollisionBehaviour()
    {
        playerInv.Invincibility();
        SpawnPoints();
        Destroy(gameObject);
    }

}
