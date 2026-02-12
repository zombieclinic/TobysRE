using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    [Header("Size (world units)")]
    public Vector2 gridWorldSize = new Vector2(60, 60);

    [Header("Grid cell size")]
    public float cellSize = 1f;

    [Header("Walls")]
    public LayerMask wallMask;
    public float checkRadius = 0.45f; // slightly less than cellSize/2

    public Node[,] grid;
    public int sizeX, sizeY;

    private Vector2 bottomLeft;

    void Awake()
    {
        Build();
    }

    public void Build()
    {
        sizeX = Mathf.RoundToInt(gridWorldSize.x / cellSize);
        sizeY = Mathf.RoundToInt(gridWorldSize.y / cellSize);

        grid = new Node[sizeX, sizeY];

        bottomLeft = (Vector2)transform.position
            - Vector2.right * gridWorldSize.x * 0.5f
            - Vector2.up    * gridWorldSize.y * 0.5f;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Vector2 p = bottomLeft
                    + Vector2.right * (x * cellSize + cellSize * 0.5f)
                    + Vector2.up    * (y * cellSize + cellSize * 0.5f);

                bool blocked = Physics2D.OverlapCircle(p, checkRadius, wallMask);
                bool walkable = !blocked;

                grid[x, y] = new Node(walkable, p, x, y);
            }
        }
    }

    public Node FromWorld(Vector2 worldPos)
    {
        float px = Mathf.Clamp01((worldPos.x - bottomLeft.x) / gridWorldSize.x);
        float py = Mathf.Clamp01((worldPos.y - bottomLeft.y) / gridWorldSize.y);

        int x = Mathf.Clamp(Mathf.FloorToInt(px * sizeX), 0, sizeX - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt(py * sizeY), 0, sizeY - 1);

        return grid[x, y];
    }

    public List<Node> Neighbors4(Node n)
    {
        var list = new List<Node>(4);

        TryAdd(n.gridX + 1, n.gridY, list);
        TryAdd(n.gridX - 1, n.gridY, list);
        TryAdd(n.gridX, n.gridY + 1, list);
        TryAdd(n.gridX, n.gridY - 1, list);

        return list;
    }

    void TryAdd(int x, int y, List<Node> list)
    {
        if (x < 0 || y < 0 || x >= sizeX || y >= sizeY) return;
        list.Add(grid[x, y]);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid == null) return;

        foreach (var n in grid)
        {
            Gizmos.color = n.walkable ? new Color(0,1,0,0.2f) : new Color(1,0,0,0.2f);
            Gizmos.DrawCube(n.worldPos, Vector3.one * (cellSize * 0.9f));
        }
    }
#endif
}
