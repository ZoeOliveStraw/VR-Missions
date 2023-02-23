using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GS_Alert : State
{
    private GuardVision myGuardVision;
    private GuardAI myGuardAI;
    private NavMeshAgent navAgent;
    
    private Vector3 lastKnownPlayerPosition;
    private Vector3 currentTarget;
    private Vector3 actualPlayerPosition;
    private float currentDistanceFromPlayerToTarget;

    [SerializeField] private float minDistanceToTarget = 1f;
    [SerializeField] private float attackDistance;
    [SerializeField] private float updateNavTargetDistance = 0.2f;

    public override void OnStateEnter()
    {
        if(!myGuardAI) myGuardAI = GetComponent<GuardAI>();
        if(!myGuardVision) myGuardVision = GetComponent<GuardVision>();
        if(!navAgent) navAgent = GetComponent<NavMeshAgent>();

        navAgent.speed = myGuardAI.alertMoveSpeed;
        StartAlert();
    }
    
    private void StartAlert()
    {
        navAgent.SetDestination(LevelManager.instance.GetPlayerPosition());
        myGuardVision.SetVisionCone(90);
    }
    
    private void FixedUpdate()
    {
        ProcessGuardBehavior();
    }

    private void ProcessGuardBehavior()
    {
        Debug.Log("Fixed update called");
        actualPlayerPosition = LevelManager.instance.GetPlayerPosition();
        //Update last known player position in manager if guard can see player
        if (myGuardVision.canSeePlayer)
        {
            LevelManager.instance.SetLastKnownPlayerPosition();
            lastKnownPlayerPosition = actualPlayerPosition;
        }
        else
        {
            lastKnownPlayerPosition = LevelManager.instance.GetLastKnownPlayerPosition();
        }

        if (Vector3.Distance(transform.position, actualPlayerPosition) < minDistanceToTarget)
        {
            myGuardAI.EnterAttackState();
            return;
        }

        if (Vector3.Distance(currentTarget, lastKnownPlayerPosition) > updateNavTargetDistance)
        {
            navAgent.SetDestination(lastKnownPlayerPosition);
            currentTarget = lastKnownPlayerPosition;
        }
    }

    public override void OnStateExit()
    {
        //haha exiting the state cool
    }
}
