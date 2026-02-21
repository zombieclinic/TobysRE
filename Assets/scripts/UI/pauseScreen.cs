using UnityEngine;
using UnityEngine.UIElements;

public class pauseScreen : MonoBehaviour
{

    private UIDocument uiDocument;


    private Button exitButton;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
       
        exitButton = root.Q<Button>("exitLabel");

        
        exitButton.clicked += OnExitClicked;

        
    }



    private void OnExitClicked()
    {
       
        Invoke(nameof(LoadQuit), 1f);
       
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