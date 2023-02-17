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
            other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && stay)
        {
            other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
        }
    }
}
