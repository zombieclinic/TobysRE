
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
        [SerializeField] public float moveSpeed = 5f;

        private Rigidbody2D rb;
        private PlayerInput playerInput;
        private InputAction moveAction;

        private Vector2 moveInput;
        private Animator animator;

        public int facingDirection = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
{
    
    playerInput.DeactivateInput();
    playerInput.ActivateInput();

   
    playerInput.SwitchCurrentActionMap("Player");
}
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    void OnEnable()
    {
        moveAction.Enable();

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.Disable();

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

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput.x > 0.01f && transform.localScale.x < 0f)
            Flip();
        else if (moveInput.x < -0.01f && transform.localScale.x > 0f)
            Flip();
    }

    void Flip()
    {
       facingDirection *= -1;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "teddy")
        {
            gameOver();
        }
    }

    void gameOver()
    {
        Destroy(gameObject);
    }
}
