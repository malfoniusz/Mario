using UnityEngine;
using System.Collections.Generic;

public class ActiveObjects : MonoBehaviour
{
    public float refreshTime = 1f;

    private List<GameObject> objects = new List<GameObject>();
    private Vector2 CAM_EXPAND = new Vector2(1.0f, 0.1f);
    private Vector2 CAM_VIEW_MIN;
    private Vector2 CAM_VIEW_MAX;
    private float time;

    private void Start()
    {
        objects.AddRange(TagNames.GetBlocks());
        objects.AddRange(TagNames.GetEnemies());
        objects.AddRange(TagNames.GetItems());
        objects.AddRange(TagNames.GetCoins());
        objects.AddRange(TagNames.GetPipes());
        objects.AddRange(TagNames.GetBackgrounds());

        CAM_VIEW_MIN = Vector2.zero - CAM_EXPAND;
        CAM_VIEW_MAX = Vector2.one + CAM_EXPAND;

        time = refreshTime; // Aby pierwszy Update sie wykonal
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > refreshTime)
        {
            time = 0;
            ControlActiveObjects();
        }
    }

    private void ControlActiveObjects()
    {
        objects.RemoveAll(obj => obj == null);
        foreach (GameObject o in objects)
        {
            Active(o);
        }
    }

    private void Active(GameObject gameObject)
    {
        Transform transform = gameObject.transform;
        Vector3 objPos = Camera.main.WorldToViewportPoint(transform.position);

        bool camVis = (objPos.x >= CAM_VIEW_MIN.x && objPos.x <= CAM_VIEW_MAX.x) && (objPos.y >= CAM_VIEW_MIN.y && objPos.y <= CAM_VIEW_MAX.y);

        if (camVis) gameObject.SetActive(true);
        else        gameObject.SetActive(false);
    }

}
