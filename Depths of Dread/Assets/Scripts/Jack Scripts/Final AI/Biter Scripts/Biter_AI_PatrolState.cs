using System.Collections;
using UnityEngine;


public class Biter_AI_PatrolState : Biter_AI_BaseState
{
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        //Steer
        biterEnemy_AI_Controller.BiterAgent.speed = 3.5f;
        biterEnemy_AI_Controller.BiterAgent.angularSpeed = 120f;
        biterEnemy_AI_Controller.BiterAgent.acceleration = 8f;
        biterEnemy_AI_Controller.BiterAgent.stoppingDistance = 0f;
        biterEnemy_AI_Controller.BiterAgent.autoBraking = false;
    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        if (biterEnemy_AI_Controller.isLeader)
        {
            if (!biterEnemy_AI_Controller.BiterAgent.pathPending && biterEnemy_AI_Controller.BiterAgent.remainingDistance < 0.5f)
            {
                Debug.Log("GoToNextPoint");
                GoToNextPoint(biterEnemy_AI_Controller);
            }

            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.BiterAgent.destination);
        }

        if (biterEnemy_AI_Controller.isFollower)
        {
            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.Leader.transform.position + biterEnemy_AI_Controller.offset);
        }

    }
    public override void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {

    }

    public void GoToNextPoint(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
        if (biterEnemy_AI_Controller.patrolPath.waypoints.Count == 0)
            return;
        biterEnemy_AI_Controller.BiterAgent.destination = biterEnemy_AI_Controller.patrolPath.waypoints[biterEnemy_AI_Controller.patrolWaypointIndex].position;
        biterEnemy_AI_Controller.patrolWaypointIndex = (biterEnemy_AI_Controller.patrolWaypointIndex + 1) % biterEnemy_AI_Controller.patrolPath.waypoints.Count;
    }

}

