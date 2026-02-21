using UnityEngine;

public class RedKeyCard : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        player.GiveRedKey();
        Destroy(gameObject);
    }
}
