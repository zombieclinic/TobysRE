using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interact Controls")]
    [SerializeField] private float interactRadius = 0.75f;
    [SerializeField] private LayerMask interactLayer;

    private IInteractable currentInteractable;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DetectInteractable();
    }

    public void TryInteract(PlayerBrain brain)
    {
        if (currentInteractable != null)
            currentInteractable.Interact(brain);
    }

    private void DetectInteractable()
    {
        if (animator == null) return;

        Vector2 direction = new Vector2(animator.GetFloat("inputX"), animator.GetFloat("inputY"));

        if (direction.sqrMagnitude < 0.001f)
            direction = new Vector2(animator.GetFloat("lastInputX"), animator.GetFloat("lastInputY"));

        if (direction.sqrMagnitude < 0.001f)
            direction = Vector2.right;

        direction.Normalize();

        Vector2 origin = (Vector2)transform.position + direction * 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, interactRadius, interactLayer);

        currentInteractable = hit.collider ? hit.collider.GetComponent<IInteractable>() : null;
        Debug.DrawRay(origin, direction * interactRadius, hit.collider ? Color.green : Color.red);
    }
}