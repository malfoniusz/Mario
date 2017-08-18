using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float minDistance;
    public float smoothing;

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
            float desired = Mathf.Lerp(transform.position.x, player.position.x + minDistance, smoothing * Time.deltaTime);
            transform.position = new Vector3(desired, transform.position.y, transform.position.z);
        }
    }

}
