using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public static SoundEffectManager instance;
[SerializeField] private AudioSource soundFXObject;

    void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
       AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

       audioSource.clip = audioClip;

       audioSource.volume = volume;

       audioSource.Play();

       float clipLength = audioSource.clip.length;

       Destroy(audioSource.gameObject, clipLength);
    }

        public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {

    int rand = Random.Range(0, audioClip.Length);
       AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

       audioSource.clip = audioClip[rand];

       audioSource.volume = volume;

       audioSource.Play();

       float clipLength = audioSource.clip.length;

       Destroy(audioSource.gameObject, clipLength);
    }
}
