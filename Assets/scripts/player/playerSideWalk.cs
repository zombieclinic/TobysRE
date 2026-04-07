using UnityEngine;

public class playerSideWalk : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrainPerSecond = 15f;
    [SerializeField] private float staminaGainPerSecond = 10f;

    [Header("Jump")]
    [SerializeField] private float jumpForced = 12f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    
    private Animator animator;

    private bool wasJumpHeld;

    [Header("Debug (Read Only)")]
    [SerializeField] private float stamina; 
    public float StaminaRaw => stamina;

    public int facingDirection = 1;

    private Rigidbody2D rb;
    private PlayerBrain brain;
    private PlayerNoise playerNoise;

    
    [Header("Ladder")]
    [SerializeField] private LayerMask ladderLayer;
    [SerializeField] private float ladderCheckRadius = 0.2f;
    [SerializeField] private float climbSpeed = 4f;
    [SerializeField] private float normalGravity = 1f;

    private bool onLadder;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        brain = GetComponent<PlayerBrain>();
        playerNoise = GetComponent<PlayerNoise>();
        stamina = maxStamina;
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = normalGravity;
        wasJumpHeld = false;
        animator.ResetTrigger("JumpStart");
        
    }


    private void FixedUpdate()
    {
        if (brain == null) return;

        float moveX = brain.MoveInput.x;
        bool moving = Mathf.Abs(moveX) > 0.01f;

        // Can sprint only if holding sprint, moving, and have stamina
        bool canSprint = brain.SprintHeld && moving && stamina > 5f;

        float moveY = brain.MoveInput.y;

        onLadder = Physics2D.OverlapCircle(groundCheck.position, ladderCheckRadius, ladderLayer);
        // Drain/regen stamina
        if (canSprint)
            stamina -= staminaDrainPerSecond * Time.fixedDeltaTime;
        else
            stamina += staminaGainPerSecond * Time.fixedDeltaTime;

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        // Speed based on stamina-allowed sprint
        float speed = canSprint ? runSpeed : walkSpeed;
       

       //ladders
        if(onLadder && Mathf.Abs(moveY) > 0.01f)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(moveX * speed, moveY * climbSpeed);
        }
        else
        {
            rb.gravityScale = normalGravity;
            rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
        }

        // jump
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("YVel", rb.linearVelocity.y);

        bool jumpPressedNow = brain.JumpPressed;

        if (jumpPressedNow && !wasJumpHeld && grounded && !onLadder)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForced, ForceMode2D.Impulse);
            animator.SetTrigger("JumpStart"); 
            animator.SetBool("Grounded", false);
            
        }

        wasJumpHeld = jumpPressedNow;
        
         // Noise
        if (playerNoise != null && moving)
                playerNoise.AddMovementNoise(canSprint, Time.fixedDeltaTime);

            
        if (brain.MoveInput.x > 0.01f && transform.localScale.x < 0f) Flip();
        else if (brain.MoveInput.x < -0.01f && transform.localScale.x > 0f) Flip();

        Debug.Log("On Ladder: " + onLadder);
        }

        private void Flip()
        {
            facingDirection *= -1;
            Vector3 s = transform.localScale;
            s.x *= -1;
            transform.localScale = s;
        }

        private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, ladderCheckRadius);
    }
}
