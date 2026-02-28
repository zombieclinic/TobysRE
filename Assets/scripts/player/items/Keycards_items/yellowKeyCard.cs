using UnityEngine;

public class yellowKeyCard : MonoBehaviour, IInteractable
{
    public void Interact(PlayerBrain player)
    {
        player.GiveYellowKey();
        Destroy(gameObject);
    }
}
