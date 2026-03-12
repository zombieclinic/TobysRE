using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioMixerGroup musicMixerGroup;

    private AudioSource currentMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource PlayMusic(AudioClip clip, bool loop, float volume = 1f)
    {
        if (clip == null) return null;

        StopMusic();

        GameObject musicObject = new GameObject("MusicTrack");
        musicObject.transform.parent = transform;

        AudioSource source = musicObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.spatialBlend = 0f; 

        if (musicMixerGroup != null)
            source.outputAudioMixerGroup = musicMixerGroup;

        source.Play();

        if (loop)
        {
            currentMusic = source;
        }
        else
        {
            Destroy(musicObject, clip.length);
        }

        return source;
    }

    public AudioSource PlayRandomMusic(AudioClip[] clips, bool loop, float volume = 1f)
    {
        if (clips == null || clips.Length == 0) return null;

        int rand = Random.Range(0, clips.Length);
        return PlayMusic(clips[rand], loop, volume);
    }

    public void StopMusic()
    {
        if (currentMusic != null)
        {
            Destroy(currentMusic.gameObject);
            currentMusic = null;
        }
    }
}