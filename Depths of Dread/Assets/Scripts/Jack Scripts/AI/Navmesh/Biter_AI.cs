using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Biter_AI : MonoBehaviour
{
    public Biter_FOV fov;
    public Transform target;
    public Transform CurrentDestination;
    public float UpdateSpeed = 0.1f;

    public NavMeshAgent Agent;

    public Transform[] patrolPoints;
    public int currentPatrolPoint;
   
    public Coroutine currentCoroutine;

    public bool chaseStateActive;
    public bool patrolStateActive;

    public delegate void StateChangeEvent(BiterState oldState, BiterState newState);
    public StateChangeEvent OnStateChange;
    public BiterState DefaultState;
    private BiterState _state;
    public BiterState State
    {
       get
        {
            return _state;
        }

        set
        {
            OnStateChange?.Invoke(_state, value);
            _state = value;
        }
    }

    // Start is called before the first frame update
    public void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        OnStateChange += StateChange;

       
    }

    // Update is called once per frame
    void Update()
    {
      if(fov.canSeePlayer)
        {
            State = BiterState.Chase;
        }

      if (!fov.canSeePlayer)
        {
            State = BiterState.Patrol;
        }

    }

    private void StateChange(BiterState oldState, BiterState newState)
    {
        if(oldState != newState)
        {
            if(currentCoroutine !=null)
            {
                StopCoroutine(currentCoroutine);
            }

            if(oldState == BiterState.Idle)
            {

            }

            switch(newState)
            {
                case BiterState.Idle:
                    currentCoroutine = StartCoroutine(IdleAction());
                    break;

                case BiterState.Patrol:
                    currentCoroutine = StartCoroutine(PatrolAction());
                    break;

                case BiterState.Chase:
                    currentCoroutine = StartCoroutine(ChaseAction());
                    break;
            }    
        }
    }

    public IEnumerator IdleAction()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateSpeed);

        while (enabled)
        {
            Agent.SetDestination(transform.position);
            yield return wait;
        }

    }

    public IEnumerator PatrolAction()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        //yield return new WaitUntil(() => Agent.enabled && Agent.isOnNavMesh);
        Agent.SetDestination(patrolPoints[currentPatrolPoint].position);

        while (enabled)
        {
            if (Agent.isOnNavMesh && Agent.enabled && Agent.remainingDistance <= Agent.stoppingDistance)
            {
                currentPatrolPoint++;

                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }

                Agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                CurrentDestination = patrolPoints[currentPatrolPoint];
            }

            yield return Wait;
        }
    }

  
    public IEnumerator ChaseAction()
    {

        WaitForSeconds wait = new WaitForSeconds(UpdateSpeed);

        while (enabled)
        {
            Agent.SetDestination(target.transform.position);
            CurrentDestination = target;
            yield return wait;
        }


    }


}
