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

    private PatrolNode nextNode;
    private float minDistanceToTarget = 0.2f;

    public override void OnStateEnter(GameObject myOwner)
    {
        base.OnStateEnter(myOwner);

        myGuardAI = owner.GetComponent<GuardAI>();
        loopPatrol = myGuardAI.loopPatrol;
        navAgent = myGuardAI.navAgent;

        nextNode = myGuardAI.firstNode;
        navAgent.SetDestination(nextNode.GetPosition());
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(owner.transform.position, nextNode.GetPosition()) < minDistanceToTarget)
        {
            nextNode = nextNode.GetNextNode();
            myGuardAI.StartCoroutine(WaitBeforeNextNode());
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
