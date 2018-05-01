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
        switch(musicName)
        {
            case MusicEnum.main:
                PlayAudio(0, startFromBeginning);
                break;
            case MusicEnum.hurry:
                PlayAudio(1, startFromBeginning);
                break;
            case MusicEnum.invincibility:
                PlayAudio(2, startFromBeginning);
                break;
            case MusicEnum.death:
                PlayAudio(3, startFromBeginning);
                break;
            case MusicEnum.underground:
                PlayAudio(4, startFromBeginning);
                break;
            case MusicEnum.stageCleared:
                PlayAudio(5, startFromBeginning);
                break;
            case MusicEnum.gameOver:
                PlayAudio(6, startFromBeginning);
                break;
            default:
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

    public float GetMusicLength(MusicEnum musicName)
    {
        switch (musicName)
        {
            case MusicEnum.main:
                return audioSources[0].clip.length;
            case MusicEnum.hurry:
                return audioSources[1].clip.length;
            case MusicEnum.invincibility:
                return audioSources[2].clip.length;
            case MusicEnum.death:
                return audioSources[3].clip.length;
            case MusicEnum.underground:
                return audioSources[4].clip.length;
            case MusicEnum.stageCleared:
                return audioSources[5].clip.length;
            case MusicEnum.gameOver:
                return audioSources[6].clip.length;
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

}
