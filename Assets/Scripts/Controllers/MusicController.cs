using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Powody uzycia kilku audioSource'ow zamiast clip'ow
    // 1. Mozliwosc latwego ustawienie opcji kazdego audioSource'a
    // 2. Kazda oddzielna muzyka po zatrzymaniu moze byc pozniej wznowiona
    public AudioSource[] audioSources;

    private int curAudioIndex = 0;

    public void Play(MusicEnum musicName, bool startFromBeginning)
    {
        int musicIndex = GetMusicIndex(musicName);
        PlayAudio(musicIndex, startFromBeginning);
    }

    private void PlayAudio(int audioIndex, bool startFromBeginning)
    {
        audioSources[curAudioIndex].Pause();    // Zatrzymanie starej muzyki

        curAudioIndex = audioIndex;
        if (startFromBeginning) audioSources[audioIndex].Stop();
        audioSources[audioIndex].Play();
    }

    public float GetMusicLength(MusicEnum musicName)
    {
        int musicIndex = GetMusicIndex(musicName);
        return audioSources[musicIndex].clip.length;
    }

    private int GetMusicIndex(MusicEnum musicName)
    {
        switch (musicName)
        {
            case MusicEnum.main:
                return 0;
            case MusicEnum.hurry:
                return 1;
            case MusicEnum.invincibility:
                return 2;
            case MusicEnum.death:
                return 3;
            case MusicEnum.underground:
                return 4;
            case MusicEnum.stageCleared:
                return 5;
            case MusicEnum.gameOver:
                return 6;
            default:
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

    public bool IsPlaying(MusicEnum musicName)
    {
        int musicIndex = GetMusicIndex(musicName);
        return audioSources[musicIndex].isPlaying;
    }

}
