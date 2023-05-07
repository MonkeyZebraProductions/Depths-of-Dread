using System.Collections;
using UnityEngine;

public class Biter_AI_ChaseState : Biter_AI_BaseState
{
    public float rotationSpeed = 10;
    public float offsetdistance = 3;
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        biterEnemy_AI_Controller.BiterAgent.speed = 3.5f;
        biterEnemy_AI_Controller.BiterAgent.angularSpeed = 120f;
        biterEnemy_AI_Controller.BiterAgent.acceleration = 8f;
        biterEnemy_AI_Controller.BiterAgent.stoppingDistance = 3f;
        biterEnemy_AI_Controller.BiterAgent.autoBraking = false;
    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        biterEnemy_AI_Controller.BiterAgent.destination = (biterEnemy_AI_Controller.player.transform.position);
        biterEnemy_AI_Controller.BiterAgent.SetDestination(biterEnemy_AI_Controller.BiterAgent.destination);

        Vector3 facingPlayer = biterEnemy_AI_Controller.player.transform.position - biterEnemy_AI_Controller.transform.position;
        facingPlayer.y = 0;
        Quaternion rot = Quaternion.LookRotation(facingPlayer);
        biterEnemy_AI_Controller.transform.rotation = Quaternion.Lerp(biterEnemy_AI_Controller.transform.rotation, rot, rotationSpeed * Time.deltaTime);

        //After Attack Countdown Switch to Retreat

    }
    public override void ExitState(BiterEnemy_AI_Controller biter_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {

    }

}