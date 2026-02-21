using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class howToPlay : MonoBehaviour
{

    private UIDocument uiDocument;


    private Button startButton;

    void Start()
    {
       
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
       
        startButton = root.Q<Button>("startButtonLabel");

        
        startButton.clicked += OnStartClicked;

        
    }



    private void OnStartClicked()
    {
         
        SceneManager.LoadScene("GameScene");
    }


}