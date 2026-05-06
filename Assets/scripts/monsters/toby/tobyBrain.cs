using UnityEngine;

public class tobyBrain : MonoBehaviour
{
    private tobyPatrol patrol;
    private GridChase2D chase;
    [Header("Hearing")]
    [SerializeField] private float chaseNoiseThreshold = 25f;
    [Range(0.1f, 0.99f)]
    [SerializeField] private float returnPercent = 0.8f;
    private PlayerNoise playerNoise;

    private GameObject pathSystem;
    private PathGrid grid;

    private float despawnTimer = 0f;
    private const float DESPAWN_TIME = 60f;


    private bool isChasing;

    void Awake()
    {
        patrol = GetComponent<tobyPatrol>();
        chase  = GetComponent<GridChase2D>();

        playerNoise = FindFirstObjectByType<PlayerNoise>();

        pathSystem = GameObject.Find("PathfindingSystem");
        if (pathSystem != null)
            grid = pathSystem.GetComponent<PathGrid>();

        SetChase(false);
    }

    void Update()
    {
        
        if (playerNoise == null)
        {
            playerNoise = FindFirstObjectByType<PlayerNoise>();
            return;
        }

        float chaseThreshold  = chaseNoiseThreshold;
        float returnThreshold = chaseNoiseThreshold * returnPercent;

        float noiseNow = playerNoise.CurrentNoise;

        if (!isChasing && noiseNow >= chaseThreshold)
            SetChase(true);
        else if (isChasing && noiseNow <= returnThreshold)
            SetChase(false);

        if (!isChasing)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= DESPAWN_TIME)
                Destroy(gameObject);
        }
        else
        {

            despawnTimer = 0f;
        }

    }

    void SetChase(bool value)
    {
        isChasing = value;

        if (patrol) patrol.enabled = !value;
        if (chase)  chase.enabled  = value;

        if (pathSystem != null)
        {
            pathSystem.SetActive(value);

            if (value && grid != null)
                grid.Build();
        }

        if (!value && patrol != null)
            patrol.ResetToClosestWaypoint();
    }
}