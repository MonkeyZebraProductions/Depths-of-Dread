using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGrapplePoint : MonoBehaviour
{
    public float HighlightAmount;
    public Material GrappleMat;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==8)
        {
            GrappleMat = other.gameObject.GetComponent<MeshRenderer>().material;
            GrappleMat.SetColor("_EmissionColor", GrappleMat.color * HighlightAmount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GrappleMat.SetColor("_EmissionColor", GrappleMat.color * 0.5f);
        }
    }
}
