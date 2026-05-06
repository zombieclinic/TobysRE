using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class titleUi : MonoBehaviour
{

    public UIDocument uiDocument;


    private Button startButton;
    private Button exitButton;
    
    [SerializeField] private GameObject ButtonEffect;
    [SerializeField] private AudioSource ButtonSound;
    void Start()
    {
        var root = uiDocument.rootVisualElement;
        startButton = root.Q<Button>("startLabel");
        exitButton = root.Q<Button>("exitLabel");

        startButton.clicked += OnStartClicked;
        exitButton.clicked += OnExitClicked;

        
    }
private void OnStartClicked()
    {
        PlayEffect();
      
        Invoke(nameof(LoadGame), 3f);
        
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnExitClicked()
    {

        PlayEffect();
        Invoke(nameof(LoadQuit), 3f);
        
    }

    private void LoadQuit()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

    private void PlayEffect()
{
    if (ButtonEffect == null) return;
    if (ButtonSound != null)
    {
        ButtonSound.Play();
    }

    ParticleSystem[] particles = ButtonEffect.GetComponentsInChildren<ParticleSystem>();

    foreach (var ps in particles)
    {
        ps.Play();
    }
}

}

