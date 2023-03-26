using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDust : MonoBehaviour
{
    public ParticleSystem SandParticles;    
    private void OnTriggerEnter(Collider other)
    {
        SandParticles.Play();
        //Debug.LogError("Poof");
    }
}
