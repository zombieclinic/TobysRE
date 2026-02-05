using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject teddy;

    Vector2[] spawnPoints = new Vector2[]
    {
        new Vector2(-39.47f, 37.48f),
        new Vector2(-23.42f, 27.43f),
        new Vector2(-9.46f, 18.53f),
        new Vector2(-40.38f, 12.59f),
        new Vector2(-25.49f, 29.52f),
        new Vector2(-23.45f, 33.42f),
        new Vector2(-2.46f, 36.47f),
        new Vector2(-2.46f, 4.6f),
        new Vector2(-2.45f, 12.53f),
        new Vector2(10.62f, 26.06f),
        new Vector2(19.52f, 12.61f),
        new Vector2(10.49f, 2.66f),
        new Vector2(-25.47f, 9.48f),
        new Vector2(-8.5f, 12.46f),
        new Vector2(-25.76f, -3.51f),
        new Vector2(-33.7f, 2.01f),
        new Vector2(-14.49f, 2.58f),
        new Vector2(-32.76f, 20.58f),
        new Vector2(-32.76f, 33.79f),
        new Vector2(-23.44f, 18.57f)
    };

    void Start()
    {
        SpawnRandomTeddies(3);
    }

    void SpawnRandomTeddies(int amount)
    {
        // Shuffle the array so the first 3 positions are random
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rand = Random.Range(i, spawnPoints.Length);
            Vector2 temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[rand];
            spawnPoints[rand] = temp;
        }

        // Spawn the first 'amount' positions
        for (int i = 0; i < amount; i++)
        {
            Instantiate(teddy, spawnPoints[i], transform.rotation);
        }
    }
}
