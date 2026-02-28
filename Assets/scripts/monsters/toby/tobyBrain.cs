using UnityEngine;

public class tobyBrain : MonoBehaviour
{
    private tobyPatrol patrol;
    private GridChase2D chase;
    private PlayerNoise playerNoise;

    private GameObject pathSystem;
    private PathGrid grid;

    private float despawnTimer = 0f;
    private const float DESPAWN_TIME = 60f;

    private AudioSource audioSource;
    private float randomSoundChance = 50f;
    private float randomSoundInterval = 25f;
    private float soundTimer = 0f;

    [SerializeField] private AudioClip[] tobySounds;

    [Range(0.1f, 0.99f)]
    [SerializeField] private float returnPercent = 0.8f;

    private bool isChasing;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        // If player object got destroyed / scene swapped, try to reacquire
        if (playerNoise == null)
        {
            playerNoise = FindFirstObjectByType<PlayerNoise>();
            return;
        }

        float chaseThreshold  = playerNoise.MaxNoise;              
        float returnThreshold = playerNoise.MaxNoise * returnPercent; 

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

        soundTimer += Time.deltaTime;
        if (soundTimer >= randomSoundInterval)
        {
            soundTimer = 0f;
            float roll = Random.Range(0f, 100f);
            if (roll <= randomSoundChance)
                PlayRandom();
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

    void PlayRandom()
    {
        if (tobySounds == null || tobySounds.Length == 0) return;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        int index = Random.Range(0, tobySounds.Length);
        AudioClip clip = tobySounds[index];

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(clip, 1f);
    }
}