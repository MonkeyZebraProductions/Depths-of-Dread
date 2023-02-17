using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class AimChanger : MonoBehaviour
{
    public float VerticalAimPC, HorizontalAimPC, VerticalAimConsole, HorizontalAimConsole;
    public PlayerInput PI;
    public CameaSensitivity MouseSensitivity, StickSensitivity;

    private CinemachineVirtualCamera Cam;
    private CinemachinePOV pOV;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GetComponent<CinemachineVirtualCamera>();
        pOV = Cam.GetCinemachineComponent<CinemachinePOV>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PI.currentControlScheme == "Gamepad")
        {
            pOV.m_HorizontalAxis.m_MaxSpeed = HorizontalAimConsole * (1+StickSensitivity.Sensitivity)/2;
            pOV.m_VerticalAxis.m_MaxSpeed = VerticalAimConsole * (1 + StickSensitivity.Sensitivity) / 2;
        }
        if (PI.currentControlScheme == "Keyboard&Mouse")
        {
            pOV.m_HorizontalAxis.m_MaxSpeed = HorizontalAimPC * (1 + MouseSensitivity.Sensitivity) / 2;
            pOV.m_VerticalAxis.m_MaxSpeed = VerticalAimPC * (1 + MouseSensitivity.Sensitivity) / 2;
        }
    }
}
