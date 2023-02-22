using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Banshee_AI_Controller : MonoBehaviour
{
    public Banshee_AI_LineOfSight banshee_AI_LineOfSight;
    public GameObject Player;
    private NavMeshAgent Agent;
    private Coroutine MovementCoroutine;
    public Vector3 nextLocation;
    public float StateUpdateRate = 0.1f;
    public float AlarmUpdate = 20f;
    public float wanderLocationRadius = 4f;
    public float wanderMoveSpeedMultipler = 0.5f;

    public Banshee_AI_State defaultState;
    private Banshee_AI_State _state;
    public delegate void StateChangeEvent(Banshee_AI_State oldState, Banshee_AI_State newState);
    public StateChangeEvent OnStateChange;

    public GameObject EnemyPrefab;
    public Vector3 spawner1;
    public Vector3 spawner2;
    public Vector3 spawner3;

    public bool justGotHit = false;

    public Banshee_AI_State State
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
        banshee_AI_LineOfSight.OnPlayerSightFound += HandleGainSight;
        banshee_AI_LineOfSight.OnPlayerSightLost += HandleLostSight;
        OnStateChange += HandleStateChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (justGotHit)
        {
            State = Banshee_AI_State.Alarm;
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
        OnStateChange?.Invoke(Banshee_AI_State.Spawn, defaultState);
    }

    public void HandleGainSight(PlayerMovement playerMovement, float time)
    {
        State = Banshee_AI_State.Alarm;
       
    }

    public void HandleLostSight(PlayerMovement playerMovement, float time)
    {
        State = defaultState;
    }

    private void HandleStateChange(Banshee_AI_State oldState, Banshee_AI_State newState)
    {
        if (oldState != newState)
        {
            switch (newState)
            {
                case Banshee_AI_State.Wander:
                    MovementCoroutine = StartCoroutine(WanderCoroutine());
                    break;

                case Banshee_AI_State.Alarm:
                    StopMovement();
                    MovementCoroutine = StartCoroutine(AlarmCoroutine());
                    break;
            }
        }
    }


    private IEnumerator WanderCoroutine()
    {
        WaitForSeconds Wait = new WaitForSeconds(StateUpdateRate);

        Agent.speed *= wanderMoveSpeedMultipler;

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

    private void StopMovement()
    {
        Agent.SetDestination(gameObject.transform.position);
    }

    private IEnumerator AlarmCoroutine()
    {
        WaitForSeconds AlarmWait = new WaitForSeconds(AlarmUpdate);

        while (gameObject.activeSelf)
        {
            yield return AlarmWait;
            Invoke("SetSpawnEnemiesLocations", AlarmUpdate);
            Invoke("SpawnEnemies", AlarmUpdate);

        }
    }


    private void SetSpawnEnemiesLocations()
    {
        Vector3 random1 = Random.insideUnitSphere * banshee_AI_LineOfSight.Collider.radius;
        random1.y = Agent.transform.position.y;
        Vector3 random2 = Random.insideUnitSphere * banshee_AI_LineOfSight.Collider.radius;
        random2.y = Agent.transform.position.y;
        Vector3 random3 = Random.insideUnitSphere * banshee_AI_LineOfSight.Collider.radius;
        random3.y = Agent.transform.position.y;

        spawner1 = Agent.transform.position + random1;
        spawner2 = Agent.transform.position + random2;
        spawner3 = Agent.transform.position + random3;
    }

    private void SpawnEnemies()
    {
        Instantiate(EnemyPrefab, spawner1, Quaternion.identity);
        Instantiate(EnemyPrefab, spawner2, Quaternion.identity);
        Instantiate(EnemyPrefab, spawner3, Quaternion.identity);
    }



}
