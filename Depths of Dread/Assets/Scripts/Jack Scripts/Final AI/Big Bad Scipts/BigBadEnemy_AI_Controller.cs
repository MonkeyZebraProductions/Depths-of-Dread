using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBadEnemy_AI_Controller : MonoBehaviour
{
    [SerializeField]
    private string currentStateName;
    private BigBad_AI_BaseState currentState;
    private BigBad_AI_BaseState defaultState;

    public BigBad_AI_PatrolState patrolState = new BigBad_AI_PatrolState();
    public BigBad_AI_ChaseState chaseState = new BigBad_AI_ChaseState();
    public BigBad_AI_AttackState attackState = new BigBad_AI_AttackState();

    
    public Big_BadEnemy_AI_LineOfSight lineOfSight;
    public BigBad_AI_AttackRadius attackRadius;

    public PlayerMovementScript playerMovementScript;
    public GameObject Player;
    public NavMeshAgent BigBadAgent;
    public Vector3 nextLocation;
    public float StateUpdateRate = 0.1f;

    public BigBadPatrolPath patrolPath;
    public int patrolWaypointIndex = 0;
    public Vector3 CurrentDestination;

    public bool justGotHit = false;
    public AudioSource ClickingSfx;
    public Animator BigBadAnimator;

    public void Awake()
    {
        BigBadAgent = GetComponent<NavMeshAgent>();
        lineOfSight = GetComponentInChildren<Big_BadEnemy_AI_LineOfSight>();
        attackRadius = GetComponentInChildren<BigBad_AI_AttackRadius>();
        Player = GameObject.Find("Player");
      
        if (Player != null)
        {
            playerMovementScript = Player.GetComponent<PlayerMovementScript>();
        }
        
        defaultState = patrolState;
        currentState = defaultState;
        SwitchState(defaultState);

        lineOfSight.OnPlayerSightFound += HandleGainSight;
        lineOfSight.OnPlayerSightLost += HandleLostSight;

    }

    // Update is called once per frame
    void Update()
    {
        currentStateName = currentState.ToString();
        currentState.UpdateState(this, attackRadius);

        if (justGotHit)
        {
            SwitchState(chaseState);
            justGotHit = false;
        }
    }

    public void SwitchState(BigBad_AI_BaseState state)
    {
        currentState = state;
        state.EnterState(this, attackRadius);
    }


    private void HandleGainSight(PlayerMovementScript playerMovementScript, float time)
    {
        SwitchState(chaseState);

    }

    private void HandleLostSight(PlayerMovementScript playerMovementScript, float time)
    {
        SwitchState(defaultState);
    }

    private IEnumerator PlaySfx()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        ClickingSfx.Play();
    }

}
