using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Biter_AI_WanderState : Biter_AI_BaseState
{
    public override void EnterState(BiterEnemy_AI_Controller biter_AI_Controller)
    {
        if (biter_AI_Controller.isLeader)
        {
            Vector3 random = Random.insideUnitSphere * biter_AI_Controller.wanderDistance;
            random.y = 0f;
            biter_AI_Controller.nextLocation = biter_AI_Controller.BiterAgent.transform.position + random;
            biter_AI_Controller.BiterAgent.SetDestination(biter_AI_Controller.nextLocation);
            biter_AI_Controller.BiterAgent.stoppingDistance = 0.5f;
        }

    }
    public override void UpdateState(BiterEnemy_AI_Controller biter_AI_Controller)
    {
        if (biter_AI_Controller.isLeader)
        {
            if (biter_AI_Controller.BiterAgent.remainingDistance < 0.5f)
            {
                Vector3 random = Random.insideUnitSphere * biter_AI_Controller.wanderDistance;
                random.y = 0f;
                biter_AI_Controller.nextLocation = biter_AI_Controller.BiterAgent.transform.position + random;

                if (NavMesh.SamplePosition(biter_AI_Controller.nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
                {
                    biter_AI_Controller.nextLocation = hit.position;
                    biter_AI_Controller.BiterAgent.SetDestination(biter_AI_Controller.nextLocation);
                }

            }
        }

        if (biter_AI_Controller.isFollower)
        {
            biter_AI_Controller.BiterAgent.SetDestination(biter_AI_Controller.Leader.BiterAgent.transform.position + biter_AI_Controller.offset);
        }

    }
    public override void ExitState(BiterEnemy_AI_Controller biter_AI_Controller)
    {
        biter_AI_Controller.BiterAgent.SetDestination(biter_AI_Controller.transform.position);
    }

}

