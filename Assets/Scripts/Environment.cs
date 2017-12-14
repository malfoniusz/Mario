using UnityEngine;

public class Environment : MonoBehaviour
{
    // Powody uzycia kilku audioSource'ow zamiast clip'ow
    // 1. Mozliwosc latwego ustawienie opcji kazdego audioSource'a
    // 2. Kazda oddzielna muzyka po zatrzymaniu moze byc pozniej wznowiona
    public AudioSource[] audioSources;

    private int curAudioIndex = 0;

    public void PlayMain(bool reset)
    {
        PlayAudio(audioSources[0], reset);
        curAudioIndex = 0;
    }

    public void PlayHurry(bool reset)
    {
        PlayAudio(audioSources[1], reset);
        curAudioIndex = 1;
    }

    public void PlayInvincibility(bool reset)
    {
        PlayAudio(audioSources[2], reset);
        curAudioIndex = 2;
    }

    public void PlayDeath(bool reset)
    {
        PlayAudio(audioSources[3], reset);
        curAudioIndex = 3;
    }

    private void PlayAudio(AudioSource audioSource, bool reset)
    {
        audioSources[curAudioIndex].Pause();

        if (reset) audioSource.Stop();

        audioSource.Play();
    }

    public void PauseCurrentMusic()
    {
        audioSources[curAudioIndex].Pause();
    }

    public void PlayCurrentMusic()
    {
        audioSources[curAudioIndex].Play();
    }

}
