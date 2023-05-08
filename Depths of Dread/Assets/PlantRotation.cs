using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRotation : MonoBehaviour
{

    public Camera cam;
    public float MaxAngle = 20;
    public float Step;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Get the camera's y and x rotations
        float yRotation = cam.transform.rotation.eulerAngles.y-180;
        float xRotation = cam.transform.rotation.eulerAngles.x;

        Debug.Log(xRotation);

       if(xRotation<MaxAngle)
       {
           transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z),Step);
       }
       else
       {
            transform.rotation = transform.rotation;
            Debug.Log("True");
       }
        
    }
}
