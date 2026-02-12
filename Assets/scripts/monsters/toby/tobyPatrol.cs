using System.Collections;
using System.Linq;
using UnityEngine;

public class tobyPatrol : MonoBehaviour
{
    public Transform waypointParent;
    public float moveSpeed = 1f;
    public float waitTime = 2f;
    public bool loopWaypoints = true;

    private Transform[] waypoints;
    private int currentWayPointIndex;
    private bool isWaiting;

    private tobyAnimDriver anim;
    private Vector2 lastDir = Vector2.down;

    private Rigidbody2D rb;



    void Start()
    {
       
        anim = GetComponent<tobyAnimDriver>();
        //  Get children, exclude the parent, SORT BY NUMBER IN NAME
        waypoints = waypointParent
            .Cast<Transform>() 
            .OrderBy(t => ExtractNumber(t.name))
            .ToArray();

       
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

}
