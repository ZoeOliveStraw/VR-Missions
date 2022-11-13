using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : StateController
{
    [Header("States")]
    [SerializeField] private State patrolState;
    [SerializeField] private State alertState;
    
    [Header("Patrol Information")]
    [SerializeField] public PatrolNode firstNode;
    [SerializeField] public bool loopPatrol; //Will the guard path to the first node upon reaching the last one or simply go back down the chain
    
    [Header("Alert Phase Information")]
    [SerializeField] public float timeSeenToAlert;
    [SerializeField] public float alertFadeTime = 10;

    [Header("Guard stats")] 
    [SerializeField] public float patrolMoveSpeed;
    [SerializeField] public float alertMoveSpeed;
    [SerializeField] public float investigationMoveSpeed;

    public NavMeshAgent navAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        LoadState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
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
}
