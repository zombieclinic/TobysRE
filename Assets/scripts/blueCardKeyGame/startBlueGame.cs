using UnityEngine;

public class startBlueGame : MonoBehaviour
{
   

    void Start()
    {
         GameObject player = GameObject.FindWithTag("Player");

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = 11f;
            player.transform.position = new Vector3(-7.4f,3.8f, 0f);
            PlayerBrain brain = player.GetComponent<PlayerBrain>();
             brain.ReStartPlayerControls();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
