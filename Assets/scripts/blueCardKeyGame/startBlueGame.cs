using UnityEngine;
using UnityEngine.InputSystem;

public class startBlueGame : MonoBehaviour
{
   private PlayerMotorTopDown topControls;
    private playerSideWalk sideControls;
    public float gravity = 3;

    void Start()
    {
         GameObject player = GameObject.FindWithTag("Player");

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = gravity;
            player.transform.position = new Vector3(-7.4f,3.8f, 0f);
            

              
        topControls = player.GetComponent<PlayerMotorTopDown>();
        topControls.enabled = false;

        sideControls = player.GetComponent<playerSideWalk>();
        sideControls.enabled = true;

        PlayerBrain brain = player.GetComponent<PlayerBrain>();
            brain.ReStartPlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
