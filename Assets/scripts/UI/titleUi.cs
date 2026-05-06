using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class titleUi : MonoBehaviour
{

    public UIDocument uiDocument;

<<<<<<< HEAD

    private Button startButton;
    private Button exitButton;
    
    [SerializeField] private GameObject ButtonEffect;
    [SerializeField] private AudioSource ButtonSound;
=======
    private Button startButton;
    private Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f
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
<<<<<<< HEAD
        PlayEffect();
      
        Invoke(nameof(LoadGame), 3f);
        
=======
      
        Invoke(nameof(LoadGame), 1f);
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnExitClicked()
    {

<<<<<<< HEAD
        PlayEffect();
        Invoke(nameof(LoadQuit), 3f);
        
=======
        Invoke(nameof(LoadQuit), 1f);
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f
    }

    private void LoadQuit()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

<<<<<<< HEAD
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

=======
}
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f
