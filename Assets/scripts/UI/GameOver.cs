using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartScreen());
    }

  IEnumerator StartScreen()
    { 
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene("titleUi");
    }
}
