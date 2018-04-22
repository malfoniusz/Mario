using UnityEngine;

public class SpawnPointsFloating : MonoBehaviour
{
    public static void Points(Vector3 pos, int points)
    {
        GameObject pointsObject = SetPos(pos);
        pointsObject.GetComponent<PointsFloating>().SetPoints(points);
    }

    public static void ExtraLife(Vector3 pos)
    {
        GameObject pointsObject = SetPos(pos);
        pointsObject.GetComponent<PointsFloating>().SetExtraLife(true);
    }

    public static void PointsAndExtraLife(Vector3 pos, int points)
    {
        GameObject pointsObject = SetPos(pos);
        pointsObject.GetComponent<PointsFloating>().SetPointsAndExtraLife(points, true);
    }

    private static GameObject SetPos(Vector3 pos)
    {
        GameObject pointsObject = (GameObject)Instantiate(Resources.Load(ResourcesNames.pointsFloating));
        pointsObject.transform.GetChild(0).position = pos;
        return pointsObject;
    }

}
