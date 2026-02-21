using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    [Header("moveSpeed")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float runSpeed = 10f;
   
    private Rigidbody2D rb;
    [Header("pauseMenu")]
    public GameObject PauseMenu;
    [Header("interactControls")]
    [SerializeField] private float interactRadius = 0.75f;
    [SerializeField] private LayerMask interactLayer;
    private IInteractable currentInteractable;


    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction pauseAction;
    private InputAction useAction;
   
    private bool isSprinting;
    private Vector2 moveInput;
    private Animator animator;

    [Header("T.O.b.YBaseChance")]
    public int teddyChance = 0;

    [Header("keycards")]

    public bool redKeyCard = false;
    public bool yellowKeyCard = false;
    public bool blueKeyCard = false;


    

    public int facingDirection = 1;

    [Header("Noise")]
    public float noiseLevel = 0f;
    public float nlps = 10f;
    public float nlds = 15f;
    public float SprintNlps = 15f;
    public float maxNoise = 100f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(EnableInputNextFrame());
    }

    IEnumerator EnableInputNextFrame()
    {
        yield return null; 

       
        if (moveAction != null)
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();
        }

        if (runAction !=null)
        {
            runAction.performed -= OnSprint;
            runAction.canceled -= OnSprint;
            runAction.Disable();
        }

        
        if (useAction !=null)
        {
            useAction.performed -= OnUse;
            useAction.canceled -= OnUse;
            useAction.Disable();
        }

        if (pauseAction !=null)
        {
            pauseAction.performed -= OnPause;
            pauseAction.Disable();
        }
        playerInput.ActivateInput();
        playerInput.SwitchCurrentActionMap("Player");

        
        pauseAction = playerInput.currentActionMap.FindAction("Pause", true);
        moveAction = playerInput.currentActionMap.FindAction("Move", true);
        runAction = playerInput.currentActionMap.FindAction("Sprint", true);
        useAction = playerInput.currentActionMap.FindAction("Interact", true);

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        moveAction.Enable();

        runAction.performed += OnSprint;
        runAction.canceled += OnSprint;
        runAction.Enable();

        useAction.performed += OnUse;
        useAction.canceled += OnUse;
        useAction.Enable();

        pauseAction.performed += OnPause;
        pauseAction.Enable();

        
    }

    void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();
        }

        if (runAction != null)
        {
            runAction.performed -= OnSprint;
            runAction.canceled -= OnSprint;
            runAction.Disable();
            
        }

        if (useAction !=null)
        {
            useAction.performed -= OnUse;
            useAction.canceled -= OnUse;
            useAction.Disable();
        }

        if (pauseAction !=null)
        {
            pauseAction.performed -= OnPause;
            pauseAction.Disable();
        }
    }

    
    private void OnUse(InputAction.CallbackContext context)
    {
        
        if(!context.performed) return;

        if (currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
        Debug.Log(currentInteractable == null ? "Nothing to interact with" : "Interacting!");
    }

    private void OnMove(InputAction.CallbackContext context)
    {


        moveInput = context.ReadValue<Vector2>();

        bool walking = moveInput.sqrMagnitude > 0.001f;
        animator.SetBool("isWalking", walking);

        if (!walking)
        {
            animator.SetFloat("lastInputX", animator.GetFloat("inputX"));
            animator.SetFloat("lastInputY", animator.GetFloat("inputY"));
        }

        animator.SetFloat("inputX", moveInput.x);
        animator.SetFloat("inputY", moveInput.y);
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValue<float>() > 0.1f;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
       if(!context.performed) return;

       bool currentlyPaused = Time.timeScale == 0f;
      

      if (currentlyPaused)
        {
            Time.timeScale = 1f;

            PauseMenu.SetActive(false);
            moveInput = Vector2.zero;
            isSprinting = false;
            rb.linearVelocity = Vector2.zero;

            StartCoroutine(EnableInputNextFrame());
        }
        else 
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);

            moveInput = Vector2.zero;
            isSprinting = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Update()
{
    DetectInteractable();
    bool moving = moveInput.sqrMagnitude > 0.001f;

    if (moving)
    {
        if (isSprinting)
            noiseLevel += SprintNlps * Time.deltaTime;
        else
            noiseLevel += nlps * Time.deltaTime;
    }
    else
    {
        noiseLevel -= nlds * Time.deltaTime;
    }

    noiseLevel = Mathf.Clamp(noiseLevel, 0f, maxNoise);
}

    void FixedUpdate()
{
    float currentSpeed = isSprinting ? runSpeed : moveSpeed;

    rb.linearVelocity = moveInput * currentSpeed;

    if (moveInput.x > 0.01f && transform.localScale.x < 0f) Flip();
    else if (moveInput.x < -0.01f && transform.localScale.x > 0f) Flip();
}


    void Flip()
    {
        facingDirection *= -1;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    void DetectInteractable()
{
    Vector2 direction = moveInput.normalized;

    if (direction == Vector2.zero)
        direction = new Vector2(animator.GetFloat("lastInputX"), animator.GetFloat("lastInputY")).normalized;

    Vector2 origin = (Vector2)transform.position + direction * 0.2f; // push origin forward a bit

    RaycastHit2D hit = Physics2D.Raycast(origin, direction, interactRadius, interactLayer);

    currentInteractable = hit.collider ? hit.collider.GetComponent<IInteractable>() : null;

    // Debug line so you can SEE the ray in Scene view
    Debug.DrawRay(origin, direction * interactRadius, hit.collider ? Color.green : Color.red);
}





    //keysCards

    public void GiveRedKey()
    {
        redKeyCard = true;
    }
    public void GiveYellowKey()
    {
        yellowKeyCard = true;
    }
    public void GiveBlueKey()
    {
        blueKeyCard = true;
    }
}
