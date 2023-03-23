using System.Collections;
using UnityEngine;

public class Biter_AI_AttackState : Biter_AI_BaseState
{
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {

        biterEnemy_AI_Controller.biterEnemy_AI_Attack.Collider.enabled = true;
    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {

        if (biterEnemy_AI_Controller.biterEnemy_AI_Attack.hasAttacked)
        {
            Debug.Log("Retreat");
            biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.retreatState);
        }

    }
    public override void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {

        biterEnemy_AI_Controller.biterEnemy_AI_Attack.hasAttacked = false;
        biterEnemy_AI_Controller.biterEnemy_AI_Attack.Collider.enabled = false;
    }
}



 