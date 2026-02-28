using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerBrain brain;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        brain = GetComponent<PlayerBrain>();
    }

    private void Update()
    {
        if (brain == null) return;

        Vector2 move = brain.MoveInput;

        bool walking = move.sqrMagnitude > 0.001f;
        animator.SetBool("isWalking", walking);

        if (!walking)
        {
            animator.SetFloat("lastInputX", animator.GetFloat("inputX"));
            animator.SetFloat("lastInputY", animator.GetFloat("inputY"));
        }

        animator.SetFloat("inputX", move.x);
        animator.SetFloat("inputY", move.y);
    }
}