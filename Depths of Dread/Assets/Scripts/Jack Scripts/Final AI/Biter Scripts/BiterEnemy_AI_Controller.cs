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

    public BiterEnemy_AI_LineOfSight lineOfSight;
    public BiterEnemy_AI_AttackRadius attackRadius;

    public PlayerMovementScript playerMovementScript;
    public GameObject player;
    public NavMeshAgent BiterAgent;
    public Vector3 nextLocation;
    public float StateUpdateRate = 0.1f;

    public float wanderLocationRadius = 4f;
    public float wanderMoveSpeedMultipler = 0.5f;

    public BigBadPatrolPath patrolPath;
    public int patrolWaypointIndex = 0;
    public int maxPatrolWaypointsNumber = 0;

    public float attackDistance = 2.5f;
    public float wanderDistance = 10f;
    public float retreatDistance = 10f;

    public bool Wandering;
    public bool Patroling;

    public bool isLeader;
    public bool isFollower;
    public Transform Leader;
    public Vector3 offset;

    public bool justGotHit = false;
    public AudioSource ClickingSfx;
    public float attackingDistance = 4f;

    public Animator BiterAnimator;

    public BiterEnemy_AI_Controller biter1, biter2, biter3, biter4;

    public  List<BiterEnemy_AI_Controller> Biters = new List<BiterEnemy_AI_Controller>();
    public float RadiusAroundPlayer = 1;
    void Awake()
    {
        BiterAgent = GetComponent<NavMeshAgent>();

        lineOfSight = GetComponentInChildren<BiterEnemy_AI_LineOfSight>();
        attackRadius = GetComponentInChildren<BiterEnemy_AI_AttackRadius>();

        lineOfSight.OnPlayerSightFound += HandleGainSight;
        lineOfSight.OnPlayerSightLost += HandleLostSight;

        maxPatrolWaypointsNumber = patrolPath.waypoints.Count;

        if (gameObject.tag == "Biter Leader")
        {
            //Debug.Log("Biter Leader");
            isLeader = true;
            isFollower = false;
            Leader = gameObject.transform;
            offset = Vector3.zero;
        }

        if (gameObject.tag == "Biter Follower")
        {
            //Debug.Log("Biter Leader");
            isLeader = false;
            isFollower = true;
            offset = gameObject.transform.position - Leader.transform.position;
        }

        if (Wandering)
        {
            defaultState = wanderState;
        }

        if (Patroling)
        {
            defaultState = patrolState;
        }

        currentState = defaultState;

        SwitchState(defaultState);
    }



    void Update()
    {
        currentStateName = currentState.ToString();

        currentState.UpdateState(this, attackRadius);

        if (justGotHit)
        {
            SwitchState(chaseState);
            justGotHit = false;
        }

        if (currentState == chaseState)
        {
            for (int i = 0; i < Biters.Count; i++)
            {
                Biters[i].SwitchState(chaseState);
            }
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
        state.EnterState(this, attackRadius);
    }

    private IEnumerator PlaySfx()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        ClickingSfx.Play();
    }


    public void MoveIntoPosition(Vector3 position)
    {
        BiterAgent.SetDestination(position);
    }

   
}