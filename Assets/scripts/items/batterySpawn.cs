using UnityEngine;
using System.Collections.Generic;

public class batterySpawn : MonoBehaviour
{
    [SerializeField] private PathGrid pathGrid;
    [SerializeField] private GameObject Battery;
    [SerializeField] private int amountToSpawn = 5;

    private List<Node> availableNodes = new List<Node>();
    void Start()
    {
        BuildNodeList();
        SpawnItems();
    }

    void BuildNodeList()
    {
        availableNodes.Clear();

        for (int x = 0; x < pathGrid.sizeX; x++)
        {
            for (int y = 0; y < pathGrid.sizeY; y++)
            {
                Node node = pathGrid.grid[x, y];
                if (node.walkable)
                {
                    availableNodes.Add(node);
                }
            }
        }
    }

     void SpawnItems()
    {
        int spawnCount = Mathf.Min(amountToSpawn, availableNodes.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, availableNodes.Count);
            Node node = availableNodes[index];

            Instantiate(Battery, node.worldPos, Quaternion.identity);

            availableNodes.RemoveAt(index);
        }
    }
}