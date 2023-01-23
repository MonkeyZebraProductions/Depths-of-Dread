using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SeeThroughBoxMove : MonoBehaviour
{

    private Ray CameraRay;
    private RaycastHit SeePlayer;
    private Camera MainCam;
    private bool _startCheck;

    public Transform Player;
    public GameObject SeeThroughShader;
    public bool ShaderOn;
    public CinemachineVirtualCamera AimCam;
    private CinemachineBrain Brain;
    // Start is called before the first frame update
    private void Awake()
    {
        SeeThroughShader.SetActive(true);
    }
    void Start()
    {
        StartCoroutine(SeeThroughFrameBuffer());
        MainCam = GetComponent<Camera>();
        Brain = MainCam.GetComponent<CinemachineBrain>();
    }

    IEnumerator SeeThroughFrameBuffer()
    {
        ShaderOn = true;
        yield return new WaitForSeconds(0.4f);
        ShaderOn = false;
        _startCheck = true;
    }
    // Update is called once per frame
    void Update()
    {
        CameraRay = new Ray(MainCam.transform.position, (Player.position - MainCam.transform.position).normalized);

        if (Physics.Raycast(CameraRay, out SeePlayer) && _startCheck)
        {
            if (SeePlayer.collider.gameObject.layer==11)
            {
                ShaderOn = false;
                Debug.Log("Can See");
            }
            else if(CinemachineCore.Instance.IsLive(AimCam))
            {
                ShaderOn = true;
                Debug.Log("On");
            }
            else
            {
                ShaderOn = false;
            }
        }

        if (ShaderOn)
        {
            SeeThroughShader.SetActive(true);
        }
        else
        {
            SeeThroughShader.SetActive(false);
        }

    }
}
