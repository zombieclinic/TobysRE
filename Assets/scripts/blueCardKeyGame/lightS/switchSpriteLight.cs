
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class switchSpriteLight : MonoBehaviour
{
    private Light2D blink;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 2f;

    void Awake()
    {
        blink = GetComponent<Light2D>();
    }
   

   
    void Update()
    {
        float pulse = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f;
        blink.intensity = Mathf.Lerp(minIntensity, maxIntensity, pulse);
    }
}
