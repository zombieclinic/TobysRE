using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder : MonoBehaviour
{
    public PathGrid grid;

    void Awake()
    {
        if (!grid) grid = FindFirstObjectByType<PathGrid>();
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node start = grid.FromWorld(startPos);
        Node goal  = grid.FromWorld(targetPos);

        if (!start.walkable || !goal.walkable) return null;

        var open = new List<Node>();
        var closed = new HashSet<Node>();

        // reset touched state (simple reset for starter)
        foreach (var n in grid.grid)
        {
            n.gCost = int.MaxValue;
            n.hCost = 0;
            n.parent = null;
        }

        start.gCost = 0;
        start.hCost = Heuristic(start, goal);
        open.Add(start);

        while (open.Count > 0)
        {
            Node current = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                Node t = open[i];
                if (t.fCost < current.fCost || (t.fCost == current.fCost && t.hCost < current.hCost))
                    current = t;
            }

            open.Remove(current);
            closed.Add(current);

            if (current == goal)
                return Retrace(start, goal);

            foreach (var nb in grid.Neighbors8(current))

            {
                if (!nb.walkable || closed.Contains(nb)) continue;

                int stepCost = (nb.gridX != current.gridX && nb.gridY != current.gridY) ? 14 : 10;
                int newCost = current.gCost + stepCost;

                if (newCost < nb.gCost)
                {
                    nb.gCost = newCost;
                    nb.hCost = Heuristic(nb, goal);
                    nb.parent = current;

                    if (!open.Contains(nb))
                        open.Add(nb);
                }
            }
        }

        return null;
    }

    int Heuristic(Node a, Node b)
{
    int dx = Mathf.Abs(a.gridX - b.gridX);
    int dy = Mathf.Abs(a.gridY - b.gridY);

    int straight = 10;
    int diag = 14;

    int min = Mathf.Min(dx, dy);
    int max = Mathf.Max(dx, dy);

    return diag * min + straight * (max - min);
}


    List<Vector2> Retrace(Node start, Node goal)
    {
        var path = new List<Vector2>();
        Node cur = goal;

        while (cur != start)
        {
            path.Add(cur.worldPos);
            cur = cur.parent;
            if (cur == null) return null;
        }

        path.Reverse();
        return path;
    }
}
