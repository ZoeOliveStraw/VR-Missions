using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Guard States/Patrol", order = 1)]
public class GS_Patrol : State
{
    private bool loopPatrol;
    private NavMeshAgent navAgent;
    private GuardAI myGuardAI;
    private GuardVision myGuardVision;

    private PatrolNode nextNode;
    private float minDistanceToTarget = 0.2f;

    private float currentAlertness = 0; //This variable will build as the guard can see the player
    private float alertnessMeter; //Amount of time the guard needs to see the player to become alert

    public override void OnStateEnter(GameObject myOwner)
    {
        Debug.Log("Patrol state entered!");
        
        currentAlertness = 0;
        
        base.OnStateEnter(myOwner);

        if(!myGuardAI) myGuardAI = owner.GetComponent<GuardAI>();
        if(!myGuardVision) myGuardVision = owner.GetComponent<GuardVision>();
        if(!navAgent) navAgent = owner.GetComponent<NavMeshAgent>();
        alertnessMeter = myGuardAI.timeSeenToAlert;
        loopPatrol = myGuardAI.loopPatrol;
            
        navAgent.speed = myGuardAI.patrolMoveSpeed;

        nextNode = myGuardAI.firstNode;
        navAgent.SetDestination(nextNode.GetPosition());
    }

    public override void UpdateState()
    {
        SetNode();
        CheckForPlayer();
    }

    private void SetNode()
    {
        
        
        if (Vector3.Distance(owner.transform.position, nextNode.GetPosition()) < minDistanceToTarget)
        {
            nextNode = nextNode.GetNextNode();
            myGuardAI.StartCoroutine(WaitBeforeNextNode());
        }
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
        yield return new WaitForSeconds(nextNode.waitAtNode);
        navAgent.SetDestination(nextNode.GetPosition());
    }
    
    public override void OnStateExit()
    {
        
    }
}
