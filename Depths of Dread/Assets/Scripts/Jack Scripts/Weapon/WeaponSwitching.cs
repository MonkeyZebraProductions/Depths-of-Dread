using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    public int weaponSlotSelected = 0;

    public List<GameObject> WeaponSlots;
    public List<GameObject> WeaponModels;
    public GameObject DefaultArm;

    public PlayerInput playerInput;

    public InputAction weaponSwitchingOneKey, weaponSwitchingTwoKey, weaponSwitchingThreeKey, weaponSwitchingFourKey;
    


    public GrappleSystem grappleSystem;
    public Punch punch;

    public bool IsAiming,IsFiring;

    public int ClipAmmo, TotalAmmo;

    public TextMeshProUGUI ClipCount, AmmoCount;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        weaponSwitchingOneKey = playerInput.actions["OneKey"];
        weaponSwitchingTwoKey = playerInput.actions["TwoKey"];
        weaponSwitchingThreeKey = playerInput.actions["ThreeKey"];
        weaponSwitchingFourKey = playerInput.actions["FourKey"];

        SwapWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponSwitchingOneKey.triggered)
        {
            SwapWeapon(0);
        }

        if(weaponSwitchingTwoKey.triggered)
        {
            SwapWeapon(1);
        }

        if (weaponSwitchingThreeKey.triggered)
        {
            SwapWeapon(2);
        }

        if (weaponSwitchingFourKey.triggered)
        {
            SwapWeapon(3);
        }

        if (IsAiming)
        {
            ClipCount.enabled = true;
            AmmoCount.enabled = true;
        }
        else
        {
            ClipCount.enabled = false;
            AmmoCount.enabled = false;
        }
        ClipCount.text = ClipAmmo.ToString();
        AmmoCount.text = TotalAmmo.ToString();

        if (IsAiming)
        {
            WeaponModels[weaponSlotSelected].SetActive(true);
            DefaultArm.SetActive(false);
        }
        else
        {
            WeaponModels[weaponSlotSelected].SetActive(false);
            DefaultArm.SetActive(true);
        }
    }


    void SwapWeapon(int weaponType)
    {
        if(weaponType !=weaponSlotSelected)
        {
            WeaponSlots[weaponSlotSelected].SetActive(false);
            weaponSlotSelected = weaponType;
            WeaponSlots[weaponSlotSelected].SetActive(true);
        }
       
    }

}

