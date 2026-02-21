using System.Collections.Generic;
using UnityEngine;

public class GridChase2D : MonoBehaviour
{

    private tobyAnimDriver anim;
    private Vector2 lastDir = Vector2.down;

    private Transform target;
    private float moveSpeed = 3f;

    private float repathEvery = 0.25f;
    private float reachDist = 0.1f;

    private AStarPathfinder pathfinder;
    private List<Vector2> path;
    private int index;
    private float nextRepath;

    void Awake()
    {
        anim = GetComponent<tobyAnimDriver>();
        pathfinder = FindFirstObjectByType<AStarPathfinder>();
        target = GameObject.Find("Player")?.transform;
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

        if (path == null || index >= path.Count)
        {
            if (anim) anim.SetMovement(lastDir, false);
            return;
        }


        Vector2 next = path[index];
        Vector2 pos = transform.position;

        Vector2 toNext = next - pos;

    if (toNext.sqrMagnitude > 0.0001f)
        {
            Vector2 dir = toNext.normalized;
            lastDir = dir;
            if (anim) anim.SetMovement(dir, true);
        }
    else
        {
            if (anim) anim.SetMovement(lastDir, false);
        }

    Vector2 newPos = Vector2.MoveTowards(pos, next, moveSpeed * Time.deltaTime);
    transform.position = newPos;


        if (Vector2.Distance(newPos, next) <= reachDist)
            index++;
    }
}
