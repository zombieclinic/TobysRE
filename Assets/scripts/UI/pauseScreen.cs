using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class pauseScreen : MonoBehaviour
{

    private UIDocument uiDocument;


    private Button exitButton;

    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
       
        exitButton = root.Q<Button>("exitLabel");

        exitButton.clicked -= OnExitClicked;
        exitButton.clicked += OnExitClicked;

        
    }



    private void OnExitClicked()
    {
       
        StartCoroutine(QuitAfterDelay());
       
    }

        private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f); // works even when timeScale = 0
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