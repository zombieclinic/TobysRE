using UnityEngine;

public class BatteryPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private float fuelAmount = 50f;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;

    private int batteryIndex = -1;

    public void SetBatteryIndex(int index)
    {
        batteryIndex = index;
    }

    public void Interact(PlayerBrain player)
    {
       if (pickupEffect != null)
{
    Instantiate(pickupEffect, transform.position, Quaternion.identity);
}

        LightToggle light = player.GetComponent<LightToggle>();

        if (light != null)
        {
            light.AddFuel(fuelAmount);
        }

        if (GameManager.Instance != null && batteryIndex != -1)
        {
            GameManager.Instance.MarkBatteryPickedUp(batteryIndex);
        }

        if (SoundEffectManager.instance != null && pickupSound != null)
        {
            SoundEffectManager.instance.PlaySoundFXClip(pickupSound, transform, 1f);
        }

        Destroy(gameObject);
    }
}