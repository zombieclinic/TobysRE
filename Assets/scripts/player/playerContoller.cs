using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float runSpeed = 10f;

    private Rigidbody2D rb;
    public event System.Action AttackPressed;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;
   
    private bool isSprinting;
    private Vector2 moveInput;
    private Animator animator;

    public int teddyChance = 0;

    

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
        playerInput.ActivateInput();
        playerInput.SwitchCurrentActionMap("Player");

        
   
        moveAction = playerInput.currentActionMap.FindAction("Move", true);
        runAction = playerInput.currentActionMap.FindAction("Sprint", true);

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        moveAction.Enable();

        runAction.performed += OnSprint;
        runAction.canceled += OnSprint;
        runAction.Enable();

        
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

    void Update()
{
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
}
