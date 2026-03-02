using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotorTopDown : MonoBehaviour
{
   [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrainPerSecond = 15f;
    [SerializeField] private float staminaGainPerSecond = 10f;

    [Header("Debug (Read Only)")]
    [SerializeField] private float stamina; 
    public float StaminaRaw => stamina;

    public int facingDirection = 1;

    private Rigidbody2D rb;
    private PlayerBrain brain;
    private PlayerNoise playerNoise;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        brain = GetComponent<PlayerBrain>();
        playerNoise = GetComponent<PlayerNoise>();
        stamina = maxStamina;
    }

    private void OnEnable()
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
          if (brain == null) return;

        bool moving = brain.MoveInput.sqrMagnitude > 0.001f;

        // Can sprint only if holding sprint, moving, and have stamina
        bool canSprint = brain.SprintHeld && moving && stamina > 5f;

        // Drain/regen stamina
        if (canSprint)
            stamina -= staminaDrainPerSecond * Time.fixedDeltaTime;
        else
            stamina += staminaGainPerSecond * Time.fixedDeltaTime;

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        // Speed based on stamina-allowed sprint
        float speed = canSprint ? runSpeed : walkSpeed;
        rb.linearVelocity = brain.MoveInput * speed;

        // Noise
        if (playerNoise != null && moving)
            playerNoise.AddMovementNoise(canSprint, Time.fixedDeltaTime);

        
        if (brain.MoveInput.x > 0.01f && transform.localScale.x < 0f) Flip();
        else if (brain.MoveInput.x < -0.01f && transform.localScale.x > 0f) Flip();
    }

    private void Flip()
    {
        facingDirection *= -1;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}