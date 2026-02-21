using UnityEngine;

public class mainEntranceDoor : MonoBehaviour, IInteractable
{

    public GameObject door;

   



    public void Interact(PlayerController player)
    {
        if( player.redKeyCard && player.yellowKeyCard && player.blueKeyCard)
        {
            openDoor();
        }
    }

    void openDoor()
        {
            door.SetActive(false);
        }
}

