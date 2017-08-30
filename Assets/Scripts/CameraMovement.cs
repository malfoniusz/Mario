using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float minDistance;

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        float dist = transform.position.x - player.position.x;

        if (dist < minDistance)
        {
            transform.position = new Vector3(player.position.x + minDistance, transform.position.y, transform.position.z);
        }
    }

}
