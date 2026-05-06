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
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null");
            return;
        }

        if (pathGrid == null)
        {
            Debug.LogError("PathGrid is missing");
            return;
        }

        if(!GameManager.Instance.batteriesGenerated)
        {
            BuildNodeList();
            GenerateAndSaveBatterySpawns();
        }

        RespawnSavedBatteries();
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

    void GenerateAndSaveBatterySpawns()
    {
        List<Vector3> spawnPositions = new List<Vector3>();
        int spawnCount = Mathf.Min(amountToSpawn, availableNodes.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, availableNodes.Count);
            Node node = availableNodes[index];

            spawnPositions.Add(node.worldPos);
            availableNodes.RemoveAt(index);
        }

        GameManager.Instance.SaveBatterySpawns(spawnPositions);
    }

     void RespawnSavedBatteries()
    {
        for (int i = 0; i < GameManager.Instance.batteryData.Count; i++)
        {
            if (!GameManager.Instance.batteryData[i].pickedUp)
            {
                GameObject battery = Instantiate(
                    Battery,
                    GameManager.Instance.batteryData[i].position,
                    Quaternion.identity
                );

                BatteryPickup pickup = battery.GetComponent<BatteryPickup>();
                if (pickup != null)
                {
                    pickup.SetBatteryIndex(i);
                }
            }
        }
    }
}