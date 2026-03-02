using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    [Header("Noise Limits")]
    [SerializeField] private float maxNoise = 100f;

    [Header("Decay")]
    [SerializeField] private float decayPerSecond = 15f;

    [Header("Movement Noise Gain")]
    [SerializeField] private float moveNoisePerSecond = 10f;
    [SerializeField] private float sprintNoisePerSecond = 20f;

    [Header("Debug (Read Only)")]
    [SerializeField] private float currentNoise; // <- visible in Inspector

    public float CurrentNoise => currentNoise;
    public float MaxNoise => maxNoise;

    private void Update()
    {
        if (currentNoise > 0f)
        {
            currentNoise -= decayPerSecond * Time.deltaTime;
            currentNoise = Mathf.Clamp(currentNoise, 0f, maxNoise);
        }
    }

    public void AddMovementNoise(bool sprinting, float deltaTime)
    {
        float gain = sprinting ? sprintNoisePerSecond : moveNoisePerSecond;
        currentNoise = Mathf.Clamp(currentNoise + gain * deltaTime, 0f, maxNoise);
    }

    public void AddNoise(float amount)
    {
        currentNoise = Mathf.Clamp(currentNoise + amount, 0f, maxNoise);
    }

    public void SetNoiseToMax() => currentNoise = maxNoise;
    public void ClearNoise() => currentNoise = 0f;
}