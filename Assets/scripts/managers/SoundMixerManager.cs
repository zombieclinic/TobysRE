using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
   [SerializeField] private AudioMixer audioMixer;
    private const string MasterKey = "masterVolume";
    private const string SFXKey = "soundVolume";
    private const string Musickey = "musicVolume";
    void Start()
    {
        LoadVolumes();       
    }
   public void SetMasterVolume(float level)
    {
        level = Mathf.Max(level, 0.0001f);
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(MasterKey, level);
    }

    public void SetSoundFXVolume(float level)
    {
        level = Mathf.Max(level, 0.0001f);
        audioMixer.SetFloat("soundVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(SFXKey, level);
    }

    public void SetMusicVolume(float level)
    {
        level = Mathf.Max(level, 0.0001f);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(Musickey, level);
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MasterKey, 1f);
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFXKey,1f);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(Musickey, 1f);
    }

    private void LoadVolumes()
    {
        SetMasterVolume(GetMasterVolume());
        SetSoundFXVolume(GetSFXVolume());
        SetMusicVolume(GetMusicVolume());
    }
}
