using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPusher : MonoBehaviour
{
    public CinemachineCameraOffset CCO;
    public float RayDistance;
    public LayerMask Mask;

    private Ray LeftRay, RightRay;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RightRay = new Ray(transform.position, transform.right);
        LeftRay = new Ray(transform.position, -transform.right);

        if(Physics.Raycast(RightRay, out hit, RayDistance,Mask))
        {
            float distance = RayDistance - (Vector3.Distance(transform.position, hit.point));
            CCO.m_Offset.x = -distance;
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            CCO.m_Offset.x = 0;
        }

        Debug.DrawRay(transform.position, transform.right * RayDistance, Color.yellow);
    }
}
