using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void Enter(StateMachine stateMachine);
    public abstract void Think(StateMachine stateMachine);
    public abstract void Exit(StateMachine stateMachine);
}