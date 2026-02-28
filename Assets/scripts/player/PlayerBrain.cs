using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(PlayerInput))]
public class PlayerBrain : MonoBehaviour
{
    [Header("Keycards")]
    public bool redKeyCard, yellowKeyCard, blueKeyCard;

    [Header("T.O.b.Y Base Chance")]
    public int teddyChance = 0;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;

    // Read-only 
    public Vector2 MoveInput { get; private set; }
    public bool SprintHeld { get; private set; }
    public bool JumpPressed { get; private set; } 

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction interactAction;
    private InputAction pauseAction;
    private InputAction attackAction;
    private LightToggle lightToggle;

    private PlayerInteractor interactor;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactor = GetComponent<PlayerInteractor>();
        lightToggle = GetComponent<LightToggle>();
    }

    private void OnEnable()
    {
        StartCoroutine(BindInputNextFrame());
    }

    private IEnumerator BindInputNextFrame()
    {
        yield return null; 

        // Action Map
        playerInput.ActivateInput();
        playerInput.SwitchCurrentActionMap("Player"); 

        // Find Input Actions
        moveAction = playerInput.currentActionMap.FindAction("Move", true);
        sprintAction = playerInput.currentActionMap.FindAction("Sprint", true);
        interactAction = playerInput.currentActionMap.FindAction("Interact", true);
        pauseAction = playerInput.currentActionMap.FindAction("Pause", true);
        attackAction = playerInput.currentActionMap.FindAction("Attack", true);

        
        Unbind();

        
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        moveAction.Enable();

        sprintAction.performed += OnSprint;
        sprintAction.canceled += OnSprint;
        sprintAction.Enable();

        interactAction.performed += OnInteract;
        interactAction.Enable();

        pauseAction.performed += OnPause;
        pauseAction.Enable();

        attackAction.performed += OnAttack;
        attackAction.Enable();
    }

    private void OnDisable()
    {
        Unbind();
    }

    private void Unbind()
    {
        if (moveAction != null)
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();
        }

        if (sprintAction != null)
        {
            sprintAction.performed -= OnSprint;
            sprintAction.canceled -= OnSprint;
            sprintAction.Disable();
        }

        if (interactAction != null)
        {
            interactAction.performed -= OnInteract;
            interactAction.Disable();
        }

        if (pauseAction != null)
        {
            pauseAction.performed -= OnPause;
            pauseAction.Disable();
        }
    }


    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnSprint(InputAction.CallbackContext ctx)
    {
        SprintHeld = ctx.ReadValue<float>() > 0.1f;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        interactor?.TryInteract(this);
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        TogglePause();
    }

    public void TogglePause()
    {
        bool paused = Time.timeScale == 0f;
        Time.timeScale = paused ? 1f : 0f;

        if (pauseMenu != null)
            pauseMenu.SetActive(!paused);
    }

    private void OnAttack(InputAction.CallbackContext ctx)
{
    if (!ctx.performed) return;
    lightToggle?.Toggle();
}

    public void GiveRedKey() => redKeyCard = true;
    public void GiveYellowKey() => yellowKeyCard = true;
    public void GiveBlueKey() => blueKeyCard = true;
}