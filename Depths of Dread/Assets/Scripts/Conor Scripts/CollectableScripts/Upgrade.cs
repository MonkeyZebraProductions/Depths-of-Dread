using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public bool ReducedAirDecreaseRate, AirCapacityUpgrade;

    public float ReductionAmount;
    public float CapacityIncrease;

    private AirArmour _aA;

    private void Start()
    {
        _aA = FindObjectOfType<AirArmour>();
    }

    private void OnTriggerEnter(Collider other)
    {

        //play Animation/Do something Special

        if (other.gameObject.tag == "Player")
        {
            if (ReducedAirDecreaseRate)
            {
                _aA.DecreaseRate(ReductionAmount);
            }

            if (AirCapacityUpgrade)
            {
                _aA.IncreaseAirCapacity(CapacityIncrease);
            }

            Destroy(this.gameObject);
        }
    }
}
