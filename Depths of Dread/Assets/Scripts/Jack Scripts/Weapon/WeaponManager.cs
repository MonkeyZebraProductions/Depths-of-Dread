using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponSlot;
    [SerializeField]
    private GameObject currentWeapon;
    [SerializeField]
    private WeaponScriptableObject equippedWeapon;

   public void EquipWeapon(WeaponScriptableObject weaponScriptableObject)
    {
        equippedWeapon = weaponScriptableObject;
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);   
        }

        Debug.Log("PickUp");
        currentWeapon = Instantiate(weaponScriptableObject.weaponPrefab);
        currentWeapon.transform.SetParent(weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
