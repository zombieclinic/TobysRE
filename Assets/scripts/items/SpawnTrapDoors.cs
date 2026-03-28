using System.Linq;
using UnityEngine;


public class SpawnTrapDoors : MonoBehaviour
{


    private Transform[] spawnPoints;
    public Transform keycardChancesParent;

    [SerializeField] private GameObject redKeyCard;
    [SerializeField] private GameObject blueKeyCard;
    [SerializeField] private GameObject yellowKeyCard;

    void Awake()
    {
        if(keycardChancesParent == null)
            keycardChancesParent = GameObject.Find("keycardChances")?.transform;       
    }

    void Start()
    {
        if (keycardChancesParent == null)
        {
            Debug.LogError("keycardChances parent not found!");
            return;
        }

        if(GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null");
            return;
        }

        spawnPoints = keycardChancesParent
            .Cast<Transform>()
            .OrderBy(t => ExtractNumber(t.name))
            .ToArray();

        if (spawnPoints.Length <3)
        {
            Debug.LogError("Need at least 3 spawn points.");
            return;
        }

        SpawnKeyCards();
    }

    void SpawnKeyCards()
    {
        GameManager gm = GameManager.Instance;

        if (gm.redKeycardIndex == -1 || gm.yellowKeycardIndex == -1 || gm.blueKeycardIndex == -1)
        {
            int[] shuffledIndexes = Enumerable.Range(0, spawnPoints.Length)
            .OrderBy(x => Random.value)
            .ToArray();

            gm.redKeycardIndex = shuffledIndexes[0];
            gm.yellowKeycardIndex = shuffledIndexes[1];
            gm.blueKeycardIndex = shuffledIndexes[2];
        }

        if (!gm.redKeycardPickedUp)
        {
            Instantiate(redKeyCard, spawnPoints[gm.redKeycardIndex].position, Quaternion.identity);
        }

         if (!gm.yellowKeycardPickedUp)
        {
            Instantiate(yellowKeyCard, spawnPoints[gm.yellowKeycardIndex].position, Quaternion.identity);
        }

        if (!gm.blueKeycardPickedUp)
        {
            Instantiate(blueKeyCard, spawnPoints[gm.blueKeycardIndex].position, Quaternion.identity);
        }
    }

    private int ExtractNumber(string s)
        {
            string digits = new string(s.Where(char.IsDigit).ToArray());
            return int.TryParse(digits, out int n) ? n : int.MaxValue;
        }


}