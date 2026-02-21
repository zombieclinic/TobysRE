
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class tobyBrain : MonoBehaviour
{
    private tobyPatrol patrol;
    private GridChase2D chase;
    private PlayerController player;

    private GameObject pathSystem;
    private PathGrid grid;
    private float despawnTimer =0f;
    private const float DESPAWN_TIME = 60F;

    private AudioSource audioSource;
    private float randomSoundChance = 50f;
    private float randomSoundInterval = 25f;

    private float soundTimer = 0f;

    [SerializeField] private AudioClip[] tobySounds;

    [Range(0.1f, 0.99f)]
    private float returnPercent = 0.8f;
    

    private bool isChasing;

    
    void Awake()
    {
      
        audioSource = GetComponent<AudioSource>();
        patrol = GetComponent<tobyPatrol>();
        chase  = GetComponent<GridChase2D>();

        player = FindFirstObjectByType<PlayerController>();

        
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

        if (!isChasing)
        {
            despawnTimer += Time.deltaTime;

            if (despawnTimer >= DESPAWN_TIME)
            {
                Destroy(gameObject);
            }
            
        }

                soundTimer += Time.deltaTime;
        if (soundTimer >= randomSoundInterval)
        {
            soundTimer = 0f;
            float roll = Random.Range(0f, 100f);

            if(roll <= randomSoundChance)
            {
                playRandom();
            }
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

        // When returning to patrol, re-sync to nearest waypoint
        if (!value && patrol != null)
            patrol.ResetToClosestWaypoint();
    }

    void playRandom()
    {
    if (audioSource == null)
    {
        audioSource = GetComponent<AudioSource>();
    }


    int index = Random.Range(0, tobySounds.Length);

    AudioClip clip = tobySounds[index];
    

    audioSource.pitch = Random.Range(0.95f, 1.05f);
    audioSource.PlayOneShot(clip, 1f);
}
}
