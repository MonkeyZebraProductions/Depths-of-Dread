using System.Collections;
using UnityEngine;

 public class Biter_AI_RetreatState : Biter_AI_BaseState
{
    private int retreatDistance = 5;
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        //Steer
        biterEnemy_AI_Controller.BiterAgent.speed = 1.75f;
        biterEnemy_AI_Controller.BiterAgent.angularSpeed = 60f;
        biterEnemy_AI_Controller.BiterAgent.acceleration = 4f;
        biterEnemy_AI_Controller.BiterAgent.stoppingDistance = 5f;
        biterEnemy_AI_Controller.BiterAgent.autoBraking = false;

    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        biterEnemy_AI_Controller.BiterAgent.destination = (biterEnemy_AI_Controller.player.transform.position);
        biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.BiterAgent.destination);

        if (biterEnemy_AI_Controller.BiterAgent.remainingDistance <= 1)
        {
            biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.chaseState);
        }

        

    }

    public override void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
     
    }

    private void RetreatAction(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
        Vector3 random = Random.insideUnitSphere * biterEnemy_AI_Controller.retreatDistance;
        biterEnemy_AI_Controller.nextLocation = biterEnemy_AI_Controller.playerMovementScript.transform.position + random;
    }


}
