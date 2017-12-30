using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public Animator anim;
    public GameObject fireballSpawn;
    public GameObject fireball;

    private PlayerPowerup playerPowerup;
    private static bool stop = false;

    private void Awake()
    {
        playerPowerup = GetComponent<PlayerPowerup>();
    }

    private void Update()
    {
        if (stop)
        {
            return;
        }

        if (playerPowerup.GetLevel() == 3 && ButtonNames.RunPressed() && Fireball.numberOfFireballs < Fireball.MAX_FIREBALLS)
        {
            Instantiate(fireball, fireballSpawn.transform.position, Quaternion.identity);
            anim.SetTrigger(AnimatorNames.playerFireballShot);
        }
    }

    public static void Stop(bool value)
    {
        stop = value;
    }

}
