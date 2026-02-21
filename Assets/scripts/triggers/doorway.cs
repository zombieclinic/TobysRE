using UnityEngine;

public class TeddyTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject teddyPrefab;
    [SerializeField] private Transform spawnWaypoint;

    private static GameObject activeTeddy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player == null) return;

        // Add chance (1 = 10%, 2 = 20% ... 10 = 100%)
        player.teddyChance = Mathf.Clamp(player.teddyChance + 1, 0, 10);

        int percentChance = player.teddyChance * 5;
        int roll = Random.Range(0, 100);

        if (roll <= percentChance)
        {
            if (GameObject.FindGameObjectWithTag("teddy") != null)
                return;

            if (teddyPrefab != null && spawnWaypoint != null)
            {
                activeTeddy = Instantiate(
                    teddyPrefab,
                    spawnWaypoint.position,
                    Quaternion.identity
                );
            }

            player.teddyChance = 0;
        }
    }
}

