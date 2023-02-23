using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GS_Patrol : State
{
    //private bool loopPatrol; //may use this in a future iteration where patrols may be looped
    private NavMeshAgent navAgent;
    private GuardAI guardAI;
    private GuardVision myGuardVision;
    private Animator animator;

    private PatrolNode firstNode;
    private float minDistanceToTarget = 0.5f;
    private List<Vector3> patrolPositions = new List<Vector3>();
    private List<float> patrolWaits = new List<float>();
    private int currentNodeIndex = 0;

    [SerializeField] private Transform nodeParentObject;



    /// <summary>
    /// Called by StateController objects when this state is loaded, used for state initialization
    /// </summary>
    public override void OnStateEnter()
    {
        Initialize();
        BuildPatrolRoute();
        animator.SetBool("Suspicious", false);
        navAgent.SetDestination(patrolPositions[currentNodeIndex]);
    }
    
    /// <summary>
    /// Get references to components as necessary
    /// </summary>
    private void Initialize()
    {
        if(!guardAI) guardAI = GetComponent<GuardAI>();
        if(!myGuardVision) myGuardVision = GetComponent<GuardVision>();
        if(!navAgent) navAgent = GetComponent<NavMeshAgent>();
        if (!animator) animator = guardAI.animator;
        navAgent.speed = guardAI.patrolMoveSpeed;
        myGuardVision.SetVisionCone(45);
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
                guardAI.StartCoroutine(WaitBeforeNextNode());
            }
        }
    }
    
    /// <summary>
    /// Caches the positions of all of the patrol nodes into the list patrolPositions
    /// Only happens if there is not a node list yet
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

            currentNodeIndex = 0;
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
            guardAI.EnterSeesPlayerState();
        }
    }

    public IEnumerator WaitBeforeNextNode()
    {
        yield return new WaitForSeconds(patrolWaits[currentNodeIndex]);
        navAgent.SetDestination(patrolPositions[currentNodeIndex]);
    }

    public override void OnStateExit()
    {
        StopAllCoroutines();
    }
}
