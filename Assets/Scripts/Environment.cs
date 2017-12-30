using UnityEngine;

public class Environment : MonoBehaviour
{
    // Powody uzycia kilku audioSource'ow zamiast clip'ow
    // 1. Mozliwosc latwego ustawienie opcji kazdego audioSource'a
    // 2. Kazda oddzielna muzyka po zatrzymaniu moze byc pozniej wznowiona
    public AudioSource[] audioSources;

    private int curAudioIndex = 0;

    public void Play(string musicName, bool reset)
    {
        if (musicName.Equals(MusicNames.main))
        {
            PlayAudio(audioSources[0], reset);
            curAudioIndex = 0;
        }
        else if (musicName.Equals(MusicNames.hurry))
        {
            PlayAudio(audioSources[1], reset);
            curAudioIndex = 1;
        }
        else if (musicName.Equals(MusicNames.invincibility))
        {
            PlayAudio(audioSources[2], reset);
            curAudioIndex = 2;
        }
        else if (musicName.Equals(MusicNames.death))
        {
            PlayAudio(audioSources[3], reset);
            curAudioIndex = 3;
        }
        else if (musicName.Equals(MusicNames.underground))
        {
            PlayAudio(audioSources[4], reset);
            curAudioIndex = 4;
        }
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
