using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    private SpawnPickup _sP;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _sP = FindObjectOfType<SpawnPickup>();
            _sP.DropPickup(transform);
        }
    }
}
