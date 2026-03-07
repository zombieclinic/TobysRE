using UnityEngine;

public class lightWobby : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float wobbleForce = 5f;
    [SerializeField] private float interval = 2f;

    private float timer;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= interval)
        {
            timer = 0f;
            rb.AddTorque(Random.Range(-wobbleForce, wobbleForce), ForceMode2D.Impulse);
        }
    }
}
