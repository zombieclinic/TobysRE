using UnityEngine;

public class RedKeyCard : MonoBehaviour, IInteractable
{
    public void Interact(PlayerBrain player)
    {
        player.GiveRedKey();
        Destroy(gameObject);
    }
}
