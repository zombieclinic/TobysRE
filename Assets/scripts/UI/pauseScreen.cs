using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class PauseScreen : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button exitButton;
    private Slider masterSlider;
    private Slider SFXSlider;
    private Slider MusicSlider;

    [SerializeField] private SoundMixerManager mixer;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        exitButton = root.Q<Button>("exitLabel");
        masterSlider = root.Q<Slider>("MasterVolumeSlider");
        SFXSlider = root.Q<Slider>("SFXVolumeSlider");
        MusicSlider = root.Q<Slider>("MusicVolumeSlider");

        if (exitButton != null)
        {
            exitButton.clicked -= OnExitClicked;
            exitButton.clicked += OnExitClicked;
        }

        if (masterSlider != null)
        {
            masterSlider.UnregisterValueChangedCallback(OnMasterVolumeChanged);
            masterSlider.RegisterValueChangedCallback(OnMasterVolumeChanged);
        }

        if (SFXSlider != null)
        {
            SFXSlider.UnregisterValueChangedCallback(OnSFXVolumeChanged);
            SFXSlider.RegisterValueChangedCallback(OnSFXVolumeChanged);
        }
        if (MusicSlider != null)
        {
            MusicSlider.UnregisterValueChangedCallback(OnMusicChanged);
            MusicSlider.RegisterValueChangedCallback(OnMusicChanged);
        }
    }

    private void OnDisable()
    {
        if (exitButton != null)
            exitButton.clicked -= OnExitClicked;

        if (masterSlider != null)
            masterSlider.UnregisterValueChangedCallback(OnMasterVolumeChanged);

        if (SFXSlider != null)
            SFXSlider.UnregisterValueChangedCallback(OnMasterVolumeChanged);
        
        if (MusicSlider != null)
            MusicSlider.UnregisterValueChangedCallback(OnMusicChanged);
    }

    private void OnMasterVolumeChanged(ChangeEvent<float> evt)
    {
        mixer.SetMasterVolume(evt.newValue);
    }

    private void OnSFXVolumeChanged(ChangeEvent<float> evt)
    {
        mixer.SetSoundFXVolume(evt.newValue);
    }
    
    private void OnMusicChanged(ChangeEvent<float> evt)
    {
        mixer.SetMusicVolume(evt.newValue);
    }

    private void OnExitClicked()
    {
        StartCoroutine(QuitAfterDelay());
    }

    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        LoadQuit();
    }

    private void LoadQuit()
    {
        Time.timeScale = 1f;
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}