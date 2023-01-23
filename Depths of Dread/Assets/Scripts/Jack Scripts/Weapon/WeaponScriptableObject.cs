using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Scriptable Object")]
public class WeaponScriptableObject : ScriptableObject
{ 
    public string weaponName;
    public GameObject weaponPrefab;
   
    // Ammo
    public int MaxAmmoCount;
    public int currentAmmoCount;
    public int ClipSize;

    //Range
    public int weaponRange;

    // Amount fired per shot
    public int bulletsPerShot;

    //Weapon Spread
    public float weaponSpread;

    //Fire Rate
    public float fireRate;
    //public bool readyToShoot;

    //Bullet Trail
    public GameObject hitBulletTrail;
    public GameObject missBulletTrail;

    //Bullet Direction
    [HideInInspector]
    public Vector3 direction;
    
}

