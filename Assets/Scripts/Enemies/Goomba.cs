using UnityEngine;

public class Goomba : Enemy
{
    private BoxCollider2D objectCollider;
    private GameObject child;
    private BoxCollider2D triggerCollider;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        objectCollider = GetComponent<BoxCollider2D>();
        child = transform.GetChild(0).gameObject;
        triggerCollider = child.GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void EnemyStompedBehaviour()
    {
        anim.SetTrigger("IsDead");
        audioSource.Play();
        DisableObject();
    }

    void DisableObject()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        objectCollider.enabled = false;
        triggerCollider.enabled = false;
    }

    private void Event_Destroy()
    {
        Destroy(gameObject);
    }

}
