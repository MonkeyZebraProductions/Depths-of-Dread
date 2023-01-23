using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : WeaponSystemController
{
    /*
    public override void FireAction()
    {
        if (Shoot.triggered && _isAiming && WeaponScriptableObject.currentAmmoCount > 0)
        {
            WeaponScriptableObject.currentAmmoCount--;
            for (int i = 0; i < WeaponScriptableObject.bulletsPerShot; i++)
            {
                RaycastHit shotgunRaycastHit;


                if (Physics.Raycast(WeaponScriptableObject.weaponMuzzle.transform.position, GetShootingDirection(), out shotgunRaycastHit, WeaponScriptableObject.weaponRange))
                {
                    Debug.DrawLine(WeaponScriptableObject.weaponMuzzle.transform.position, shotgunRaycastHit.point, Color.green, 5f);
                    HitBulletTrail(shotgunRaycastHit.point);

                    if (shotgunRaycastHit.collider.tag == "Enemy")
                    {
                        Destroy(shotgunRaycastHit.transform.gameObject);
                    }

                }

                else
                {
                    Debug.DrawLine(WeaponScriptableObject.weaponMuzzle.transform.position, AimCamera.transform.forward + WeaponScriptableObject.direction * WeaponScriptableObject.weaponRange, Color.red, 5f);
                    MissBulletTrail(WeaponScriptableObject.direction);
                }
            }
        }
    }
    */

    /*
     //nextTimeToFire = Time.time + 1f / fireRate;
            WeaponScriptableObject.currentAmmoCount--;

            for (int i = 0; i < WeaponScriptableObject.bulletsPerShot; i++)
            {
                RaycastHit shotgunRaycastHit;

                if (Physics.Raycast(AimCamera.transform.position, AimCamera.transform.position, out shotgunRaycastHit, 100))
                {
                    Debug.Log(shotgunRaycastHit.transform.name);

                    BulletTrail(shotgunRaycastHit.point);

                    if (shotgunRaycastHit.collider.tag == "Enemy")
                    {
                        Destroy(shotgunRaycastHit.transform.gameObject);
                    }

                }
            }
    
    public override void ResetShot()
    {
        WeaponScriptableObject.readyToShoot = true;
    }
    

    void Awake()
    {
        base.Awake();
        WeaponScriptableObject.weaponMuzzle = GameObject.Find("Shotgun Muzzle");
    }


    void Update()
    {
        base.Update();
    }

   */
}

