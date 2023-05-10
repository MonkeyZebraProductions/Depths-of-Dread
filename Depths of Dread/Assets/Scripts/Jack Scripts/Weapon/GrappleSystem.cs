using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using VolumetricLines;       

public class GrappleSystem : MonoBehaviour
{
    private PlayerInput playerInput;

    public WeaponSwitching _WS;

    public CinemachineVirtualCamera AimCam;
    public float HorizSpeed, VertSpeed;

    public List<GameObject> GrappleHookModel;
    //input actions


    private InputAction Grapple;

    [Header("Grapple Hook")]
    //GameObject SetUp
    public GameObject GrappleObject;
    public GrapplingHook _gH, VisibleAnchor;
    public bool IsGrappling;
    private PlayerMovementScript _pMS;
    public Transform SpawnPoint;
    private CharacterController controller;
    public VolumetricLineBehavior VLB;
    

    //bool set up
    public bool _isAiming;
    private bool _moveToGrapple;
    Vector3 moveVector;

    [Header("When Object is Grappled")]
    //WhenObjectIsGrabbed
    public GameObject GrabbedObject;
    private Rigidbody _grappleRigidbody;
    public float LaunchForce;
    public Material GrappleMat;
    public float AlphaAmount=0.2f;
    public float HookMultiplyer = 2;
    private float _currentHookMultiplier;
    public bool GrappleHookActive;

    public AudioSource HookSfx;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        Grapple = playerInput.actions["Grapple"];
        IsGrappling = false;

        controller = GetComponentInParent<CharacterController>();
        _pMS = GetComponentInParent<PlayerMovementScript>();

        //Cursor.lockState = CursorLockMode.Locked;
        HorizSpeed = AimCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
        VertSpeed = AimCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;
        _currentHookMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!_WS.IsAiming && GrabbedObject != null)
        {
            GrabbedObject.GetComponent<MoveOutOfWall>().grappled = false;
            GrabbedObject.GetComponent<MoveOutOfWall>().moveback = false;
            GrabbedObject.transform.parent = null;
            _grappleRigidbody.isKinematic = false;
            _grappleRigidbody.useGravity = true;
            GrappleMat.color = new Color(GrappleMat.color.r, GrappleMat.color.g, GrappleMat.color.b, 2f);
            GrabbedObject = null;
            IsGrappling = false;
        }

        if (Grapple.triggered && _WS.IsAiming && !_WS.IsFiring)
        {
            IsGrappling = true;

            
            if (VisibleAnchor == null && GrabbedObject == null)
            {
                SpawnHook();
            }
            else
            {
                
                if (GrabbedObject != null)
                {
                    GrabbedObject.GetComponent<MoveOutOfWall>().grappled = false;
                    GrabbedObject.GetComponent<MoveOutOfWall>().moveback = false;
                    GrabbedObject.transform.parent = null;
                    //_grappleRigidbody.isKinematic = false;
                    _grappleRigidbody.useGravity = true;
                    _grappleRigidbody.AddForce(transform.forward * LaunchForce, ForceMode.Impulse);
                    GrappleMat.color = new Color(GrappleMat.color.r, GrappleMat.color.g, GrappleMat.color.b, 2f);
                    GrabbedObject = null;
                    IsGrappling = false;
                }
            }
        }

        if (VisibleAnchor.IsHooked && GrappleHookActive)
        {
            //moves the Player if attatched to a point
            _moveToGrapple = true;
            
            _pMS.CanMove = false;
        }
        moveVector = VisibleAnchor.target - transform.position;
        if (VisibleAnchor.transform.position==VisibleAnchor.target)
        {
            BringBack();
        }

        

        //destroys hook once it gets close to the player
        if (Vector3.Distance(VisibleAnchor.transform.position, transform.position) <= VisibleAnchor.SpawnDistance*_currentHookMultiplier)
        {
            foreach (GameObject obj in GrappleHookModel)
            {
                obj.SetActive(true);
            }
            if (VisibleAnchor.ObjectGrabbed)
            {
                GrabbedObject = VisibleAnchor.transform.GetChild(1).gameObject;
                GrabbedObject.transform.SetParent(this.transform);
                GrabbedObject.transform.localPosition = SpawnPoint.localPosition;
                GrabbedObject.transform.localRotation = Quaternion.identity;
                _grappleRigidbody = GrabbedObject.GetComponent<Rigidbody>();
                //_grappleRigidbody.isKinematic = true;
                _grappleRigidbody.useGravity = false;
                GrappleMat = GrabbedObject.GetComponent<MeshRenderer>().material;
                GrappleMat.color = new Color(GrappleMat.color.r, GrappleMat.color.g, GrappleMat.color.b, 1.2f);
                GrabbedObject.GetComponent<MoveOutOfWall>().grappled = true;
            }
            else
            {
                IsGrappling = false;
            }
            Debug.Log("Die");
            VisibleAnchor.IsHooked = false;
            Destroy(VisibleAnchor.gameObject);
            _moveToGrapple = false;
            _pMS.CanMove = true;
            VLB.gameObject.SetActive(false);
            AimCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = VertSpeed;
            AimCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = HorizSpeed;
        }
        if (_moveToGrapple)
        {
            controller.Move(moveVector.normalized * Time.deltaTime * _gH.ZipSpeed);
            //rigidbody.AddForce(moveVector.normalized * _gH.ZipSpeed * 100, ForceMode.Impulse);
            _currentHookMultiplier = HookMultiplyer;
        }
        else
        {
            _currentHookMultiplier = 1;
        }
        VLB.EndPos = new Vector3(0, 0, Vector3.Distance(VisibleAnchor.transform.position, transform.position)-3f);
        
        
    }

    void SpawnHook()
    {
        //spawn Grapple Hook if not visible or Grabbing
        VLB.gameObject.SetActive(true);
        foreach (GameObject obj in GrappleHookModel)
        {
            obj.SetActive(false);
        }
        _gH.target = SpawnPoint.position + SpawnPoint.forward * _gH.Length;
        Instantiate(GrappleObject, SpawnPoint.position + SpawnPoint.forward * _gH.SpawnDistance * 1.1f, Quaternion.identity);
        VisibleAnchor = FindObjectOfType<GrapplingHook>();
        VisibleAnchor.TargetReached = false;
        HookSfx.Play();
        
    }

    public void BringBack()
    {
        //brings hook back
        VisibleAnchor.TargetReached = false;
        VisibleAnchor.target = SpawnPoint.position;
        _pMS.CanMove = false;
        AimCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
        AimCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
    }

    public void ActivateHeavyRetractor()
    {
        GrappleHookActive = true;
    }
}



