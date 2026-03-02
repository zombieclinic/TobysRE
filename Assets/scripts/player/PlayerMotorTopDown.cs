using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotorTopDown : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    public int facingDirection = 1;

    private Rigidbody2D rb;
    private PlayerBrain brain;
    private PlayerNoise playerNoise;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        brain = GetComponent<PlayerBrain>();
        playerNoise = GetComponent<PlayerNoise>();
    }

    private void OnEnable()
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (brain == null) return;

        float speed = brain.SprintHeld ? runSpeed : walkSpeed;
        Vector2 v = brain.MoveInput * speed;

        rb.linearVelocity = v;

        // Send movement info to noise system
        if (playerNoise != null && brain.MoveInput.sqrMagnitude > 0.001f)
        {
            playerNoise.AddMovementNoise(brain.SprintHeld, Time.fixedDeltaTime);
        }

        // Flip by x direction only
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