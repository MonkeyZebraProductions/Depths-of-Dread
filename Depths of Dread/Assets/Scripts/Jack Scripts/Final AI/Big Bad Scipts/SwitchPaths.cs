using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPaths : MonoBehaviour
{
    public BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller;
    public BigBadPatrolPath newpatrolPath;
    public PlayerMovementScript playerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovementScript>(out playerMovementScript))
        {
            bigBadEnemy_AI_Controller.patrolWaypointIndex = 0;
            bigBadEnemy_AI_Controller.patrolPath = newpatrolPath;
        }
    }

}
