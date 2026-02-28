using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;

public class LightToggle : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Light2D light2D;

    [Header("Fuel")]
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float fuel = 100f;
    [SerializeField] private float drainPerSecond = 5f;

    [Header("State")]
    [SerializeField] private bool isOn = false;

    [Header("Toby")]
    [SerializeField] private GameObject walkSound;

    private CircleCollider2D lightTrigger;
    private PlayerNoise noise;

    private void Awake()
    {
        noise = GetComponent<PlayerNoise>();
        lightTrigger = GetComponent<CircleCollider2D>();

        if (light2D == null)
            light2D = GetComponentInChildren<Light2D>(true);

        isOn = false;
        ApplyLightState();
    }

    private void Update()
    {
        if (!isOn) return;

        fuel -= drainPerSecond * Time.deltaTime;

        if (fuel <= 0f)
        {
            fuel = 0f;
            isOn = false;
            ApplyLightState();

            if (walkSound != null)
                walkSound.SetActive(true);

            StartCoroutine(GameOverMan());
        }
    }

    // ✅ Called by PlayerBrain when Attack is pressed
    public void Toggle()
    {
        // Don't turn on if no fuel
        if (!isOn && fuel <= 0f) return;

        isOn = !isOn;
        ApplyLightState();
    }

    private void ApplyLightState()
    {
        if (light2D != null)
            light2D.enabled = isOn;

        if (lightTrigger != null)
            lightTrigger.enabled = isOn;
    }

    public void AddFuel(float amount)
    {
        fuel = Mathf.Clamp(fuel + amount, 0f, maxFuel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOn) return;

        if (other.CompareTag("teddy"))
            noise?.SetNoiseToMax();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isOn) return;

        if (other.CompareTag("teddy"))
            noise?.SetNoiseToMax();
    }

    private IEnumerator GameOverMan()
    {
        yield return new WaitForSecondsRealtime(15f);
        SceneManager.LoadScene("GameOver");
    }
}