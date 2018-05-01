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
        else if (musicName.Equals(MusicNames.gameOver))
        {
            PlayAudio(6, startFromBeginning);
        }
        else
        {
            throw new System.Exception("No such music exists.");
        }
    }

    private void PlayAudio(int audioIndex, bool startFromBeginning)
    {
        audioSources[curAudioIndex].Pause();    // Zatrzymanie starej muzyki

        curAudioIndex = audioIndex;
        if (startFromBeginning) audioSources[audioIndex].Stop();
        audioSources[audioIndex].Play();
    }

    public float GetMusicLength(string musicName)
    {
        if (musicName.Equals(MusicNames.main))
        {
            return audioSources[0].clip.length;
        }
        else if (musicName.Equals(MusicNames.hurry))
        {
            return audioSources[1].clip.length;
        }
        else if (musicName.Equals(MusicNames.invincibility))
        {
            return audioSources[2].clip.length;
        }
        else if (musicName.Equals(MusicNames.death))
        {
            return audioSources[3].clip.length;
        }
        else if (musicName.Equals(MusicNames.underground))
        {
            return audioSources[4].clip.length;
        }
        else if (musicName.Equals(MusicNames.stageCleared))
        {
            return audioSources[5].clip.length;
        }
        else if (musicName.Equals(MusicNames.gameOver))
        {
            return audioSources[6].clip.length;
        }
        else
        {
            throw new System.Exception("No such music exists.");
        }
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
