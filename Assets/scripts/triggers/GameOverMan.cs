using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMan : MonoBehaviour
{
   
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        GameObject cine = GameObject.FindGameObjectWithTag("CineMachine");
        GameObject main = GameObject.FindGameObjectWithTag("MainCamera");
        if (collision.CompareTag("Player"))
        {
             Destroy(target);
             Destroy(cine);
             Destroy(main);
             SceneManager.LoadScene("GameOver");
        }
    }
}
