using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    public int weaponSlotSelected = 1;

    public GameObject firstWeaponSlotOPrefab, secondWeaponSlotPrefab;

    public PlayerInput playerInput;

    public InputAction weaponSwitchingOneKey, weaponSwitchingTwoKey;


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

        SwapWeapon(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponSwitchingOneKey.IsPressed())
        {
            if(weaponSlotSelected != 1)
            {
                SwapWeapon(1);
            }
        }

        if(weaponSwitchingTwoKey.IsPressed())
        {
            if (weaponSlotSelected != 2)
            {
                SwapWeapon(2);
            }
        }
        if(IsAiming)
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
    }


    void SwapWeapon(int weaponType)
    {
        if(weaponType == 1)
        {
            weaponSlotSelected = 1;

            firstWeaponSlotOPrefab.SetActive(true);
            secondWeaponSlotPrefab.SetActive(false);
        }

        if(weaponType == 2)
        {
            weaponSlotSelected = 2;

            firstWeaponSlotOPrefab.SetActive(false);
            secondWeaponSlotPrefab.SetActive(true);
        }
}
}
