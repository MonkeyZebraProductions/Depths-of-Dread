using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class SpikeShotModule : MonoBehaviour
{

    [SerializeField]
    private WeaponScriptableObject WeaponScriptableObject;
    [SerializeField]
    public GrappleSystem grappleSystem;


    // Controls
    public PlayerInput playerInput;
    public InputAction Aim;
    public InputAction Shoot;
    public InputAction Reload;

    // Third Person Aim
    public int PriorityChanger;
    public bool _isAiming, SetAmmotoMax;
    public CinemachineVirtualCamera AimCamera;

    public float lastShootTime;
    public float ChargeTime;
    private float chargeAmount;

    public GameObject muzzle;

    // Weapon Data
    public int _MaxAmmoCount;
    public int _currentAmmoCount;
    public int _weaponRange;
    public int _bulletsPerShot;
    public int _clipSize;
    public float _weaponSpread;
    public float _fireRate;

    public int Ammo;

    public bool isFiring;

    public LineRenderer lr;

    public WeaponSwitching _WS;

    public Animator WeaponAnims;
    public ParticleSystem ChargeParticles;

    public AudioSource Fire;

    public Color LineColor;

    public RaycastHit WeaponHit;

    public GameObject Projectile;

    private AirArmour _aA;

    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        _aA = GetComponentInParent<AirArmour>();

        _MaxAmmoCount = WeaponScriptableObject.MaxAmmoCount;
        if(SetAmmotoMax)
        {
            WeaponScriptableObject.currentAmmoCount= _MaxAmmoCount;
        }
        _currentAmmoCount = WeaponScriptableObject.currentAmmoCount;
        _weaponRange = WeaponScriptableObject.weaponRange;
        _bulletsPerShot = WeaponScriptableObject.bulletsPerShot;
        _weaponSpread = WeaponScriptableObject.weaponSpread;
        _fireRate = WeaponScriptableObject.fireRate;
        _clipSize= WeaponScriptableObject.ClipSize;
        Ammo = _clipSize;

        Aim = playerInput.actions["Aim"];
        Shoot = playerInput.actions["Shoot"];
        Reload = playerInput.actions["Reload"];

        _currentAmmoCount = _MaxAmmoCount;
        lr.sharedMaterial.SetColor("_Color", LineColor);
        _isAiming = _WS.IsAiming;

        lastShootTime = _fireRate;
    }


    public void Update()
    {
        _currentAmmoCount = WeaponScriptableObject.currentAmmoCount;
        if (_currentAmmoCount > _MaxAmmoCount)
        {
            _currentAmmoCount = _MaxAmmoCount;
        }

        _WS.IsAiming = _isAiming;
        _WS.IsFiring = isFiring;
        _WS.ClipAmmo = Ammo;
        _WS.TotalAmmo = _currentAmmoCount;
        if (_isAiming)
        {
            WeaponLaser();
            Physics.Raycast(muzzle.transform.position, transform.forward, out WeaponHit,10000f);
        }
        if (lastShootTime < _fireRate)
        {
            isFiring = false;
            lastShootTime += Time.deltaTime;
        }

        if (isFiring)
        {
            if(Ammo>0)
            {
                Charge();
                if(!ChargeParticles.isPlaying)
                {
                    ChargeParticles.Play();
                }
            }
            else
            {
                WeaponShoot();
            }
            
        }

        if (Reload.triggered && WeaponScriptableObject.currentAmmoCount > 0)
        {
            ReloadFunction();
        }


    }

    public void OnEnable()
    {
        Aim.performed += _ => StartAim();
        Aim.canceled += _ => EndAim();

        Shoot.performed += _ => StartShoot();
        Shoot.canceled += _ => EndShoot();

    }

    public void OnDisable()
    {
        Aim.performed -= _ => StartAim();
        Aim.canceled -= _ => EndAim();

        Shoot.performed -= _ => StartShoot();
        Shoot.canceled -= _ => EndShoot();
    }

    public void StartAim()
    {
        AimCamera.Priority = PriorityChanger;
        _isAiming = true;
        chargeAmount = 0;

    }

    public void WeaponLaser()
    {

            if (WeaponHit.collider && !WeaponHit.collider.gameObject.CompareTag("IgnoreProjectileCollisions"))
            {
                lr.SetPosition(1, new Vector3(0, 0, WeaponHit.distance));
            }
            else
            {
                lr.SetPosition(1, new Vector3(0, 0, 10000f));
            }
        
    }



    public void EndAim()
    {
        AimCamera.Priority = 0;
        _isAiming = false;
        chargeAmount = 0;
        lr.SetPosition(1, new Vector3(0, 0, 0));
        _aA.CancelShake();
        ChargeParticles.Stop();
    }


    public  void StartShoot()
    {
        isFiring = true;
        
    }

    public void EndShoot()
    {
        isFiring = false;
        lr.sharedMaterial.SetColor("_Color", LineColor);
        chargeAmount = 0;
        ChargeParticles.Stop();
    }




    public void RaycastShoot()
    {
        if (_isAiming && !grappleSystem.IsGrappling)
        {

                Instantiate(Projectile, muzzle.transform.position, muzzle.transform.rotation);
                WeaponAnims.Play("FireWeapon");
                Fire.Play();
                Ammo--;
     
        }


    }

    public void WeaponShoot()
    {
            lastShootTime = 0;
            
            if (_currentAmmoCount > 0)
            {
                ReloadFunction();
            }
            else
            {
                Debug.Log("No Ammo");
            }
        

    }

    public void ReloadFunction()
    {
        lastShootTime = 0;
        WeaponScriptableObject.currentAmmoCount -= (_clipSize-Ammo);
        Ammo = _clipSize;
        chargeAmount = 0;
    }

    public void Charge()
    {
        
            if (chargeAmount >= ChargeTime)
            {
                RaycastShoot();
                ChargeParticles.Stop();
                lastShootTime = 0;
                chargeAmount = 0;
            }
            else
            {
                Debug.Log("Hi");
                chargeAmount += Time.deltaTime;
            }
       
    }
}