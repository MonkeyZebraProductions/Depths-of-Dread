using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOutOfWall : MonoBehaviour
{
    public float smoothTime = 0.5F;
    public bool moveback,grappled;
    public Vector3 movePos;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        
    }

    void Update()
    {
        if(moveback && grappled)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, movePos, ref velocity, smoothTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer==12)
        {
            moveback = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            moveback = true;
        }
    }

}
