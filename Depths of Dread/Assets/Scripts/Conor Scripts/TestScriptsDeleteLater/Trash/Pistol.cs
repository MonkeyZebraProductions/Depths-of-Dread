using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : WeaponSystemController
{
    /*

     public override void FireAction()
     {
         Debug.Log("Pistol FireAction");

         if (Shoot.triggered && _isAiming&& WeaponScriptableObject.readyToShoot && WeaponScriptableObject.currentAmmoCount > 0 )
         {
             WeaponScriptableObject.readyToShoot = false;

             WeaponScriptableObject.currentAmmoCount--;

             for (int i = 0; i < WeaponScriptableObject.bulletsPerShot; i++)
             {
                  RaycastHit pistolRaycastHit; 

                 if (Physics.Raycast(WeaponScriptableObject.weaponMuzzle.transform.position, GetShootingDirection(), out pistolRaycastHit, WeaponScriptableObject.weaponRange))
                 {
                     Debug.DrawLine(WeaponScriptableObject.weaponMuzzle.transform.position, pistolRaycastHit.point, Color.green, 5f);
                     Debug.Log(pistolRaycastHit.transform.name);
                     HitBulletTrail(pistolRaycastHit.point);

                     if (pistolRaycastHit.collider.tag == "Enemy")
                     {
                         Destroy(pistolRaycastHit.transform.gameObject);
                     }

                 }

                 else
                 {
                     Debug.DrawLine(WeaponScriptableObject.weaponMuzzle.transform.position, AimCamera.transform.forward + WeaponScriptableObject.direction * WeaponScriptableObject.weaponRange, Color.red, 5f);
                     MissBulletTrail(WeaponScriptableObject.direction);
                 }

             }

             Invoke("ResetShot", WeaponScriptableObject.timeBetweenShooting);
         }
     }

     public override void ResetShot()
     {
         WeaponScriptableObject.readyToShoot = true;
     }

      void Awake()
     {
         base.Awake();

         WeaponScriptableObject.weaponMuzzle = GameObject.Find("Pistol Muzzle ");

     }

      void Update()
     {
         base.Update();
     }

     private void OnEnable()
     {
         base.OnEnable();
     }

     private void OnDisable()
     {
         base.OnEnable();
     }
    */
}

