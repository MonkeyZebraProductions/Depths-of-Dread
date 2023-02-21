using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banshee_AI_LineOfSight : MonoBehaviour
{
    public delegate void HandlePlayerSightFound(PlayerMovement playerMovement, float time);
    public delegate void HandlePlayerSightLost(PlayerMovement playerMovement, float time);
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
        PlayerMovement playerMovement;
        if (other.TryGetComponent<PlayerMovement>(out playerMovement))
        {
            OnPlayerSightLost?.Invoke(playerMovement, 0.5f);

            if (CheckLineOfSight(playerMovement))
            {
                CheckForLineOfSightCoroutine = StartCoroutine(CheckLineOfSightCoroutine(playerMovement));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement playerMovement;

        if (other.TryGetComponent<PlayerMovement>(out playerMovement))
        {
            OnPlayerSightLost?.Invoke(playerMovement, 5f);
            if (CheckForLineOfSightCoroutine != null)
            {
                StopCoroutine(CheckForLineOfSightCoroutine);
                CheckForLineOfSightCoroutine = StartCoroutine(LineOfSightDelayLossCoroutine(playerMovement));

            }
        }
    }


    private IEnumerator CheckLineOfSightCoroutine(PlayerMovement playerMovement)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.1f);

        while (!CheckLineOfSight(playerMovement))
        {
            yield return Wait;
        }
    }

    private bool CheckLineOfSight(PlayerMovement playerMovement)
    {
        WaitForSeconds Wait = new WaitForSeconds(5f);
        Vector3 Direction = (playerMovement.transform.position - transform.position).normalized;
        float DotProduct = Vector3.Dot(transform.forward, Direction);

        if (DotProduct >= Mathf.Cos(FieldOfView))
        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, Direction, out Hit, Collider.radius, targetMask))
            {
                if (Hit.transform.GetComponent<PlayerMovement>() != null)
                {
                    OnPlayerSightFound?.Invoke(playerMovement, 0.1f);
                    return true;
                }
            }
        }

        return false;
    }

    private IEnumerator LineOfSightDelayLossCoroutine(PlayerMovement playerMovement)
    {
        WaitForSeconds Wait = new WaitForSeconds(5f);
        CheckLineOfSight(playerMovement);
        yield return Wait;
    }

}
