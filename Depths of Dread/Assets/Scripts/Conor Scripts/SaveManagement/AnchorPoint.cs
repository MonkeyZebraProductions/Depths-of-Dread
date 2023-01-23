using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnchorPoint : MonoBehaviour
{

    public int AreaNumber;
    public int AnchorPointNumber;
    public GameObject SaveCanvas;

    public PlayerInput saveInput;
    private InputAction Submit;

    private SaveLoadGame _sLG;

    private void Awake()
    {
        Submit = saveInput.actions["Submit"];
        _sLG = FindObjectOfType<SaveLoadGame>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            saveInput.SwitchCurrentActionMap("Player/UI");
            SaveCanvas.SetActive(true);
            if(Submit.IsPressed())
            {
                _sLG.CurrentAreaNumber = AreaNumber;
                _sLG.CurrentAnchorPointNumber = AnchorPointNumber;
                _sLG.SaveState();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            saveInput.SwitchCurrentActionMap("Player");
            SaveCanvas.SetActive(false);
        }
    }
}
