using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueKeyCard : MonoBehaviour, IInteractable
{
    private GameObject Player;
    private GameObject MainCamera;
    private GameObject CineMachine;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        CineMachine = GameObject.FindGameObjectWithTag("CineMachine");
    }
    public void Interact(PlayerBrain player)
    {
        
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(MainCamera);
        DontDestroyOnLoad(CineMachine);
        SceneManager.LoadScene("blueCardKeyGame");
        
    }
}
