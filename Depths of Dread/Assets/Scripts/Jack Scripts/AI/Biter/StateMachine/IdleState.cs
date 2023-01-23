using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override void Enter(StateMachine stateMachine)
    {
        Debug.Log("Hello");
    }
    public override void Think(StateMachine stateMachine)
    {

        if (stateMachine.fov.canSeePlayer)
        {
            stateMachine.SwitchState(stateMachine.pursueState);
            Debug.Log("canSeePlayer");
        }

        if (stateMachine.fov.inAttackRange)
        {
            stateMachine.SwitchState(stateMachine.attackState);
        }

        if (!stateMachine.fov.canSeePlayer && !stateMachine.fov.inAttackRange)
        {
            stateMachine.SwitchState(stateMachine.patrolState);
        }


    }
    public override void Exit(StateMachine stateMachine)
    {

    }
}
