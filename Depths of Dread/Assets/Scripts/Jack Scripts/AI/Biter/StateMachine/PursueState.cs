
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : BaseState
{
    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.GetComponent<Arrive>().enabled = true;
    }

    public override void Think(StateMachine stateMachine)
    {

    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.GetComponent<Arrive>().enabled = false;
    }
}
