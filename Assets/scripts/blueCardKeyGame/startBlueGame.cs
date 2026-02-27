using UnityEngine;

public class startBlueGame : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
         GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            player.transform.position = new Vector3(-7.752f, 8.52f, 0f);
            var pc = player.GetComponent<PlayerController>();
            pc?.RebindInputNow();

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
