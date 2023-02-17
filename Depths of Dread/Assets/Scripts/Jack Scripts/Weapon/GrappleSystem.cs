using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class GrappleSystem : MonoBehaviour
{
    private PlayerInput playerInput;

    public WeaponSwitching _WS;

    public CinemachineVirtualCamera AimCam;
    public float HorizSpeed, VertSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(SpawnPoint.position, SpawnPoint.forward * 20, Color.green);

        if(!_WS.IsAiming && GrabbedObject != null)
        {
            GrabbedObject.GetComponent<MoveOutOfWall>().moveback = false;
            GrabbedObject.transform.parent = null;
            _grappleRigidbody.isKinematic = false;
            _grappleRigidbody.useGravity = true;
            GrappleMat.color = new Color(GrappleMat.color.r, GrappleMat.color.g, GrappleMat.color.b, 1f);
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
                    GrabbedObject.GetComponent<MoveOutOfWall>().moveback = false;
                    GrabbedObject.transform.parent = null;
                    //_grappleRigidbody.isKinematic = false;
                    _grappleRigidbody.useGravity = true;
                    _grappleRigidbody.AddForce(transform.forward * LaunchForce, ForceMode.Impulse);
                    GrappleMat.color = new Color(GrappleMat.color.r, GrappleMat.color.g, GrappleMat.color.b, 1f);
                    GrabbedObject = null;
                    IsGrappling = false;
                }
            }
        }

        if (VisibleAnchor.IsHooked)
        {
            //moves the Player if attatched to a point
            _moveToGrapple = true;
            moveVector = VisibleAnchor.target - transform.position;
            _pMS.CanMove = false;
        }

        if(VisibleAnchor.transform.position==VisibleAnchor.target)
        {
            BringBack();
        }

        

        //destroys hook once it gets close to the player
        if (Vector3.Distance(VisibleAnchor.transform.position, transform.position) <= VisibleAnchor.SpawnDistance)
        {
            if (VisibleAnchor.ObjectGrabbed)
            {
                GrabbedObject = VisibleAnchor.transform.GetChild(0).gameObject;
                GrabbedObject.transform.SetParent(this.transform);
                GrabbedObject.transform.localPosition = SpawnPoint.localPosition;
                GrabbedObject.transform.localRotation = Quaternion.identity;
                _grappleRigidbody = GrabbedObject.GetComponent<Rigidbody>();
                //_grappleRigidbody.isKinematic = true;
                _grappleRigidbody.useGravity = false;
                GrappleMat = GrabbedObject.GetComponent<MeshRenderer>().material;
                GrappleMat.color = new Color(GrappleMat.color.r, GrappleMat.color.g, GrappleMat.color.b, AlphaAmount);
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
            AimCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = VertSpeed;
            AimCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = HorizSpeed;
        }
        if (_moveToGrapple)
        {
            controller.Move(moveVector.normalized * Time.deltaTime * _gH.ZipSpeed);
        }
    }

    void SpawnHook()
    {
        //spawn Grapple Hook if not visible or Grabbing
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
}



