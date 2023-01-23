using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biter_FOV : MonoBehaviour
{
    public float sightRadius;
    
    [Range(0, 360)]
    public float sightAngle;
    [Range(0, 360)]
    

    public GameObject playerRef;

    public Transform sightPoint;
   

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVSightRoutine());
      

      
    }

    private IEnumerator FOVSightRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return wait;
            FieldOfViewSightCheck();
        }
    }

 
    private IEnumerator InSight(float delay)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);

        canSeePlayer = true;
    }

    private IEnumerator outOfSight(float delay)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);

        canSeePlayer = false;
    }

    public Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void FieldOfViewSightCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(sightPoint.transform.position, sightRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                   StartCoroutine(InSight(0.5f)); 
                }

              

                else
                {
                    StartCoroutine(outOfSight(5f));
                }

            }
            else
            {
                StartCoroutine(outOfSight(5f));
            }

        }

        else if (canSeePlayer)
        {
            StartCoroutine(outOfSight(5f));
        }

    }


    
}
