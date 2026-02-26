using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueKeyCard : MonoBehaviour, IInteractable
{
    public GameObject player;
    public GameObject MainCamera;
    public GameObject CineMachine;
    public void Interact(PlayerController player)
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(MainCamera);
        DontDestroyOnLoad(CineMachine);
        SceneManager.LoadScene("blueCardKeyGame");
        
    }
}
