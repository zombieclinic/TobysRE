using UnityEngine;

public class tobyAnimDriver : MonoBehaviour
{
    private Animator animator;
    public int facingDirection = 1;
    private Vector2 lastDir = Vector2.down;
    private GameObject walkSound;
    private float hearDistance = 10f;
    private Vector2 soundPointOffSet = Vector2.zero;


    private Transform player;
    void Awake()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if(target != null) player = target.transform;
        walkSound = GameObject.Find("walkSound");
        animator = GetComponent<Animator>();
        walkSound.SetActive(false);
    }


    

    public void SetMovement(Vector2 dir, bool isMoving)
    {
        if (isMoving && dir.sqrMagnitude > 0.0001f)
        {
            dir.Normalize();

            animator.SetBool("isWalking", true);
            animator.SetFloat("inputX", dir.x);
            animator.SetFloat("inputY", dir.y);
            if (player != null && walkSound != null)
            {
                Vector2 hearPoint = (Vector2)transform.position + soundPointOffSet;
                Vector2 playerPos = player.position;

                bool inside = (hearPoint - playerPos).sqrMagnitude <= hearDistance * hearDistance;
                walkSound.SetActive(inside); 
            }

            if (dir.x > 0.01f && facingDirection != 1) Flip();
            else if (dir.x < -0.01f && facingDirection != -1) Flip();

            lastDir = dir;
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("lastInputX", lastDir.x);
            animator.SetFloat("lastInputY", lastDir.y);
             if (walkSound != null) walkSound.SetActive(false);
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
