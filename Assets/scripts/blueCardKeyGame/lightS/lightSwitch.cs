using UnityEngine;
using System.Collections;

public class lightSwitch : MonoBehaviour, IInteractable
{
    public Sprite on;
    public Sprite off;
    private SpriteRenderer sr;

    private bool isOn = false;

    public GameObject Light;

    [Header("Timer")]
    public int timer = 15;

    [Header("Flicker")]
    public float flickerSpeed = 0.2f; // public flicker control
    public int flickerStartTime = 5;  // when flicker begins

    private Coroutine timerRoutine;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(PlayerBrain player)
    {
        isOn = !isOn;
        sr.sprite = isOn ? on : off;

        if (isOn)
        {
            Light.SetActive(true);

            if (timerRoutine != null)
                StopCoroutine(timerRoutine);

            timerRoutine = StartCoroutine(LightTimer());
        }
        else
        {
            Light.SetActive(false);

            if (timerRoutine != null)
                StopCoroutine(timerRoutine);
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
}