using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDamage : MonoBehaviour
{
    public int Damage;
    public float PunchForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy")
        {
            other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * PunchForce, ForceMode.Impulse);
        }
    }
}
