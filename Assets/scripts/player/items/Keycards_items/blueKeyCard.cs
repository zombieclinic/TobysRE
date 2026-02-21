using UnityEngine;

public class BlueKeyCard : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        player.GiveBlueKey();
        Destroy(gameObject);
    }
}
