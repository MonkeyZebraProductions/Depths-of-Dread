using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class FragmentShotModule : MonoBehaviour
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
    public bool _isAiming,SetAmmotoMax;
    public CinemachineVirtualCamera AimCamera;

    public float lastShootTime;

    public GameObject muzzle;

    public AudioSource Fire;

    // Weapon Data
    public int _MaxAmmoCount;
    public int _currentAmmoCount;
    public int _weaponRange;
    public int _clipSize;
    public int _bulletsPerShot;
    public float _weaponSpread;
    public float _fireRate;

    public int Ammo;

    public bool isFiring;

    public LineRenderer lr;

    public WeaponSwitching _WS;

    public Color LineColor;

    public RaycastHit WeaponHit;

    public Collider ShotgunBox;

    public Animator WeaponAnims;

    private ParticleSystem Blast;
    private AirArmour _aA;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        _aA = GetComponentInParent<AirArmour>();
        _MaxAmmoCount = WeaponScriptableObject.MaxAmmoCount;
        if (SetAmmotoMax)
        {
            WeaponScriptableObject.currentAmmoCount = _MaxAmmoCount;
        }
        _currentAmmoCount = WeaponScriptableObject.currentAmmoCount;
        _weaponRange = WeaponScriptableObject.weaponRange;
        _bulletsPerShot = WeaponScriptableObject.bulletsPerShot;
        _weaponSpread = WeaponScriptableObject.weaponSpread;
        _fireRate = WeaponScriptableObject.fireRate;
        _clipSize = WeaponScriptableObject.ClipSize;
        Ammo = _clipSize;

        Aim = playerInput.actions["Aim"];
        Shoot = playerInput.actions["Shoot"];
        Reload = playerInput.actions["Reload"];

        _currentAmmoCount = _MaxAmmoCount;

        ShotgunBox.enabled = false;
        _isAiming = _WS.IsAiming;
        //_WS.IsFiring = isFiring;
        lr.sharedMaterial.SetColor("_Color", LineColor);
        Blast = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentAmmoCount = WeaponScriptableObject.currentAmmoCount;
        if(_currentAmmoCount>_MaxAmmoCount)
        {
            _currentAmmoCount = _MaxAmmoCount;
        }

        if (isFiring)
        {
            WeaponShoot();
        }

        _WS.IsAiming = _isAiming;
        _WS.IsFiring = isFiring;
        _WS.ClipAmmo = Ammo;
        _WS.TotalAmmo = _currentAmmoCount;

        if (_isAiming)
        {
            WeaponLaser();
            Physics.Raycast(muzzle.transform.position, transform.forward, out WeaponHit, 10000f);
        }

        if (Reload.triggered)
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

    }

    public void WeaponLaser()
    {
        if (WeaponHit.collider)
        {
            if(WeaponHit.collider.name=="Cone")
            {
                Physics.IgnoreCollision(WeaponHit.collider,WeaponHit.collider);
            }
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

        lr.SetPosition(1, new Vector3(0, 0, 0));
        _aA.CancelShake();
    }

    public void StartShoot()
    {
        isFiring = true;
    }

    public void EndShoot()
    {
        isFiring = false;
        lr.sharedMaterial.SetColor("_Color", LineColor);
    }

    public void WeaponShoot()
    {

        if (Time.time > lastShootTime + _fireRate)
        {
            lastShootTime = Time.time;
            
            if(Ammo>0)
            {
                RaycastShoot();
            }
            else if (_currentAmmoCount > 0)
            {
                ReloadFunction();
            }
            else
            {
                Debug.Log("No Ammo");
            }
        }

    }


    public void RaycastShoot()
    {

        if (_isAiming && !grappleSystem.IsGrappling)
        {

           
                StartCoroutine(ShotgunCoroutine());
                WeaponAnims.Play("FireWeapon");
            Fire.Play();
            Blast.Play();
                Ammo--;
            

           
        }


    }

    IEnumerator ShotgunCoroutine()
    {
        ShotgunBox.enabled = true;
        yield return new WaitForSeconds(0.2f);
        ShotgunBox.enabled = false;
    }

    public void ReloadFunction()
    {
        lastShootTime = Time.time;
        WeaponScriptableObject.currentAmmoCount -= (_clipSize - Ammo);
        Ammo = _clipSize;

    }

}
