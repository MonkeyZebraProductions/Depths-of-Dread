using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class AimChanger : MonoBehaviour
{
    public float VerticalAimPC, HorizontalAimPC, VerticalAimConsole, HorizontalAimConsole;
    public PlayerInput PI;
    public CameaSensitivity MouseSensitivity, MouseAimSensitivity, StickSensitivity, StickAimSensitivity;

    private CinemachineVirtualCamera Cam;
    private CinemachinePOV pOV;
    private WeaponSwitching _ws;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GetComponent<CinemachineVirtualCamera>();
        pOV = Cam.GetCinemachineComponent<CinemachinePOV>();
        _ws = FindObjectOfType<WeaponSwitching>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PI.currentControlScheme == "Gamepad")
        {
            if(_ws.IsAiming)
            {
                pOV.m_HorizontalAxis.m_MaxSpeed = HorizontalAimConsole * (1 + StickAimSensitivity.Sensitivity) / 2;
                pOV.m_VerticalAxis.m_MaxSpeed = VerticalAimConsole * (1 + StickAimSensitivity.Sensitivity) / 2;
            }
            else
            {
                pOV.m_HorizontalAxis.m_MaxSpeed = HorizontalAimConsole * (1 + StickSensitivity.Sensitivity) / 2;
                pOV.m_VerticalAxis.m_MaxSpeed = VerticalAimConsole * (1 + StickSensitivity.Sensitivity) / 2;
            }
        }
        if (PI.currentControlScheme == "Keyboard&Mouse")
        {
            if (_ws.IsAiming)
            {
                pOV.m_HorizontalAxis.m_MaxSpeed = HorizontalAimPC * (1 + MouseAimSensitivity.Sensitivity) / 2;
                pOV.m_VerticalAxis.m_MaxSpeed = VerticalAimPC * (1 + MouseAimSensitivity.Sensitivity) / 2;
            }
            else
            {
                pOV.m_HorizontalAxis.m_MaxSpeed = HorizontalAimPC * (1 + MouseSensitivity.Sensitivity) / 2;
                pOV.m_VerticalAxis.m_MaxSpeed = VerticalAimPC * (1 + MouseSensitivity.Sensitivity) / 2;
            }
        }
    }
}
