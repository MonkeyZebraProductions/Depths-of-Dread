using System.Collections;
using UnityEngine;

public class Biter_AI_AttackState : Biter_AI_BaseState
{
    public float rotationSpeed = 10;
    public override void EnterState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        //Steer
        biterEnemy_AI_Controller.BiterAgent.speed = 1.75f;
        biterEnemy_AI_Controller.BiterAgent.angularSpeed = 60f;
        biterEnemy_AI_Controller.BiterAgent.acceleration = 4f;
        biterEnemy_AI_Controller.BiterAgent.stoppingDistance = 3f;
        biterEnemy_AI_Controller.BiterAgent.autoBraking = false;
    }
    public override void UpdateState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        EncirclePlayer(biterEnemy_AI_Controller);

        Vector3 facingPlayer = biterEnemy_AI_Controller.player.transform.position - biterEnemy_AI_Controller.transform.position;
        facingPlayer.y = 0;
        Quaternion rot = Quaternion.LookRotation(facingPlayer);
        biterEnemy_AI_Controller.transform.rotation = Quaternion.Lerp(biterEnemy_AI_Controller.transform.rotation, rot, rotationSpeed * Time.deltaTime);

        /*
        if (biterEnemy_AI_AttackRadius.Attacking)
        {
            biterEnemy_AI_AttackRadius.StartCoroutine(CountdownToRetreat(biterEnemy_AI_Controller,biterEnemy_AI_AttackRadius));
        }

        */





    }
    public override void ExitState(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        biterEnemy_AI_AttackRadius.StopCoroutine(CountdownToRetreat(biterEnemy_AI_Controller,biterEnemy_AI_AttackRadius));
  
    }

    private void EncirclePlayer(BiterEnemy_AI_Controller biterEnemy_AI_Controller)
    {
        if(biterEnemy_AI_Controller.BiterAgent.remainingDistance < biterEnemy_AI_Controller.RadiusAroundPlayer)
        {
            for (int i = 0; i < biterEnemy_AI_Controller.Biters.Count; i++)
            {
                biterEnemy_AI_Controller.Biters[i].MoveIntoPosition(new Vector3(
                    biterEnemy_AI_Controller.player.transform.position.x + biterEnemy_AI_Controller.RadiusAroundPlayer * Mathf.Cos(2 * Mathf.PI * i / biterEnemy_AI_Controller.Biters.Count),
                    biterEnemy_AI_Controller.player.transform.position.y,
                    biterEnemy_AI_Controller.player.transform.position.z + biterEnemy_AI_Controller.RadiusAroundPlayer * Mathf.Sin(2 * Mathf.PI * i / biterEnemy_AI_Controller.Biters.Count)));

            }
        }

        
               
    }

    private IEnumerator CountdownToRetreat(BiterEnemy_AI_Controller biterEnemy_AI_Controller, BiterEnemy_AI_AttackRadius biterEnemy_AI_AttackRadius)
    {
        WaitForSeconds Wait = new WaitForSeconds(biterEnemy_AI_AttackRadius.TimerToRetreat);
        biterEnemy_AI_Controller.SwitchState(biterEnemy_AI_Controller.retreatState);
        yield return Wait;
    }

    public void MoveIntoPosition(BiterEnemy_AI_Controller biterEnemy_AI_Controller, Vector3 position)
    {
        biterEnemy_AI_Controller.BiterAgent.SetDestination(position);
    }

   
}



 