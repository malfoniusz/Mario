using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public Animator anim;
    public GameObject fireballSpawn;
    public GameObject fireball;

    private PlayerPowerup playerPowerup;

    private void Awake()
    {
        playerPowerup = GetComponent<PlayerPowerup>();
    }

    private void Update()
    {
        if (playerPowerup.level == 3 && Input.GetButtonDown("Run") && Fireball.numberOfFireballs < Fireball.MAX_FIREBALLS)
        {
            Instantiate(fireball, fireballSpawn.transform.position, Quaternion.identity);
            anim.SetTrigger("FireballShot");
        }
    }

}
