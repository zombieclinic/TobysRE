using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class titleUi : MonoBehaviour
{

    public UIDocument uiDocument;

    private Button startButton;
    private Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
      
        Invoke(nameof(LoadGame), 1f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnExitClicked()
    {

        Invoke(nameof(LoadQuit), 1f);
    }

    private void LoadQuit()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

}