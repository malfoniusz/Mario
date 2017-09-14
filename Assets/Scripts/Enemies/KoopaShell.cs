using UnityEngine;

public class KoopaShell : Enemy
{
    public AudioSource audioKick;

    protected override void MovingBehaviour()
    {
        //if (activated == false)
        //{
        //    CheckVisibility();
        //}
        //else
        //{
        //    ChangeDirection();
        //}
    }

    protected override void EnemyCollision(Collider2D collision)
    {
        //Transform playerTrans = collision.gameObject.transform;
        //float playerVelYAbs = Mathf.Abs(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        //float minimalHeight = playerVelYAbs < PLAYER_VELOCITY_SWITCH ? MINIMAL_HEIGHT_BIG : MINIMAL_HEIGHT_SMALL;

        //if (playerTrans.position.y > transform.position.y + minimalHeight)
        //{
        //    EnemyStomped(collision);
        //}
    }

}
