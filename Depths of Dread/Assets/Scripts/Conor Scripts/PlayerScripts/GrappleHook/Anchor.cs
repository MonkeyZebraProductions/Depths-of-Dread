using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Anchor : MonoBehaviour
{
    private GrapplingHook _gH;
    private GrappleSystem _gS;
    private PlayerMovementScript _pMS;
    // Start is called before the first frame update
    void Start()
    {
        _gH = FindObjectOfType<GrapplingHook>();
        _gS = FindObjectOfType<GrappleSystem>();
        _pMS = FindObjectOfType<PlayerMovementScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==8)
        {
            
            if (other.gameObject.tag == "GrapplePad")
            {
                _gH.IsHooked = true;
                _gH.TargetReached = true;
            }
            else if (other.gameObject.tag == "GrabbableObject")
            {
                
                other.gameObject.transform.SetParent(this.transform);
                other.gameObject.transform.localPosition = Vector3.zero;
                other.transform.localRotation = Quaternion.identity;
                _gH.ObjectGrabbed = true;
                _gS.BringBack();
                
            }
            
        }
        else if(other.gameObject.tag != "IgnoreProjectileCollisions")
        {
            _gS.IsGrappling = false;
            _gS.AimCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _gS.VertSpeed;
            _gS.AimCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = _gS.HorizSpeed;
            Debug.Log(other.gameObject);
            Destroy(this.gameObject);
            _pMS.CanMove = true;

        }
    }
}
