using System.Collections;
using UnityEngine;

public class BigBad_AI_ChaseState : BigBad_AI_BaseState
{

    public override void EnterState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
        //Steer
        bigBadEnemy_AI_Controller.BigBadAgent.speed = 3.5f;
        bigBadEnemy_AI_Controller.BigBadAgent.angularSpeed = 120f;
        bigBadEnemy_AI_Controller.BigBadAgent.acceleration = 8f;
        bigBadEnemy_AI_Controller.BigBadAgent.stoppingDistance = 5f;
        bigBadEnemy_AI_Controller.BigBadAgent.autoBraking = false;

        bigBadEnemy_AI_Controller.BigBadAnimator.SetBool("ChaseState", true);

        Debug.Log("Chase");

    }

    public override void UpdateState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
       
        bigBadEnemy_AI_Controller.BigBadAgent.destination = (bigBadEnemy_AI_Controller.Player.transform.position);
        bigBadEnemy_AI_Controller.BigBadAgent.SetDestination(bigBadEnemy_AI_Controller.BigBadAgent.destination);

        

    }

    public override void ExitState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
        bigBadEnemy_AI_Controller.BigBadAnimator.SetBool("ChaseState", false);
    }

  
}

