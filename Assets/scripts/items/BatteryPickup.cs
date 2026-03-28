using UnityEngine;

public class BatteryPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private float fuelAmount = 50f;

    private int batteryIndex = -1;

    public void SetBatteryIndex(int index)
    {
        batteryIndex = index;
    }

    public void Interact(PlayerBrain player)
    {
        LightToggle light = player.GetComponent<LightToggle>();

        if (light != null)
        {
            light.AddFuel(fuelAmount);
        }

        // mark as picked up
        if (GameManager.Instance != null && batteryIndex != -1)
        {
            GameManager.Instance.MarkBatteryPickedUp(batteryIndex);
        }

        Destroy(gameObject);
    }
}