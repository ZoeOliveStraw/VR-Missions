using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GS_Patrol : State
{
    //private bool loopPatrol; //may use this in a future iteration where patrols may be looped
    private NavMeshAgent navAgent;
    private GuardAI myGuardAI;
    private GuardVision myGuardVision;

    private PatrolNode firstNode;
    private float minDistanceToTarget = 0.2f;
    private List<Vector3> patrolPositions = new List<Vector3>();
    private List<float> patrolWaits = new List<float>();
    private int currentNodeIndex = 0;

    private float currentAlertness = 0; //This variable will build as the guard can see the player
    private float alertnessMeter; //Amount of time the guard needs to see the player to become alert

    [SerializeField] private Transform nodeParentObject;



    /// <summary>
    /// Called by StateController objects when this state is loaded, used for state initialization
    /// </summary>
    public override void OnStateEnter()
    {
        Debug.Log("Patrol state entered!");
        
        currentAlertness = 0;

        if(!myGuardAI) myGuardAI = GetComponent<GuardAI>();
        if(!myGuardVision) myGuardVision = GetComponent<GuardVision>();
        if(!navAgent) navAgent = GetComponent<NavMeshAgent>();
        alertnessMeter = myGuardAI.timeSeenToAlert;
        //loopPatrol = myGuardAI.loopPatrol;
        navAgent.speed = myGuardAI.patrolMoveSpeed;
        
        BuildPatrolRoute();
        currentNodeIndex = 0;
        navAgent.SetDestination(patrolPositions[currentNodeIndex]);
    }

    private void Update()
    {
        SetNode();
        CheckForPlayer();
    }
    
    /// <summary>
    /// Check if guard has reached its current destination and sets the next node if so
    /// </summary>
    private void SetNode()
    {
        if (patrolPositions.Count != 0)
        {
            if (Vector3.Distance(transform.position, patrolPositions[currentNodeIndex]) < minDistanceToTarget)
            {
                GetNextPatrolPosition();
                myGuardAI.StartCoroutine(WaitBeforeNextNode());
            }
        }
    }
    
    /// <summary>
    /// Caches the positions of all of the patrol nodes into the list patrolPositions
    /// </summary>
    private void BuildPatrolRoute()
    {
        if (patrolPositions.Count == 0)
        {
            foreach (Transform n in nodeParentObject)
            {
                patrolPositions.Add(n.position);
                patrolWaits.Add(n.GetComponent<PatrolNode>().waitAtNode);
            }
        }
    }

    private void GetNextPatrolPosition()
    {
        if (currentNodeIndex < patrolPositions.Count - 1) currentNodeIndex++;
        else currentNodeIndex = 0;
    }
    
    private void CheckForPlayer()
    {
        if (myGuardVision.canSeePlayer)
        {
            currentAlertness += Time.deltaTime * myGuardVision.GetAmountPlayerIsVisible();
            if (currentAlertness > alertnessMeter) currentAlertness = alertnessMeter;
        }
        else
        {
            currentAlertness -= Time.deltaTime;
            if (currentAlertness < 0) currentAlertness = 0;
        }
        
        if (currentAlertness >= alertnessMeter)
        {
            LevelManager.instance.LevelWideAlert();
        }
    }

    public IEnumerator WaitBeforeNextNode()
    {
        yield return new WaitForSeconds(patrolWaits[currentNodeIndex]);
        SetNavDestination();
    }

    private void SetNavDestination()
    {
        navAgent.SetDestination(patrolPositions[currentNodeIndex]);
    }

    public override void OnStateExit()
    {
        
    }
}
