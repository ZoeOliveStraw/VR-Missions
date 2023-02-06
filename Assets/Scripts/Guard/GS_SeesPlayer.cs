using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GS_SeesPlayer : State
{
    private GuardVision guardVision;
    private GuardAI guardAI;
    private NavMeshAgent navAgent;
    private float alertMeter;
    private float alertMeterLength;

    [SerializeField] private float waitBeforeReturningToPatrol;

    public override void OnStateEnter()
    {
        alertMeter = 0;
        Initialize();
        navAgent.isStopped = true;
    }

    public override void OnStateExit()
    {
        navAgent.isStopped = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// Fixed update is called 60 times per second at a consistent rate
    /// </summary>
    private void FixedUpdate()
    {
        Debug.Log(alertMeter);
        if (!guardVision.canSeePlayer)
        {
            StartCoroutine(Wait());
        }
        else
        {
            TurnTowardsPlayer();
            alertMeter += Time.fixedDeltaTime;
            if (alertMeter >= alertMeterLength)
            {
                guardAI.EnterAlertState();
            }
        }
    }

    /// <summary>
    /// When sight line is broken with the player the guard will wait for the wait time and then return to patrol
    /// </summary>
    /// <returns></returns>
    private IEnumerator Wait()
    {
        float timeSinceSightBroken = 0;

        while (timeSinceSightBroken < waitBeforeReturningToPatrol)
        {
            if (guardVision.canSeePlayer)
            {
                yield break;
            }

            timeSinceSightBroken += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        guardAI.EnterPatrolState();
    }

    private void TurnTowardsPlayer()
    {
        
    }

    //Set component references if they haven't been set already
    private void Initialize()
    {
        if (!guardVision) guardVision = GetComponent<GuardVision>();
        if (!guardAI) guardAI = GetComponent<GuardAI>();
        if (!navAgent) navAgent = GetComponent<NavMeshAgent>();
        
        alertMeterLength = guardAI.timeSeenToAlert;
    }
}
