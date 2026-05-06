using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDistance : MonoBehaviour
{
 private float KillDistance = 0.9f;

 private Vector2 killPointOffset = Vector2.zero;

 private Transform player;
 private bool triggered;

    void Awake()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if(target != null) player = target.transform;
    }

    void Update()
    {
        if(triggered) return;

        Vector2 KillPoint = (Vector2)transform.position + killPointOffset;
        Vector2 playerPos = player.position;

        if ((KillPoint - playerPos).sqrMagnitude <= KillDistance * KillDistance)
        {
            triggered = true;
            SceneManager.LoadScene("GameOver");
        }
        
    }

   #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Vector3 killPoint = (Vector2)transform.position + killPointOffset;
        Gizmos.DrawWireSphere(killPoint, KillDistance);
        Gizmos.DrawLine(transform.position, killPoint);
    }
#endif
}
