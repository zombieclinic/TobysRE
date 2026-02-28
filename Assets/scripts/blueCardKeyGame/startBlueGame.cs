using UnityEngine;

public class startBlueGame : MonoBehaviour
{


    void Start()
    {
         GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
           

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = 11f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
