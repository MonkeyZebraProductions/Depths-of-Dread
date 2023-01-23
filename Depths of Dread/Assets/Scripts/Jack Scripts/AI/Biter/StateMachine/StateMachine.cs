using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;
    public FOV fov;

    public IdleState idleState = new IdleState();
    public PursueState pursueState = new PursueState();
    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();

    public void Start()
    {
        currentState = idleState;

        currentState.Enter(this);
    }

    public void Update()
    {
        currentState.Think(this);
    }

    public void SwitchState(BaseState state)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }

        currentState = state;
        state.Enter(this);
    }

}
