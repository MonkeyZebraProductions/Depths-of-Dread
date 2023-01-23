using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockGrappleHook : MonoBehaviour
{
    private GrappleSystem _gS;

    private void Awake()
    {
        _gS = FindObjectOfType<GrappleSystem>();
    }

    public void ActivateGrappleHook()
    {
        
        _gS.enabled = true;
    }
}
