using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public int weaponSlotSelected = 0;
    private int prevWeapon;

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

    public Transform WeaponWheel;
    public Canvas WeaponCanvas;
    public List<CanvasGroup> SlotImages;
    public List<float> WheelRotations;
    private Quaternion TargetRotation;
    float RotateSpeed = 5f;
    float step;
    float smoothTime;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        weaponSwitchingOneKey = playerInput.actions["OneKey"];
        weaponSwitchingTwoKey = playerInput.actions["TwoKey"];
        weaponSwitchingThreeKey = playerInput.actions["ThreeKey"];
        weaponSwitchingFourKey = playerInput.actions["FourKey"];

        SwapWeapon(0);
        WeaponWheel.rotation = Quaternion.Euler(0, 0, WheelRotations[weaponSlotSelected]);
        SlotImages[weaponSlotSelected].alpha = 1;
        ClipCount = SlotImages[weaponSlotSelected].GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ClipCount = SlotImages[weaponSlotSelected].GetComponentInChildren<TextMeshProUGUI>();
        if (weaponSwitchingOneKey.triggered)
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
            WeaponCanvas.enabled = true;
            WeaponModels[weaponSlotSelected].SetActive(true);
            DefaultArm.SetActive(false);
        }
        else
        {
            WeaponCanvas.enabled = false;
            WeaponModels[weaponSlotSelected].SetActive(false);
            DefaultArm.SetActive(true);
        }
        ClipCount.text = ClipAmmo.ToString();
        AmmoCount.text = TotalAmmo.ToString();
        
        step += RotateSpeed * Time.deltaTime;
        TargetRotation = Quaternion.Euler(0, 0, WheelRotations[weaponSlotSelected]);
        WeaponWheel.localRotation = Quaternion.Slerp(WeaponWheel.localRotation, Quaternion.Euler(0, 0, WheelRotations[weaponSlotSelected]), step);
        smoothTime += 2*Time.deltaTime;
        SlotImages[prevWeapon].alpha = Mathf.Lerp(0.2f,0f,smoothTime);
    }


    void SwapWeapon(int weaponType)
    {
        if(weaponType !=weaponSlotSelected)
        {
            WeaponSlots[weaponSlotSelected].SetActive(false);
            SlotImages[weaponSlotSelected].alpha = 0.2f;
            prevWeapon = weaponSlotSelected;
            weaponSlotSelected = weaponType;
            WeaponSlots[weaponSlotSelected].SetActive(true);
            SlotImages[weaponSlotSelected].alpha = 0.75f;
            step = 0;
            smoothTime = 0;
        }
       
    }

}

