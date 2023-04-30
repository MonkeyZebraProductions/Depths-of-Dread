using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_BadEnemy_AI_LineOfSight : MonoBehaviour
{
    public delegate void HandlePlayerSightFound(PlayerMovementScript playerMovementScript, float time);
    public delegate void HandlePlayerSightLost(PlayerMovementScript playerMovementScript, float time);

    public HandlePlayerSightFound OnPlayerSightFound;
    public HandlePlayerSightLost OnPlayerSightLost;

    public HandlePlayerSightFound OnPlayerSightFoundDelay;
    public HandlePlayerSightLost OnPlayerSightLostDelay;

    public SphereCollider Collider;
    public float FieldOfView = 90f;
    public LayerMask targetMask;
    public float LineOfSightUpdate = 1f;
    private Coroutine CheckForLineOfSightCoroutine;

    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
      
        PlayerMovementScript playerMovementScript;
        if (other.TryGetComponent<PlayerMovementScript>(out playerMovementScript))
        {
            Debug.Log("Player");
            OnPlayerSightLost?.Invoke(playerMovementScript, 0.5f);

            if (CheckLineOfSight(playerMovementScript))
            {
                CheckForLineOfSightCoroutine = StartCoroutine(CheckLineOfSightCoroutine(playerMovementScript));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        PlayerMovementScript playerMovementScript;
        if (other.TryGetComponent<PlayerMovementScript>(out playerMovementScript))
        {
            OnPlayerSightLost?.Invoke(playerMovementScript, 5f);
            if (CheckForLineOfSightCoroutine != null)
            {
                StopCoroutine(CheckForLineOfSightCoroutine);
                CheckForLineOfSightCoroutine = StartCoroutine(LineOfSightDelayLossCoroutine(playerMovementScript));

            }
        }
    }


    private IEnumerator CheckLineOfSightCoroutine(PlayerMovementScript playerMovementScript)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.1f);

        while (!CheckLineOfSight(playerMovementScript))
        {
            yield return Wait;
        }
    }

    public bool CheckLineOfSight(PlayerMovementScript playerMovementScript)
    {
        WaitForSeconds Wait = new WaitForSeconds(5f);
        Vector3 Direction = (playerMovementScript.transform.position - transform.position).normalized;
        float DotProduct = Vector3.Dot(transform.forward, Direction);

        if (DotProduct >= Mathf.Cos(FieldOfView))
        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, Direction, out Hit, Collider.radius, targetMask))
            {
                if (Hit.transform.GetComponent<PlayerMovementScript>() != null)
                {
                    OnPlayerSightFound?.Invoke(playerMovementScript, 0.1f);
                    return true;
                }
            }
        }

        return false;
    }



    private IEnumerator LineOfSightDelayLossCoroutine(PlayerMovementScript playerMovementScript)
    {
        WaitForSeconds Wait = new WaitForSeconds(20f);
        CheckLineOfSight(playerMovementScript);
        yield return Wait;


    }

}
