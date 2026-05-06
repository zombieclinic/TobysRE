using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] private bool loop = true;
    [SerializeField] private float volume = 1f;

    void Start()
    {
        if (MusicManager.instance != null)
            MusicManager.instance.PlayMusic(music, loop, volume);
    }
}