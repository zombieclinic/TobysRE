using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    [Header("Noise Settings")]
    [SerializeField] private float maxNoise = 100f;
    [SerializeField] private float decayPerSecond = 15f;

    public float CurrentNoise { get; private set; }

    public float MaxNoise => maxNoise;

    void Update()
    {
        // Always decay over time
        if (CurrentNoise > 0f)
        {
            CurrentNoise -= decayPerSecond * Time.deltaTime;
            CurrentNoise = Mathf.Clamp(CurrentNoise, 0f, maxNoise);
        }
    }

    public void AddNoise(float amount)
    {
        CurrentNoise = Mathf.Clamp(CurrentNoise + amount, 0f, maxNoise);
    }

    public void SetNoiseToMax()
    {
        CurrentNoise = maxNoise;
    }

    public void ClearNoise()
    {
        CurrentNoise = 0f;
    }
}