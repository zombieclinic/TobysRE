using System.Collections.Generic;
using UnityEngine;

public class GridChase2D : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2.5f;

    public float repathEvery = 0.25f;
    public float reachDist = 0.1f;

    private AStarPathfinder pathfinder;
    private List<Vector2> path;
    private int index;
    private float nextRepath;

    void Awake()
    {
        pathfinder = FindFirstObjectByType<AStarPathfinder>();
    }

    void Update()
    {
        if (!target || pathfinder == null) return;

        if (Time.time >= nextRepath)
        {
            nextRepath = Time.time + repathEvery;
            path = pathfinder.FindPath(transform.position, target.position);
            index = 0;
        }

        if (path == null || index >= path.Count) return;

        Vector2 next = path[index];
        Vector2 pos = transform.position;

        Vector2 newPos = Vector2.MoveTowards(pos, next, moveSpeed * Time.deltaTime);
        transform.position = newPos;

        if (Vector2.Distance(newPos, next) <= reachDist)
            index++;
    }
}
