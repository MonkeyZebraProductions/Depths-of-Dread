using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Biter_AI_Controller : MonoBehaviour
{
    public Biter_AI_LineOfSight biter_AI_LineOfSight;
    public Biter_AI_Attack biter_AI_Attack;

    public GameObject Player;
    private NavMeshAgent Agent;
    private Coroutine MovementCoroutine;
    public Vector3 nextLocation;
    public float StateUpdateRate = 0.1f;
    public float wanderLocationRadius = 4f;
    public float wanderMoveSpeedMultipler = 0.5f;
    public float attackDistance = 2.5f;

    public Biter_AI_State defaultState;
    public Biter_AI_State _state;
    public delegate void StateChangeEvent(Biter_AI_State oldState, Biter_AI_State newState);
    public StateChangeEvent OnStateChange;

    public Transform[] Waypoints;
    [SerializeField]
    private int WaypointIndex = 0;
    public Transform CurrentDestination;

    public bool justGotHit=false;

    public Biter_AI_State State
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

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        biter_AI_LineOfSight = GetComponentInChildren<Biter_AI_LineOfSight>();
        biter_AI_Attack = GetComponentInChildren<Biter_AI_Attack>();
        biter_AI_LineOfSight.OnPlayerSightFound += HandleGainSight;
        biter_AI_LineOfSight.OnPlayerSightLost += HandleLoseSight;
        OnStateChange += HandleStateChange;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(justGotHit)
        {
            State = Biter_AI_State.Chase;
            justGotHit = false;
        }
    }

    private void OnEnable()
    {
        IniziateDefaultState();
    }

    private void OnDisable()
    {
        _state = defaultState;
    }

    public void IniziateDefaultState()
    {
        OnStateChange?.Invoke(Biter_AI_State.Spawn, defaultState);

    }

    private void HandleGainSight(PlayerMovementScript playerMovementScript, float time)
    {
        State = Biter_AI_State.Chase;
    }

    private void HandleLoseSight(PlayerMovementScript playerMovementScript, float time)
    {
        State = defaultState;

    }

     private void HandleStateChange(Biter_AI_State oldState, Biter_AI_State newState)
    {
        if(oldState != newState)
        {
            if(MovementCoroutine !=null)
            {
                StopCoroutine(MovementCoroutine);
            }

            if(oldState == Biter_AI_State.Wander)
            {
                Agent.speed /= wanderMoveSpeedMultipler;
            }

            switch(newState)
            {
                case Biter_AI_State.Wander:
                    MovementCoroutine = StartCoroutine(WanderCoroutine());
                    break;
                case Biter_AI_State.Chase:
                    MovementCoroutine = StartCoroutine(ChaseCoroutine());
                    break;
                case Biter_AI_State.Patrol:
                    MovementCoroutine = StartCoroutine(PatrolCoroutine());
                    break;
                case Biter_AI_State.Retreat:

                    break;
            }
        }
    }

    private IEnumerator WanderCoroutine()
    {
        WaitForSeconds Wait = new WaitForSeconds(StateUpdateRate);

        Agent.speed *= wanderMoveSpeedMultipler;

        yield return new WaitUntil(() => Agent.enabled && Agent.isOnNavMesh);

        while (true)
        {
            if (!Agent.enabled || !Agent.isOnNavMesh)
            {
                yield return Wait;
                Debug.Log("Problem");
            }
            else if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                Vector3 random = Random.insideUnitSphere * wanderLocationRadius;
                random.y = 0f;
                nextLocation = Agent.transform.position + random;


                if (NavMesh.SamplePosition(nextLocation, out NavMeshHit hit, wanderLocationRadius, NavMesh.AllAreas))
                {
                    nextLocation = hit.position;
                    Agent.SetDestination(nextLocation);
                }
            }

            yield return Wait;
        }
    }

    private IEnumerator PatrolCoroutine()
    {
        WaitForSeconds Wait = new WaitForSeconds(StateUpdateRate);
        yield return new WaitUntil(() => Agent.enabled && Agent.isOnNavMesh);
        Agent.SetDestination(Waypoints[WaypointIndex].position);

        while(true)
        {
            if (Agent.isOnNavMesh && Agent.enabled && Agent.remainingDistance <= Agent.stoppingDistance)
            {
                WaypointIndex++;

                if (WaypointIndex >= Waypoints.Length)
                {
                    WaypointIndex = 0;
                }

                Agent.SetDestination(Waypoints[WaypointIndex].position);
                CurrentDestination = Waypoints[WaypointIndex];
            }
            yield return Wait;
        }
    }


    private IEnumerator ChaseCoroutine()
    {
        WaitForSeconds Wait = new WaitForSeconds(StateUpdateRate);

        while (gameObject.activeSelf)
        {
            if (Agent.enabled)
            {
                nextLocation = Player.transform.position;
                Agent.SetDestination(nextLocation);
                CurrentDestination = Player.transform;
                Agent.stoppingDistance = attackDistance;
            }

            yield return Wait;
        }

    }

  
}
