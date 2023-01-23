using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class RaycastStandardShotModule : MonoBehaviour
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
    public bool _isAiming;
    public CinemachineVirtualCamera AimCamera;

    public float lastShootTime;

    public Transform muzzle;

    // Weapon Data
    public int _MaxAmmoCount;
    public int _currentAmmoCount;
    public int _weaponRange;
    public int _bulletsPerShot;
    public float _weaponSpread;
    public float _fireRate;

    public bool isFiring;

    public LineRenderer lr;

    public Color LineColor;

    public RaycastHit WeaponHit;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();

        _MaxAmmoCount = WeaponScriptableObject.MaxAmmoCount;
        _currentAmmoCount = WeaponScriptableObject.currentAmmoCount;
        _weaponRange = WeaponScriptableObject.weaponRange;
        _bulletsPerShot = WeaponScriptableObject.bulletsPerShot;
        _weaponSpread = WeaponScriptableObject.weaponSpread;
        _fireRate = WeaponScriptableObject.fireRate;

        Aim = playerInput.actions["Aim"];
        Shoot = playerInput.actions["Shoot"];
        Reload = playerInput.actions["Reload"];

        _currentAmmoCount = _MaxAmmoCount;
        lr.sharedMaterial.SetColor("_Color", LineColor);
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
        AimCamera.Priority += PriorityChanger;
        _isAiming = true;
    }

    public void EndAim()
    {
        AimCamera.Priority -= PriorityChanger;
        _isAiming = false;
        lr.SetPosition(1, new Vector3(0, 0, 0));
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

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(muzzle.position, muzzle.forward * 20, Color.green);

        _currentAmmoCount = Mathf.Clamp(_currentAmmoCount, 0, _MaxAmmoCount);

        if (Shoot.IsPressed())
        {
            WeaponShoot();
        }

    }

    public void RaycastShoot()
    {
        if (_isAiming && !grappleSystem.IsGrappling && _currentAmmoCount > 0)
        {
            for (int i = 0; i < _bulletsPerShot; i++)
            {
                _currentAmmoCount--;
                RaycastHit hit;
                if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, _weaponRange))
                {
                    Debug.DrawRay(muzzle.position, hit.point, Color.blue, 5f);
                    if (hit.collider.tag == "Enemy")
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        hit.transform.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(1);
                    }
                }
            }
        }
    }

    public void WeaponShoot()
    {
        if (Time.time > lastShootTime + _fireRate)
        {
            lastShootTime = Time.time;
            RaycastShoot();
            Debug.Log("RaycastShoot");
        }
    }
}
