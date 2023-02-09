using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class TriggerUI : MonoBehaviour
{

    public PlayerInput saveInput;
    private InputAction Submit;

    public GameObject UICanvas;

    public UnityEvent TriggerEvent;

    public bool RistrictControl;

    private void Awake()
    {
        Submit = saveInput.actions["Submit"];
        UICanvas.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(RistrictControl)
            {
                saveInput.SwitchCurrentActionMap("Player/UI");
            }

            
            UICanvas.SetActive(true);
            if (Keyboard.current.eKey.isPressed)
            {
                if(RistrictControl)
                {
                    saveInput.SwitchCurrentActionMap("Player");
                }
                TriggerEvent.Invoke();
            }

            if (Gamepad.all.Count > 0 && Gamepad.current.buttonSouth.isPressed)
            {
                if (RistrictControl)
                {
                    saveInput.SwitchCurrentActionMap("Player");
                }
                TriggerEvent.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (RistrictControl)
            {
                saveInput.SwitchCurrentActionMap("Player");
            }

            UICanvas.SetActive(false);
        }
    }
}
