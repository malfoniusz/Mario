using System.Collections;
using UnityEngine;

public class End : MonoBehaviour
{
    public Transform spawnPointsPos;
    public AudioSource audioFlagpole;
    public float pointsRiseDistance = 135;
    public float pointsRiseTimeInSeconds = 1;

    private Environment environment;

    private void Start()
    {
        environment = TagNames.GetEnvironment().GetComponent<Environment>();
    }

    public IEnumerator ClearStage(int flagpolePoints, bool extraLife)
    {
        SpawnPointsFloating.Flagpole(spawnPointsPos.position, flagpolePoints, extraLife, pointsRiseDistance, pointsRiseTimeInSeconds);

        environment.PauseCurrentMusic();
        audioFlagpole.Play();

        yield return new WaitForSeconds(audioFlagpole.clip.length);


        yield return null;
    }

}
