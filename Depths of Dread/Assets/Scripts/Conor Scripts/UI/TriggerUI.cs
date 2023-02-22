using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Cinemachine;

public class TriggerUI : MonoBehaviour
{

    public PlayerInput saveInput;
    private InputAction Submit;

    public GameObject UICanvas;

    public UnityEvent TriggerEvent;

    public bool RistrictControl;

    public CinemachineInputProvider CIP;
    public CinemachineBrain CB;
    public InputActionReference NormalLookAxis, TutorialAxis;

    private void Awake()
    {
        Submit = saveInput.actions["Submit"];
        UICanvas.SetActive(false);
    }

    private void Update()
    {
        if(RistrictControl)
        {
            CIP = CB.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineInputProvider>();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            

            
            UICanvas.SetActive(true);
            if (Keyboard.current.eKey.isPressed)
            {
                
                TriggerEvent.Invoke();
            }

            if (Gamepad.all.Count > 0 && Gamepad.current.buttonSouth.isPressed)
            {
                TriggerEvent.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {


            UICanvas.SetActive(false);
        }
    }
}
