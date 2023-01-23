using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="GrappleHook")
        {
            //Do a thing
            Destroy(this.gameObject);
        }
    }
}

