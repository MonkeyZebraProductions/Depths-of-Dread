using System.Collections;
using UnityEngine;

 public class Biter_AI_RetreatState : Biter_AI_BaseState
{
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
        if (biterEnemy_AI_Controller.isLeader)
        {
            RetreatAction(biterEnemy_AI_Controller);
            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.nextLocation);
        }

        if (biterEnemy_AI_Controller.isFollower)
        {
            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.Leader.BiterAgent.transform.position + biterEnemy_AI_Controller.offset);
        }


    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
        if (biterEnemy_AI_Controller.isLeader)
        {
            if (biterEnemy_AI_Controller.BiterAgent.remainingDistance < 1f)
            {
                biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.chaseState);
            }
        }

        if (biterEnemy_AI_Controller.isFollower)
        {
            if (biterEnemy_AI_Controller.BiterAgent.remainingDistance < 1f)
            {
                biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.chaseState);
            }
        }



    }
    public override void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
     
    }

    private void RetreatAction(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
        Vector3 random = Random.insideUnitSphere * biterEnemy_AI_Controller.retreatDistance;
        biterEnemy_AI_Controller.nextLocation = biterEnemy_AI_Controller.playerMovementScript.transform.position + random;
    }

}
