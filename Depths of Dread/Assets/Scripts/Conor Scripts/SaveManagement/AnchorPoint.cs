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

    public void SetPoints()
    {
        _sLG.CurrentAnchorPointNumber = AnchorPointNumber;
        _sLG.SaveState();
    }
   
}
