
using UnityEngine;

public class elevatorButton : MonoBehaviour, IInteractable
{
    private float speed = 1;
    private bool GoingUp = false;

    void Update()
    {
        if(GoingUp)
        {
            transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
        }
    }
    public void Interact(PlayerBrain player)
    {
       if (player.blueKeyCard)
        {
            GoingUp = true;
        }
    }
}
