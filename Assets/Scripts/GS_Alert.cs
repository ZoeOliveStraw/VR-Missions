using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "AlertState", menuName = "Guard States/Alert", order = 1)]
public class GS_Alert : State
{
    private GuardVision myGuardVision;
    private GuardAI myGuardAI;
    private NavMeshAgent navMesh;

    private float timeSincePlayerLastSeen;
    private float timeUntilAlertFadesIfCantSeePlayer = 5;
    private Vector3 lastKnownPlayerPosition;
    private Vector3 currentTarget;
    
    private float minDistanceToTarget = 0.2f;

    public override void OnStateEnter(GameObject myOwner)
    {
        base.OnStateEnter(myOwner);
        
        myGuardAI = owner.GetComponent<GuardAI>();
        myGuardVision = owner.GetComponent<GuardVision>();
        navMesh = owner.GetComponent<NavMeshAgent>();

        timeUntilAlertFadesIfCantSeePlayer = myGuardAI.alertFadeTime;
    }
    
    public override void UpdateState()
    {
        if (myGuardVision.canSeePlayer)
        {
            lastKnownPlayerPosition = LevelManager.instance.GetPlayerPosition();
            timeSincePlayerLastSeen = 0;
        }
        else
        {
            timeSincePlayerLastSeen += Time.deltaTime;
        }

        if (timeSincePlayerLastSeen >= timeUntilAlertFadesIfCantSeePlayer)
        {
            myGuardAI.EnterPatrolState();
            return;
        }
        
        SetNavigationTarget();
    }

    private void SetNavigationTarget()
    {
        currentTarget = lastKnownPlayerPosition;
        if (Vector3.Distance(owner.transform.position, currentTarget) <= minDistanceToTarget)
        {
            
        }
    }
    
    public override void OnStateExit()
    {
        
    }
}
