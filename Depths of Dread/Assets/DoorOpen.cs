using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private bool _doorOpen;
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        door.SetBool("DoorOpen", _doorOpen);
    }

    public void OpenDoor()
    {
        _doorOpen = true;
    }
    
    public void CloseDoor()
    {
        _doorOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        OpenDoor();
    }
    private void OnTriggerExit(Collider other)
    {
        CloseDoor();
    }
}
