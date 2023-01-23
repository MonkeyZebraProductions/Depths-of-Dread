using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.GetComponent<Patrol>().enabled = true;
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
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.GetComponent<Patrol>().enabled = false;
    }
}
