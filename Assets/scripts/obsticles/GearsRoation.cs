using UnityEngine;

public class GearsRoation : MonoBehaviour
{
    [SerializeField] public float gearRotation = 5;
    
   

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0f, 0f, gearRotation * Time.deltaTime);
    }
}
