using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    public float Damage;
    public bool stay;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.GetComponent<EnemyUnitHealth>() != null)
            {
                other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
            }
            if (other.gameObject.GetComponent<NewEnemyHealth>() != null)
            {
                other.gameObject.GetComponent<NewEnemyHealth>().TakeDamage(Damage);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyUnitHealth>() != null)
        {
            other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
        }
        if (other.gameObject.GetComponent<NewEnemyHealth>() != null)
        {
            other.gameObject.GetComponent<NewEnemyHealth>().TakeDamage(Damage);
        }
    }
}
