using UnityEngine;

public class destroyTeddy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

         LightToggle lt = other.GetComponent<LightToggle>();
        if (lt != null)
        {
            lt.AddFuel(100);
        }
        if (other.CompareTag("teddy"))
        {
            Destroy(other.gameObject);
        }
    }
}
