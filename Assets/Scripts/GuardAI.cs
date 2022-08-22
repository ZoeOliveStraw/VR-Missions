using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : StateController
{
    [SerializeField] private State patrolState;
    [SerializeField] public PatrolNode firstNode;
    [SerializeField] public bool loopPatrol; //Will the guard path to the first node upon reaching the last one or simply go back down the chain
    
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
}
