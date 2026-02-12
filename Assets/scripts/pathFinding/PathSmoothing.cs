using System.Collections.Generic;
using UnityEngine;

public static class PathSmoothing
{
    public static List<Vector2> Smooth(List<Vector2> path, LayerMask wallMask)
    {
        if (path == null || path.Count < 3) return path;

        var smooth = new List<Vector2>();
        int i = 0;
        smooth.Add(path[0]);

        while (i < path.Count - 1)
        {
            int furthest = i + 1;

            for (int j = path.Count - 1; j > furthest; j--)
            {
                if (!Physics2D.Linecast(path[i], path[j], wallMask))
                {
                    furthest = j;
                    break;
                }
            }

            smooth.Add(path[furthest]);
            i = furthest;
        }

        return smooth;
    }
}
