using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Biter_AI_WanderState : Biter_AI_BaseState
{
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        if (biterEnemy_AI_Controller.isLeader)
        {
            Vector3 random = Random.insideUnitSphere * biterEnemy_AI_Controller.wanderDistance;
            random.y = 0f;
            biterEnemy_AI_Controller.nextLocation = biterEnemy_AI_Controller.BiterAgent.transform.position + random;
            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.nextLocation);
            biterEnemy_AI_Controller.BiterAgent.stoppingDistance = 0.5f;
        }

    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        if (biterEnemy_AI_Controller.isLeader)
        {
            if (biterEnemy_AI_Controller.BiterAgent.remainingDistance < 0.5f)
            {
                Vector3 random = Random.insideUnitSphere * biterEnemy_AI_Controller.wanderDistance;
                random.y = 0f;
                biterEnemy_AI_Controller.nextLocation = biterEnemy_AI_Controller.BiterAgent.transform.position + random;

                if (NavMesh.SamplePosition(biterEnemy_AI_Controller.nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
                {
                    biterEnemy_AI_Controller.nextLocation = hit.position;
                    biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.nextLocation);
                }

            }
        }

        if (biterEnemy_AI_Controller.isFollower)
        {
            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.Leader.transform.position + biterEnemy_AI_Controller.offset);
        }

    }
    public override void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.transform.position);
    }

}

