using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueKeyCard : MonoBehaviour, IInteractable
{
[SerializeField] private AudioClip pickupSound;
private bool soundPlaying = false; 
 public void Interact(PlayerBrain player)
    {
        player.GiveBlueKey();
        Destroy(gameObject);

        if(!soundPlaying)
        {
            SoundEffectManager.instance.PlaySoundFXClip(pickupSound, transform, 1f);
            soundPlaying = true;
        }
    }
}
