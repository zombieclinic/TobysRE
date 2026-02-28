using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotorTopDown : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    [Header("Noise")]
    public float noiseLevel = 0f;
    public float nlps = 10f;
    public float nlds = 15f;
    public float sprintNlps = 15f;
    public float maxNoise = 100f;

    public int facingDirection = 1;

    private Rigidbody2D rb;
    private PlayerBrain brain;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        brain = GetComponent<PlayerBrain>();
    }

    private void OnEnable()
    {
        // Top-down: no gravity
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (brain == null) return;

        float speed = brain.SprintHeld ? runSpeed : walkSpeed;
        Vector2 v = brain.MoveInput * speed;

        rb.linearVelocity = v;

        // Flip by x direction only
        if (brain.MoveInput.x > 0.01f && transform.localScale.x < 0f) Flip();
        else if (brain.MoveInput.x < -0.01f && transform.localScale.x > 0f) Flip();

        UpdateNoise();
    }

    private void UpdateNoise()
    {
        bool moving = brain.MoveInput.sqrMagnitude > 0.001f;

        if (moving)
            noiseLevel += (brain.SprintHeld ? sprintNlps : nlps) * Time.fixedDeltaTime;
        else
            noiseLevel -= nlds * Time.fixedDeltaTime;

        noiseLevel = Mathf.Clamp(noiseLevel, 0f, maxNoise);
    }

    private void Flip()
    {
        facingDirection *= -1;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}