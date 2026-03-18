using UnityEngine;

public class battery : MonoBehaviour, IInteractable
{

    [SerializeField] private float fuelAmount = 50f;
 public void Interact(PlayerBrain player)
    {
        LightToggle light = player.GetComponent<LightToggle>();
        if (light != null)
        {
            light.AddFuel(fuelAmount);
        }

        Destroy(gameObject);
    }
}