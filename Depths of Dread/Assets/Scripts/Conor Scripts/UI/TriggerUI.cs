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
    public Animator UIAnimatior;

    public UnityEvent TriggerEvent;

   // public bool RistrictControl;

   

    private void Awake()
    {
        Submit = saveInput.actions["Submit"];
        UICanvas.SetActive(false);
    }

    private void Update()
    {
        
        
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
            StartCoroutine(CloseMessage());
        }
    }

    public void CloseVoid()
    {
        transform.position = Vector3.zero;
    }

    private IEnumerator CloseMessage()
    {
        UIAnimatior.Play("Message Close");
        yield return new WaitForSeconds(0.42f);
        UICanvas.SetActive(false);
    }
}
