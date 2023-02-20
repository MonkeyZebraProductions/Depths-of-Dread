using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public WeaponScriptableObject WeaponToAddAmmoTo;
    public int AmmoAmmount;
    public float TimeForFloating = 2;

    private Rigidbody _rB;
    private void Start()
    {
        _rB = GetComponent<Rigidbody>();
        _rB.useGravity = false;
        StartCoroutine(FloatForABit(TimeForFloating));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" || other.gameObject.tag == "GrappleHook")
        {
            WeaponToAddAmmoTo.currentAmmoCount+=AmmoAmmount;
            Destroy(this.gameObject);
        }
    }

    private IEnumerator FloatForABit(float floatTime)
    {
        yield return new WaitForSeconds(floatTime);
        _rB.useGravity = true;
    }
}
