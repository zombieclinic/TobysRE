using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Rendering.Universal;

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
    public bool isOn = true;

    void Awake()
    {
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
    }

    public void AddFuel(float amount)
    {
        fuel = Mathf.Clamp(fuel + amount, 0f, maxFuel);
    }
}
