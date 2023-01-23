using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class MapControl : MonoBehaviour
{
    public Camera MapCamera;
    public Canvas MapCanvas;
    private PlayerMovementScript _psm;
    public PlayerInput mapInput;
    public CinemachineVirtualCamera AimingCamera;

    public float PanSpeed, RotateSpeed, ZoomSpeed;
    public float MaxRotateXLimit, MinRotateXLimit;

    private bool _mapOpen, _isGlobal;

    private InputAction Pan;
    private InputAction Rotate;
    private InputAction ZoomIn;
    private InputAction ZoomOut;
    private InputAction MoveUp;
    private InputAction MoveDown;
    private InputAction LocalGlobalToggle;
    private InputAction OpenMap;
    private InputAction CloseMap;
    private void Awake()
    {
        _psm = FindObjectOfType<PlayerMovementScript>();
        Pan = mapInput.actions["Pan"];
        Rotate = mapInput.actions["Rotate"];
        ZoomIn = mapInput.actions["ZoomIn"];
        ZoomOut = mapInput.actions["ZoomOut"];
        MoveUp = mapInput.actions["MoveUp"];
        MoveDown = mapInput.actions["MoveDown"];
        LocalGlobalToggle = mapInput.actions["LocalGlobalToggle"];
        OpenMap = mapInput.actions["OpenMap"];
        CloseMap = mapInput.actions["CloseMap"];
    }

    // Update is called once per frame
    void Update()
    {
        if (_mapOpen)
        {
            MapCanvas.enabled = true;
            AimingCamera.Priority = 15;
            
        }
        else
        {
            MapCanvas.enabled = false;
            AimingCamera.Priority = 5;
        }

        Vector2 PanInput = Pan.ReadValue<Vector2>();
        Vector3 movement = transform.rotation *  new Vector3(PanInput.x, 0, PanInput.y);
        transform.position += movement* PanSpeed*Time.deltaTime;

        Vector2 RotateInput = Rotate.ReadValue<Vector2>();
        
        transform.localEulerAngles+=new Vector3(RotateInput.y, RotateInput.x, 0f) * RotateSpeed * Time.deltaTime;
        float angle=transform.eulerAngles.x;

        if(angle>180)
        {
            angle-=360;
        }
        if (angle > MaxRotateXLimit)
        {
            transform.localEulerAngles = new Vector3(MaxRotateXLimit, transform.localEulerAngles.y, 0f);
        }
        else if (angle < MinRotateXLimit)
        {
            transform.localEulerAngles = new Vector3(360+MinRotateXLimit, transform.localEulerAngles.y, 0f);
        }

        

        if (ZoomIn.IsPressed())
        {
            MapCamera.fieldOfView += ZoomSpeed * Time.deltaTime;
        }
        if (ZoomOut.IsPressed())
        {
            MapCamera.fieldOfView -= ZoomSpeed * Time.deltaTime;
        }

        if(MoveUp.IsPressed())
        {
            transform.Translate(0, PanSpeed * Time.deltaTime, 0);
        }
        if (MoveDown.IsPressed())
        {
            transform.Translate(0, -PanSpeed * Time.deltaTime, 0);
        }

        if (LocalGlobalToggle.triggered)
        {
            _isGlobal = !_isGlobal;
        }
        if(_isGlobal)
        {
            MapCamera.cullingMask = (1 << LayerMask.NameToLayer("LocalMap")) | (1 << LayerMask.NameToLayer("AreaMap"));
        }
        else
        {
            MapCamera.cullingMask = 1 << LayerMask.NameToLayer("LocalMap");
        }
       
    }

    private void OnEnable()
    {
        OpenMap.performed += OpenCloseMap;
        CloseMap.performed += OpenCloseMap;
    }

    private void OnDisable ()
    {
        OpenMap.performed -= OpenCloseMap;
        CloseMap.performed -= OpenCloseMap;
    }

    private void OpenCloseMap(InputAction.CallbackContext context)
    {
        _mapOpen = !_mapOpen;
        _psm.CanMove = !_psm.CanMove;
        if(_mapOpen)
        {
            mapInput.SwitchCurrentActionMap("Map");
        }
        else
        {
            mapInput.SwitchCurrentActionMap("Player");
        }
    }
}
