using UnityEngine;
using UnityEngine.AI;

public class GuardAI : StateController
{
    [Header("States")]
    private State patrolState;
    private State alertState;
    private State seesPlayerState;
    
    [Header("Patrol Information")]
    [SerializeField] public bool loopPatrol; //Will the guard path to the first node upon reaching the last one or simply go back down the chain

    [Header("Alert Phase Information")]
    [SerializeField] public float timeSeenToAlert;
    [SerializeField] public float alertFadeTime = 10;

    [Header("Guard stats")]
    [SerializeField] public float patrolMoveSpeed;
    [SerializeField] public float alertMoveSpeed;
    [SerializeField] public float investigationMoveSpeed;

    [Header("Animation stuff")] 
    [SerializeField] private Animator animator;
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
        
        alertState.enabled = false;
        LoadState(patrolState);
    }
    
    public void EnterAlertState()
    {
        if (currentState != alertState)
        {
            Debug.Log("Triggering alert");
            LoadState(alertState);
        }
    }

    public void EnterPatrolState()
    {
        Debug.Log("Triggering patrol");
        LoadState(patrolState);
    }

    public void EnterSeesPlayerState()
    {
        LoadState(seesPlayerState);
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
