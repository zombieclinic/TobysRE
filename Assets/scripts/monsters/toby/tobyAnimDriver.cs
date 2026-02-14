using UnityEngine;

public class tobyAnimDriver : MonoBehaviour
{
    private Animator animator;
    public int facingDirection = 1;
    private Vector2 lastDir = Vector2.down;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMovement(Vector2 dir, bool isMoving)
    {
        if (isMoving && dir.sqrMagnitude > 0.0001f)
        {
            dir.Normalize();

            animator.SetBool("isWalking", true);
            animator.SetFloat("inputX", dir.x);
            animator.SetFloat("inputY", dir.y);

            if (dir.x > 0.01f && facingDirection != 1) Flip();
            else if (dir.x < -0.01f && facingDirection != -1) Flip();

            lastDir = dir;
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("lastInputX", lastDir.x);
            animator.SetFloat("lastInputY", lastDir.y);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
