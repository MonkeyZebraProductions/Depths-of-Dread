using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Punch : MonoBehaviour
{
    [Header("Punch")]
    public GameObject PunchBox, SlamBox;
    public bool SlamUpgrade;
    public float PunchDelay, PunchLasting, PunchCooldown;
    private bool _canPunch;

    private PlayerMovementScript _pMS;
    public WeaponSwitching _WS;

    private PlayerInput playerInput;
    private InputAction Puncher;

    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        Puncher = playerInput.actions["Shoot"];

        _pMS = GetComponentInParent<PlayerMovementScript>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        _canPunch = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Puncher.triggered && !_WS.IsAiming && _canPunch)
        {
            StartCoroutine(PunchEnum());
        }
    }

    IEnumerator PunchEnum()
    {
        _pMS.CanMove = false;
        _WS.IsAiming = false;
        yield return new WaitForSeconds(PunchDelay);
        if (SlamUpgrade)
        {
            SlamBox.SetActive(true);
        }
        else
        {
            PunchBox.SetActive(true);
        }
        yield return new WaitForSeconds(PunchDelay);
        if (SlamUpgrade)
        {
            SlamBox.SetActive(false);
        }
        else
        {
            PunchBox.SetActive(false);
        }
        _pMS.CanMove = true;
        _WS.IsAiming = false;
        _canPunch = false;
        yield return new WaitForSeconds(PunchCooldown);
        _canPunch = true;
    }
}
