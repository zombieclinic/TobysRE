using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LightToggle : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Light2D light2D;

    private PlayerInput playerInput;
    private InputAction attackAction;

    [Header("Fuel")]
    public float maxFuel = 100f;
    public float fuel = 100f;
    public float drainPerSecond = 5f;

    [Header("State")]
    public bool isOn = false;

    [Header("Toby")]
    public GameObject walkSound;



  


    
    private CircleCollider2D lightTrigger;
      private PlayerController pc;

    void Awake()
    {
        isOn = false;
        pc = GetComponent<PlayerController>();

        lightTrigger = GetComponent<CircleCollider2D>();
        playerInput = GetComponent<PlayerInput>();

        if (light2D == null)
            light2D = GetComponentInChildren<Light2D>(true);
    }

    IEnumerator Start()
    {
        yield return null;

        attackAction = playerInput.currentActionMap.FindAction("Attack", true);
        attackAction.performed += OnAttack;
        attackAction.Enable();

        ApplyLightState();
    }

    void OnDisable()
    {
        if (attackAction != null)
        {
            attackAction.performed -= OnAttack;
            attackAction.Disable();
        }
    }

    void Update()
    {
        if (!isOn) return;

        fuel -= drainPerSecond * Time.deltaTime;

        if (fuel <= 0f)
        {
            fuel = 0f;
            isOn = false;
            ApplyLightState();   
             walkSound.SetActive(true);
            StartCoroutine(gameOverMan());        
        }
        
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        // Don't turn on if no fuel
        if (!isOn && fuel <= 0f)
            return;

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

    if (other.CompareTag("teddy") && pc != null)
    {
        pc.noiseLevel = pc.maxNoise;
    }
}

private void OnTriggerStay2D(Collider2D other)
{
    if (!isOn) return;

    if (other.CompareTag("teddy") && pc != null)
    {
        pc.noiseLevel = pc.maxNoise;
    }
}

IEnumerator gameOverMan()
{
        yield return new WaitForSecondsRealtime(15f);
        SceneManager.LoadScene("GameOver");}

}
