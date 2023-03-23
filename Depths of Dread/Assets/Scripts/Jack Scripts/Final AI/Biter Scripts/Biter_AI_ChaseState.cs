using System.Collections;
using UnityEngine;

public class Biter_AI_ChaseState : Biter_AI_BaseState
{
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {

    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
        if (biterEnemy_AI_Controller.isLeader)
        {
            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.player.transform.position);
        }

        if (biterEnemy_AI_Controller.isFollower)
        {
            biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.player.transform.position);
        }

        if (biterEnemy_AI_Controller.BiterAgent.remainingDistance < biterEnemy_AI_Controller.attackDistance)
        {
            biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.retreatState);
        }
    }
    public override void ExitState(BiterEnemy_AI_Controller biter_AI_Controller)
    {

    }

}