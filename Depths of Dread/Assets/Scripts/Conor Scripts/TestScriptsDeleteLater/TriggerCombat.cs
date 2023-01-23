using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCombat : MonoBehaviour
{
    private CombatTransition _cT;
    private void Start()
    {
        _cT = FindObjectOfType<CombatTransition>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _cT.SwapTrack(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _cT.SwapTrack(false);
        }
    }
}
