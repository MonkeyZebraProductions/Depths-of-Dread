using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public WeaponScriptableObject WeaponToAddAmmoTo;
    public int AmmoAmmount;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            WeaponToAddAmmoTo.currentAmmoCount+=AmmoAmmount;
            Destroy(this.gameObject);
        }
    }
}
