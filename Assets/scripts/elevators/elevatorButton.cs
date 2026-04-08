using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class ElevatorButton : MonoBehaviour, IInteractable
{
    [Header("Movement")]
    [SerializeField] private float speed = 0.9f;

    [Header("Audio")]
    [SerializeField] private AudioClip startElevator;
    [SerializeField] private AudioClip elevatorLoop;
    [SerializeField] private AudioClip endElevator;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    private bool isAtTop = false;
    private bool isStopping = false;

    private enum ElevatorState
    {
        Idle,
        MovingUp,
        MovingDown
    }

    private ElevatorState currentState = ElevatorState.Idle;

    private Coroutine audioRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case ElevatorState.MovingUp:
                rb.linearVelocity = new Vector2(0f, speed);
                break;

            case ElevatorState.MovingDown:
                rb.linearVelocity = new Vector2(0f, -speed);
                break;

            case ElevatorState.Idle:
                rb.linearVelocity = Vector2.zero;
                break;
        }
    }

    public void Interact(PlayerBrain player)
    {
        if (!player.blueKeyCard)
            return;

        if (currentState != ElevatorState.Idle || isStopping)
            return;

        if (isAtTop)
            StartGoingDown();
        else
            StartGoingUp();
    }

    private void StartGoingUp()
    {
        currentState = ElevatorState.MovingUp;
        StartElevatorAudio();
    }

    private void StartGoingDown()
    {
        currentState = ElevatorState.MovingDown;
        StartElevatorAudio();
    }

    private void StartElevatorAudio()
    {
        if (audioRoutine != null)
            StopCoroutine(audioRoutine);

        audioRoutine = StartCoroutine(PlayElevatorStartThenLoop());
    }

    private IEnumerator PlayElevatorStartThenLoop()
    {
        // Play start sound first
        if (startElevator != null)
        {
            audioSource.loop = false;
            audioSource.clip = startElevator;
            audioSource.Play();

            yield return new WaitForSeconds(startElevator.length);
        }

        // Then immediately switch to loop sound
        if (currentState != ElevatorState.Idle && elevatorLoop != null)
        {
            audioSource.loop = true;
            audioSource.clip = elevatorLoop;
            audioSource.Play();
        }

        audioRoutine = null;
    }

    private void StopElevator()
    {
        if (isStopping)
            return;

        StartCoroutine(StopElevatorRoutine());
    }

    private IEnumerator StopElevatorRoutine()
    {
        isStopping = true;

        currentState = ElevatorState.Idle;
        rb.linearVelocity = Vector2.zero;

        if (audioRoutine != null)
        {
            StopCoroutine(audioRoutine);
            audioRoutine = null;
        }

        // Stop the looping hum
        audioSource.Stop();

        // Play ending sound
        if (endElevator != null)
        {
            audioSource.loop = false;
            audioSource.clip = endElevator;
            audioSource.Play();

            yield return new WaitForSeconds(endElevator.length);
        }

        isStopping = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ElevatorTop"))
        {
            StopElevator();
            isAtTop = true;
        }
        else if (collision.CompareTag("ElevatorBottom"))
        {
            StopElevator();
            isAtTop = false;
        }
    }
}