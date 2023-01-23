using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGlobalSwitcher : MonoBehaviour
{
    public List<GameObject> AreasToSwitch;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            foreach(GameObject Area in AreasToSwitch)
            {
                Area.layer = LayerMask.NameToLayer("LocalMap");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject Area in AreasToSwitch)
            {
                Area.layer = LayerMask.NameToLayer("AreaMap");
            }
        }
    }
}
