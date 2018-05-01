using UnityEngine;

public class SpawnPointsFloating : MonoBehaviour
{
    public static PointsFloating Points(Vector3 pos, int points, bool extraLife)
    {
        PointsFloating pointsFloating = CreatePoints(pos);
        pointsFloating.SetPointsAndExtraLife(points, extraLife);
        return pointsFloating;
    }

    public static void Flagpole(Vector3 pos, int points, bool extraLife, float riseDistance, float riseTimeInSeconds)
    {
        PointsFloating pointsFloating = Points(pos, points, extraLife);

        pointsFloating.SetExtraLifePlaySound(false);
        pointsFloating.SetRiseDistance(riseDistance);
        pointsFloating.SetRiseTimeInSeconds(riseTimeInSeconds);
        pointsFloating.SetDeleteAfterReachingEnd(false);
        pointsFloating.SetAddPointsAtEnd(true);
    }

    private static PointsFloating CreatePoints(Vector3 pos)
    {
        GameObject pointsObject = (GameObject)Instantiate(Resources.Load(ResourcesNames.pointsFloating), pos, Quaternion.identity);
        PointsFloating pointsFloating = pointsObject.GetComponent<PointsFloating>();
        return pointsFloating;
    }

}
