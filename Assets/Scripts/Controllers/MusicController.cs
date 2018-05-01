using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Powody uzycia kilku audioSource'ow zamiast clip'ow
    // 1. Mozliwosc latwego ustawienie opcji kazdego audioSource'a
    // 2. Kazda oddzielna muzyka po zatrzymaniu moze byc pozniej wznowiona
    public AudioSource[] audioSources;

    private int curAudioIndex = 0;

    public void Play(string musicName, bool startFromBeginning)
    {
        if (musicName.Equals(MusicNames.main))
        {
            PlayAudio(0, startFromBeginning);
        }
        else if (musicName.Equals(MusicNames.hurry))
        {
            PlayAudio(1, startFromBeginning);
        }
        else if (musicName.Equals(MusicNames.invincibility))
        {
            PlayAudio(2, startFromBeginning);
        }
        else if (musicName.Equals(MusicNames.death))
        {
            PlayAudio(3, startFromBeginning);
        }
        else if (musicName.Equals(MusicNames.underground))
        {
            PlayAudio(4, startFromBeginning);
        }
        else if (musicName.Equals(MusicNames.stageCleared))
        {
            PlayAudio(5, startFromBeginning);
        }
    }

    private void PlayAudio(int audioIndex, bool startFromBeginning)
    {
        audioSources[curAudioIndex].Pause();    // Zatrzymanie starej muzyki

        curAudioIndex = audioIndex;
        if (startFromBeginning) audioSources[audioIndex].Stop();
        audioSources[audioIndex].Play();
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
