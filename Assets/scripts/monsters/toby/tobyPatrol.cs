using System.Collections;
using System.Linq;
using UnityEngine;

public class tobyPatrol : MonoBehaviour
{
    public Transform waypointParent;
    private float moveSpeed = 1f;
    private float waitTime = 5f;
    public bool loopWaypoints = true;

    private Transform[] waypoints;
    private int currentWayPointIndex;
    private bool isWaiting;

    private tobyAnimDriver anim;
    private Vector2 lastDir = Vector2.down;

    private Rigidbody2D rb;



    void Awake()
    {
         if (waypointParent == null)
        waypointParent = GameObject.Find("waypoints")?.transform;

    if (waypointParent == null)
    {
        Debug.LogError($"{name}: Could not find GameObject named 'waypoints' in the scene.");
        enabled = false;
    }
    }

    void Start()
    {
    anim = GetComponent<tobyAnimDriver>();

    waypoints = waypointParent
        .Cast<Transform>()
        .OrderBy(t => ExtractNumber(t.name))
        .ToArray();

    // Start patrol at closest waypoint
    currentWayPointIndex = GetClosestWaypointIndex();
    }

    private int GetClosestWaypointIndex()
    {
        if (waypoints == null || waypoints.Length == 0) return 0;

        int bestIndex = 0;
        float bestDistSqr = Mathf.Infinity;
        Vector2 myPos = transform.position;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float d = ((Vector2)waypoints[i].position - myPos).sqrMagnitude;
            if (d < bestDistSqr)
            {
                bestDistSqr = d;
                bestIndex = i;
            }
        }
        return bestIndex;
    }

    void Update()
    {
        if (isWaiting) return;
        if (waypoints == null || waypoints.Length == 0) return;

        MoveToWayPoint();
    }

    void MoveToWayPoint()
    {
        Transform target = waypoints[currentWayPointIndex];

        Vector2 toTarget = (Vector2)target.position - (Vector2)transform.position;
        float dist = toTarget.magnitude;

        if (dist < 0.1f)
        {
            if (!isWaiting)
            StartCoroutine(WaitAtWaypoint());
            return;
        }

        Vector2 direction = toTarget.normalized;

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        anim.SetMovement(direction, true);
        lastDir = direction;
    }
        

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        anim.SetMovement(lastDir, false);

        yield return new WaitForSeconds(waitTime);

        currentWayPointIndex = loopWaypoints
            ? (currentWayPointIndex + 1) % waypoints.Length
            : Mathf.Min(currentWayPointIndex + 1, waypoints.Length - 1);

        isWaiting = false;
        
    }


    //waypoint_20 -> 20, waypoint_3 -> 3 
    private int ExtractNumber(string s)
    {
        string digits = new string(s.Where(char.IsDigit).ToArray());
        return int.TryParse(digits, out int n) ? n : int.MaxValue;
    }

    public void ResetToClosestWaypoint()
{
    if (waypoints == null || waypoints.Length == 0) return;

    StopAllCoroutines();
    isWaiting = false;

    currentWayPointIndex = GetClosestWaypointIndex();


    anim.SetMovement(lastDir, false);
}


}


