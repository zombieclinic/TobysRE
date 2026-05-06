using UnityEngine;

public class BatteryPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private float fuelAmount = 50f;
    [SerializeField] private AudioClip pickupSound;
<<<<<<< HEAD
    [SerializeField] private GameObject pickupEffect;
=======
  
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f

    private int batteryIndex = -1;

    public void SetBatteryIndex(int index)
    {
        batteryIndex = index;
    }

    public void Interact(PlayerBrain player)
    {
<<<<<<< HEAD
       if (pickupEffect != null)
{
    Instantiate(pickupEffect, transform.position, Quaternion.identity);
}

=======
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f
        LightToggle light = player.GetComponent<LightToggle>();

        if (light != null)
        {
            light.AddFuel(fuelAmount);
        }

<<<<<<< HEAD
=======
        // mark as picked up
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f
        if (GameManager.Instance != null && batteryIndex != -1)
        {
            GameManager.Instance.MarkBatteryPickedUp(batteryIndex);
        }
<<<<<<< HEAD

        if (SoundEffectManager.instance != null && pickupSound != null)
        {
            SoundEffectManager.instance.PlaySoundFXClip(pickupSound, transform, 1f);
        }
=======
         SoundEffectManager.instance.PlaySoundFXClip(pickupSound, transform, 1f);
      
        
>>>>>>> 9df20c9712e323a21c46916fb447b423ef3d4c7f

        Destroy(gameObject);
    }
}