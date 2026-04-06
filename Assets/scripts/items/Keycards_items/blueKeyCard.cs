using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueKeyCard : MonoBehaviour, IInteractable
{
    
 public void Interact(PlayerBrain player)
    {
        player.GiveBlueKey();
        Destroy(gameObject);
    }
}
