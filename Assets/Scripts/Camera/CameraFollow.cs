using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minDistance = 90;

    private Transform player;
    private bool pause = false;
    private ActiveObjects activeObjects;

    private void Awake()
    {
        player = TagNames.GetPlayer().transform;
        activeObjects = GetComponent<ActiveObjects>();
    }

    private void LateUpdate()
    {
        if (!pause) Follow();
    }

    private void Follow()
    {
        float dist = transform.position.x - player.position.x;

        if (dist < minDistance)
        {
            transform.position = new Vector3(player.position.x + minDistance, transform.position.y, transform.position.z);
        }
    }

    public void Pause(bool pause)
    {
        this.pause = pause;
    }

    public void UpdateMinDistanceToCurrent()
    {
        minDistance = transform.position.x - player.position.x;
    }

    public void Refresh()
    {
        Follow();
        activeObjects.Refresh();
    }

}
