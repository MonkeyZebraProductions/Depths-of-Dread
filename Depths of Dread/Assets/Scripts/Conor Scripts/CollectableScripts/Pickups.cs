using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public bool RepairKit, AirPack;

    public float RepairAmount;
    public float RefillAmount;

    private AirArmour _aA;

    private void Start()
    {
        _aA = FindObjectOfType<AirArmour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
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
}
