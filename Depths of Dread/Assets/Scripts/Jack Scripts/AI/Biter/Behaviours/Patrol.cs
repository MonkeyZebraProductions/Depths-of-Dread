using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public EnemyController enemeyController;
    public PatrolPath path;
    public Vector3 nextWaypoint;
    public float waypointDistance = 5;
    public int next = 0;
    public bool looped = false;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, nextWaypoint);
        }
    }

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nextWaypoint = NextWaypoint();


        Vector3 desiredVelocity = (nextWaypoint - enemeyController.position);
        float distance = desiredVelocity.magnitude;
        enemeyController.steeringForce = desiredVelocity - enemeyController.velocity;

        if (Vector3.Distance(transform.position, nextWaypoint) < waypointDistance)
        {
            AdvanceToNext();
            //Debug.Log("Next Waypoint");
        }

        if (!looped && IsLast())
        {



            //Debug.Log("ArriveForce");
        }
        else
        {

            //Debug.Log("ArriveForce2");

        }


    }
    public Vector3 NextWaypoint()
    {
        return path.waypoints[next];

    }

    public void AdvanceToNext()
    {
        if (looped)
        {
            next = (next + 1) % path.waypoints.Count;
        }
        else
        {
            if (next != path.waypoints.Count - 1)
            {
                next++;
            }
        }
    }


    public bool IsLast()
    {
        return next == path.waypoints.Count - 1;
    }



}
