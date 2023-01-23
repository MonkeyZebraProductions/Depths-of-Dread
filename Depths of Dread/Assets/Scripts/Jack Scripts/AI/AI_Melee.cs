using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AI_Melee : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayerMask;
    private float lastAttack = 0f;
    public float attackRate;

    private void Start()
    {
        
    }

    private void Update()
    {
        Attack();
      
    }

    private void Attack()
    {
        if (Time.time - lastAttack < attackRate)
        {
            return;
        }

        lastAttack = Time.time;

        Collider[] hitplayer = Physics.OverlapSphere(attackPoint.position, attackRange,playerLayerMask);

        foreach(Collider player in hitplayer)
        {
            Debug.Log("AttackHit");
        }

    }

    private void OnDrawGizmos()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
