using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBad_AI_AttackState : BigBad_AI_BaseState
{
  
    public override void EnterState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {

        //Steer
        bigBadEnemy_AI_Controller.BigBadAgent.speed = 1.75f;
        bigBadEnemy_AI_Controller.BigBadAgent.angularSpeed = 60f;
        bigBadEnemy_AI_Controller.BigBadAgent.acceleration = 4f;
        bigBadEnemy_AI_Controller.BigBadAgent.stoppingDistance = 5f;
        bigBadEnemy_AI_Controller.BigBadAgent.autoBraking = false;

      
    }

    public override void UpdateState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
        // Movement 
        bigBadEnemy_AI_Controller.BigBadAgent.destination = (bigBadEnemy_AI_Controller.Player.transform.position);
        bigBadEnemy_AI_Controller.BigBadAgent.SetDestination(bigBadEnemy_AI_Controller.BigBadAgent.destination);

    }

    public override void ExitState(BigBadEnemy_AI_Controller bigBadEnemy_AI_Controller, BigBad_AI_AttackRadius bigBad_AI_AttackRadius)
    {
        
    }

   

}
