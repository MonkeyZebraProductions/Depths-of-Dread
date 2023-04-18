using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    public float Damage;
    public bool stay;

    private BiterEnemy_AI_Controller b_A_C;
    public string HitAnimationName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.GetComponent<EnemyUnitHealth>() != null)
            {
                other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
                b_A_C = other.GetComponent<BiterEnemy_AI_Controller>();
                b_A_C.BiterAnimator.Play(HitAnimationName);
            }
            if (other.gameObject.GetComponent<NewEnemyHealth>() != null)
            {
                other.gameObject.GetComponent<NewEnemyHealth>().TakeDamage(Damage);
                b_A_C = other.GetComponent<BiterEnemy_AI_Controller>();
                b_A_C.BiterAnimator.Play(HitAnimationName);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyUnitHealth>() != null)
        {
            other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
            b_A_C = other.GetComponent<BiterEnemy_AI_Controller>();
            b_A_C.BiterAnimator.Play(HitAnimationName);
        }
        if (other.gameObject.GetComponent<NewEnemyHealth>() != null)
        {
            other.gameObject.GetComponent<NewEnemyHealth>().TakeDamage(Damage);
            b_A_C = other.GetComponent<BiterEnemy_AI_Controller>();
            b_A_C.BiterAnimator.Play(HitAnimationName);
        }
    }
}
