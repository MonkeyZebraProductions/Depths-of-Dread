using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BiterEnemy_AI_Controller : MonoBehaviour
{
    [SerializeField]
    private string currentStateName;
    private Biter_AI_BaseState currentState;
    private Biter_AI_BaseState defaultState;

    public Biter_AI_WanderState wanderState = new Biter_AI_WanderState();
    public Biter_AI_PatrolState patrolState = new Biter_AI_PatrolState();
    public Biter_AI_ChaseState chaseState = new Biter_AI_ChaseState();
    public Biter_AI_RetreatState retreatState = new Biter_AI_RetreatState();
    public Biter_AI_AttackState attackState = new Biter_AI_AttackState();

    public BiterEnemy_AI_LineOfSight biterEnemy_AI_LineOfSight;
    public BiterEnemy_AI_Attack biterEnemy_AI_Attack;

    public PlayerMovementScript playerMovementScript;
    public GameObject player;
    public NavMeshAgent BiterAgent;
    public Vector3 nextLocation;
    public float StateUpdateRate = 0.1f;
    public float wanderLocationRadius = 4f;
    public float wanderMoveSpeedMultipler = 0.5f;
    public float attackDistance = 2.5f;
    public float wanderDistance = 10f;
    public float retreatDistance = 10f;

    public bool Wandering;
    public bool Patroling;

    public bool isLeader;
    public bool isFollower;
    public BiterEnemy_AI_Controller Leader;
    public BiterEnemy_AI_Controller Follower1;
    public BiterEnemy_AI_Controller Follower2;
    public BiterEnemy_AI_Controller Follower3;
    public BiterEnemy_AI_Controller Follower4;
    public Vector3 offset;

    public bool justGotHit = false;
    public AudioSource ClickingSfx;
    public float attackingDistance = 4f;

    public Animator BiterAnimator;
     void Awake()
    {
        BiterAgent = GetComponent<NavMeshAgent>();
        
        biterEnemy_AI_LineOfSight.OnPlayerSightFound += HandleGainSight;
        biterEnemy_AI_LineOfSight.OnPlayerSightLost += HandleLostSight;

        if (Wandering)
        {
            defaultState = wanderState;
        }

        if (Patroling)
        {
            defaultState = patrolState;
        }

        if (isFollower)
        {
            offset = gameObject.transform.position - Leader.transform.position;
        }

        if (isLeader)
        {
            offset = gameObject.transform.position;
        }

        currentState = defaultState;

        SwitchState(defaultState);
    }



    void Update()
    {
        currentStateName = currentState.ToString();

        currentState.UpdateState(this);

        if (justGotHit)
        {
            SwitchState(chaseState);
            justGotHit = false;
        }

       

    }

    private void HandleGainSight(PlayerMovementScript playerMovementScript, float time)
    {
        if(currentState != retreatState)
        {
            SwitchState(chaseState);
        }
        BiterAnimator.SetBool("ChaseState", true);

        Debug.Log("Chase");

    }

    private void HandleLostSight(PlayerMovementScript playerMovementScript, float time)
    {
        SwitchState(defaultState);
        Debug.Log("Default");
        BiterAnimator.SetBool("ChaseState", false);
    }

    public void SwitchState(Biter_AI_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private IEnumerator PlaySfx()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        ClickingSfx.Play();
    }




}