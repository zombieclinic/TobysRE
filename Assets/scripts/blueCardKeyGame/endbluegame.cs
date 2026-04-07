using UnityEngine;
using UnityEngine.SceneManagement;

public class endbluegame : MonoBehaviour

{
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("!Player")) return;
        SceneManager.LoadScene("GameScene");
    }
}
