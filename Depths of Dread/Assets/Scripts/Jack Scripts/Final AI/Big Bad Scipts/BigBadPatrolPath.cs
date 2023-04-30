using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBadPatrolPath : MonoBehaviour
{
    public List<Transform> waypoints;

    private void Start()
    {
        waypoints = PopulatePath(transform);
    }

    private void Update()
    {
        
    }

  

    List<Transform> PopulatePath(Transform parent)
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in parent)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

}
