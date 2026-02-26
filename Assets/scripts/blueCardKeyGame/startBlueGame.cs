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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
