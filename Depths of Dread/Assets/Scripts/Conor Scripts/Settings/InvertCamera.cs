using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InvertCamera : MonoBehaviour
{
    private CinemachinePOV NormalAxis, AimAxis;
    public CinemachineVirtualCamera NormalCam, AimCam;
    private bool InvertX, InvertY;
    // Start is called before the first frame update
    void Start()
    {
        NormalAxis = NormalCam.GetCinemachineComponent<CinemachinePOV>();
        AimAxis = AimCam.GetCinemachineComponent<CinemachinePOV>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InvertX)
        {
            NormalAxis.m_HorizontalAxis.m_InvertInput = true;
            AimAxis.m_HorizontalAxis.m_InvertInput = true;
        }
        else
        {
            NormalAxis.m_HorizontalAxis.m_InvertInput = false;
            AimAxis.m_HorizontalAxis.m_InvertInput = false;
        }
        if (InvertY)
        {
            NormalAxis.m_VerticalAxis.m_InvertInput = false;
            AimAxis.m_VerticalAxis.m_InvertInput = false;
        }
        else
        {
            NormalAxis.m_VerticalAxis.m_InvertInput = true;
            AimAxis.m_VerticalAxis.m_InvertInput = true;
        }
    }

    public void ChangeX()
    {
        InvertX = !InvertX;
    }

    public void ChangeY()
    {
        InvertY = !InvertY;
    }
}
