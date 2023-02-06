using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GS_Alert : State
{
    private GuardVision myGuardVision;
    private GuardAI myGuardAI;
    private NavMeshAgent navAgent;
    
    private Vector3 lastKnownPlayerPosition;
    private Vector3 currentTarget;
    
    private float minDistanceToTarget = 0.2f;

    public override void OnStateEnter()
    {
        Debug.Log("Alert state entered");

        if(!myGuardAI) myGuardAI = GetComponent<GuardAI>();
        if(!myGuardVision) myGuardVision = GetComponent<GuardVision>();
        if(!navAgent) navAgent = GetComponent<NavMeshAgent>();

        navAgent.speed = myGuardAI.alertMoveSpeed;
    }
    
    private void Update()
    {
        if (myGuardVision.canSeePlayer)
        {
            LevelManager.instance.SetLastKnownPlayerPosition();
            lastKnownPlayerPosition = LevelManager.instance.GetPlayerPosition();
        }
        
        currentTarget = lastKnownPlayerPosition;
        SetNavigationTarget();
    }

    private void SetNavigationTarget()
    {
        currentTarget = lastKnownPlayerPosition;
        if (Vector3.Distance(transform.position, currentTarget) >= minDistanceToTarget)
        {
            navAgent.SetDestination(currentTarget);
        }
    }
    
    public override void OnStateExit()
    {
        
    }
}
