using UnityEngine;

public class yellowKeyCard : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        player.GiveYellowKey();
        Destroy(gameObject);
    }
}
