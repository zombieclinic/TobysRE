using System.Linq;
using UnityEngine;


public class keycards : MonoBehaviour
{
    
      private Transform[] spawnPoints;
      public Transform keycardChancesParent;
    [SerializeField] private GameObject redKeyCard;
    [SerializeField] private GameObject yellowKeyCard;
    [SerializeField] private GameObject blueKeyCard;


    void Awake()
    {
       if (keycardChancesParent == null)
        keycardChancesParent = GameObject.Find("keycardChances")?.transform;
    }

    void Start()
    {
        spawnPoints = keycardChancesParent
            .Cast<Transform>()
            .OrderBy(t => ExtractNumber(t.name))
            .ToArray();

            spawnKeyCards();

    }

    void spawnKeyCards()
    {
        spawnPoints = spawnPoints.OrderBy(x => Random.value).ToArray();

        Instantiate(redKeyCard, spawnPoints[0].position, Quaternion.identity);
        Instantiate(yellowKeyCard, spawnPoints[1].position, Quaternion.identity);
        Instantiate(blueKeyCard, spawnPoints[2].position, Quaternion.identity);
    }

    private int ExtractNumber(string s)
    {
        string digits = new string(s.Where(char.IsDigit).ToArray());
        return int.TryParse(digits, out int n) ? n : int.MaxValue;
    }
}