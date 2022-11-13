using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "AlertState", menuName = "Guard States/Alert", order = 1)]
public class GS_Alert : State
{
    private GuardVision myGuardVision;
    private GuardAI myGuardAI;
    private NavMeshAgent navAgent;
    
    private Vector3 lastKnownPlayerPosition;
    private Vector3 currentTarget;
    
    private float minDistanceToTarget = 0.2f;

    public override void OnStateEnter(GameObject myOwner)
    {
        Debug.Log("Alert state entered");
        
        base.OnStateEnter(myOwner);

        if(!myGuardAI) myGuardAI = owner.GetComponent<GuardAI>();
        if(!myGuardVision) myGuardVision = owner.GetComponent<GuardVision>();
        if(!navAgent) navAgent = owner.GetComponent<NavMeshAgent>();

        navAgent.speed = myGuardAI.alertMoveSpeed;
    }
    
    public override void UpdateState()
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
        if (Vector3.Distance(owner.transform.position, currentTarget) >= minDistanceToTarget)
        {
            navAgent.SetDestination(currentTarget);
        }
    }
    
    public override void OnStateExit()
    {
        
    }
}
