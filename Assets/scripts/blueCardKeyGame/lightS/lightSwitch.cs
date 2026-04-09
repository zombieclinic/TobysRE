using UnityEngine;
using System.Collections;

public class lightSwitch : MonoBehaviour, IInteractable
{
    public Sprite on;
    public Sprite off;
    private SpriteRenderer sr;
    [SerializeField] private AudioClip switchSound;

    private bool isOn = false;

    public GameObject Light;

    [Header("Timer")]
    public int timer = 15;

    [Header("Flicker")]
    public float flickerSpeed = 0.2f; // public flicker control
    public int flickerStartTime = 5;  // when flicker begins

    private Coroutine timerRoutine;

    [Header("NoiseGain")]
    [SerializeField] private float noiseAmount = 20f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(PlayerBrain player)
{
    isOn = !isOn;
    sr.sprite = isOn ? on : off;

    var noise = player.GetComponent<PlayerNoise>();
    if (noise != null)
    {
        noise.AddNoise(noiseAmount);
    }

    if (isOn)
    {
        Light.SetActive(true);

        if (timerRoutine != null)
            StopCoroutine(timerRoutine);

        timerRoutine = StartCoroutine(LightTimer());
        PlaySwitchSound();
    }
    else
    {
        Light.SetActive(false);

        if (timerRoutine != null)
            StopCoroutine(timerRoutine);

        PlaySwitchSound();
    }
}

    IEnumerator LightTimer()
    {
        float timeLeft = timer;

        while (timeLeft > 0)
        {
            if (timeLeft <= flickerStartTime)
            {
                // flicker
                Light.SetActive(!Light.activeSelf);
                yield return new WaitForSeconds(flickerSpeed);
                timeLeft -= flickerSpeed;
            }
            else
            {
                yield return new WaitForSeconds(1f);
                timeLeft -= 1f;
            }
        }

      

        // shut off
        isOn = false;
        sr.sprite = off;
        Light.SetActive(false);
    }
     private void PlaySwitchSound()
    {
        if (switchSound == null) return;
        if (SoundEffectManager.instance == null) return;

        SoundEffectManager.instance.PlaySoundFXClip(switchSound, transform, 1f);
    }
}