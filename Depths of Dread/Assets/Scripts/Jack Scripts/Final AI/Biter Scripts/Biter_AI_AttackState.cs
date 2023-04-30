using System.Collections;
using UnityEngine;

public class Biter_AI_AttackState : Biter_AI_BaseState
{
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

        if(biterEnemy_AI_AttackRadius.Attacking)
        {
            biterEnemy_AI_AttackRadius.StartCoroutine(CountdownToRetreat(biterEnemy_AI_Controller,biterEnemy_AI_AttackRadius));
        }

        /*
        if (biterEnemy_AI_Controller.biterEnemy_AI_Attack.hasAttacked)
        {
            Debug.Log("Retreat");
            biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.retreatState);
        }

        */

    }
    public override void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {

        //biterEnemy_AI_Controller.biterEnemy_AI_Attack.hasAttacked = false;
        //biterEnemy_AI_Controller.biterEnemy_AI_Attack.Collider.enabled = false;
        biterEnemy_AI_AttackRadius.StopCoroutine(CountdownToRetreat(biterEnemy_AI_Controller,biterEnemy_AI_AttackRadius));
    
    }

    private IEnumerator CountdownToRetreat(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        WaitForSeconds Wait = new WaitForSeconds(biterEnemy_AI_AttackRadius.TimerToRetreat);
        biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.retreatState);
        yield return Wait;
    }
}



 