using UnityEngine;

public class destoryTeddy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("teddy"))
        {
            Destroy(other.gameObject);
        }
    }
}
