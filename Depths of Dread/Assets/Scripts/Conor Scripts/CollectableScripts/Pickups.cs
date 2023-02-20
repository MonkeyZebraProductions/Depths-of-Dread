using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public bool RepairKit, AirPack;

    public float RepairAmount;
    public float RefillAmount;
    public float TimeForFloating = 2;

    private AirArmour _aA;
    private Rigidbody _rB;

    private void Start()
    {
        _aA = FindObjectOfType<AirArmour>();
        _rB = GetComponent<Rigidbody>();
        _rB.useGravity = false;
        StartCoroutine(FloatForABit(TimeForFloating));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" || other.gameObject.tag == "GrappleHook")
        {
            if(RepairKit)
            {
                _aA.RecieveArmourRepair(RepairAmount);
            }

            if(AirPack)
            {
                _aA.RefillAir(RefillAmount);
            }
            Destroy(this.gameObject);
        }
    }

    private IEnumerator FloatForABit(float floatTime)
    {
        yield return new WaitForSeconds(floatTime);
        _rB.useGravity = true;
    }
}
