using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBad_AI_PatrolState : BigBad_AI_BaseState
{
    public override void EnterState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
        //Steer
        bigBadEnemy_AI_Controller.BigBadAgent.speed = 3.5f;
        bigBadEnemy_AI_Controller.BigBadAgent.angularSpeed = 120f;
        bigBadEnemy_AI_Controller.BigBadAgent.acceleration = 8f;
        bigBadEnemy_AI_Controller.BigBadAgent.stoppingDistance = 0f;
        bigBadEnemy_AI_Controller.BigBadAgent.autoBraking = false;
    }

    public override void UpdateState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
        if (!bigBadEnemy_AI_Controller.BigBadAgent.pathPending && bigBadEnemy_AI_Controller.BigBadAgent.remainingDistance < 0.5f)
        {
            Debug.Log("GoToNextPoint");
            GoToNextPoint(bigBadEnemy_AI_Controller);
        }

        bigBadEnemy_AI_Controller.BigBadAgent.SetDestination(bigBadEnemy_AI_Controller.BigBadAgent.destination);
    }

    public override void ExitState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
        
    }

   public void GoToNextPoint(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller)
    {
        if (bigBadEnemy_AI_Controller.patrolPath.waypoints.Count == 0)
            return;
        bigBadEnemy_AI_Controller.BigBadAgent.destination = bigBadEnemy_AI_Controller.patrolPath.waypoints[bigBadEnemy_AI_Controller.patrolWaypointIndex].position;
        bigBadEnemy_AI_Controller.patrolWaypointIndex = (bigBadEnemy_AI_Controller.patrolWaypointIndex + 1) % bigBadEnemy_AI_Controller.patrolPath.waypoints.Count;
    }

}
