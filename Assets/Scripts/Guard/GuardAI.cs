using UnityEngine;
using UnityEngine.AI;

public class GuardAI : StateController
{
    [Header("States")]
    private State patrolState;
    private State alertState;
    private State seesPlayerState;
    private State attackState;
    
    [Header("Patrol Information")]
    [SerializeField] public bool loopPatrol; //Will the guard path to the first node upon reaching the last one or simply go back down the chain

    [Header("Alert Phase Information")]
    [SerializeField] public float timeSeenToAlert;
    [SerializeField] public float alertFadeTime = 10;
    [SerializeField] public float playerDistanceForAlert = 25;

    [Header("Guard stats")]
    [SerializeField] public float patrolMoveSpeed;
    [SerializeField] public float alertMoveSpeed;
    [SerializeField] public float investigationMoveSpeed;

    [Header("Animation stuff")] 
    [SerializeField] public Animator animator;
    private NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        InitializeGuardAI();
    }

    void FixedUpdate()
    {
        SetAnimation();
    }
    
    
    /// <summary>
    /// Grab component references and set initial state for the guard AI
    /// </summary>
    private void InitializeGuardAI()
    {
        navAgent = GetComponent<NavMeshAgent>();
        patrolState = GetComponent<GS_Patrol>();
        alertState = GetComponent<GS_Alert>();
        seesPlayerState = GetComponent<GS_SeesPlayer>();
        attackState = GetComponent<GS_Attack_Melee>();
        
        alertState.enabled = false;
        LoadState(patrolState);
    }
    
    public void EnterAlertState()
    {
        float currentDistanceToPlayer = Vector3.Distance(LevelManager.instance.GetPlayerPosition(), transform.position);
        if (currentState != alertState && currentDistanceToPlayer <= playerDistanceForAlert)
        {
            LoadState(alertState);
        }
    }

    public void EnterPatrolState()
    {
        LoadState(patrolState);
    }

    public void EnterSeesPlayerState()
    {
        LoadState(seesPlayerState);
    }

    public void EnterAttackState()
    {
        LoadState(attackState);
    }

    private void OnEnable()
    {
        LevelManager.AlertTriggered += EnterAlertState;
        LevelManager.PatrolTriggered += EnterPatrolState;
    }

    private void OnDisable()
    {
        LevelManager.AlertTriggered -= EnterAlertState;
        LevelManager.PatrolTriggered -= EnterPatrolState;
    }

    private void SetAnimation()
    {
        animator.SetFloat("CurrentMoveSpeed", navAgent.velocity.magnitude);
    }
}
