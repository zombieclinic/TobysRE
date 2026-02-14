using UnityEngine;

public class tobyBrain : MonoBehaviour
{
    private tobyPatrol patrol;
    private GridChase2D chase;
    private PlayerController player;

    private GameObject pathSystem;
    private PathGrid grid;

    [Range(0.1f, 0.99f)]
    [SerializeField] private float returnPercent = 0.8f;

    private bool isChasing;

    void Awake()
    {
        patrol = GetComponent<tobyPatrol>();
        chase  = GetComponent<GridChase2D>();

        player = FindFirstObjectByType<PlayerController>();

        // Pathfinding system in scene (can start disabled)
        pathSystem = GameObject.Find("PathfindingSystem");
        if (pathSystem != null)
            grid = pathSystem.GetComponent<PathGrid>();

        SetChase(false);
    }

    void Update()
    {
        if (player == null) return;

        float chaseThreshold  = player.maxNoise;
        float returnThreshold = player.maxNoise * returnPercent;

        if (!isChasing && player.noiseLevel >= chaseThreshold)
            SetChase(true);
        else if (isChasing && player.noiseLevel <= returnThreshold)
            SetChase(false);
    }

    void SetChase(bool value)
    {
        isChasing = value;

        if (patrol) patrol.enabled = !value;
        if (chase)  chase.enabled  = value;

        // Pathfinding only during chase
        if (pathSystem != null)
        {
            pathSystem.SetActive(value);

            // If you don't want this to rebuild every time,
            // add a "built" bool in PathGrid.Build()
            if (value && grid != null)
                grid.Build();
        }

        // When returning to patrol, re-sync to nearest waypoint
        if (!value && patrol != null)
            patrol.ResetToClosestWaypoint();
    }
}
