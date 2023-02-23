using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GS_SeesPlayer : State
{
    private GuardVision guardVision;
    private GuardAI guardAI;
    private NavMeshAgent navAgent;
    private Animator animator;
    private float alertMeter;
    private float alertMeterLength;

    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private float waitBeforeReturningToPatrol;

    public override void OnStateEnter()
    {
        alertMeter = 0;
        Initialize();
        animator.SetBool("Suspicious", true);
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
                LevelManager.instance.LevelWideAlert();
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
    
    /// <summary>
    /// 
    /// </summary>
    private void TurnTowardsPlayer()
    {
        Vector3 dir = guardVision.playerPosition - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    //Set component references if they haven't been set already
    private void Initialize()
    {
        if (!guardVision) guardVision = GetComponent<GuardVision>();
        if (!guardAI) guardAI = GetComponent<GuardAI>();
        if (!navAgent) navAgent = GetComponent<NavMeshAgent>();
        if (!animator) animator = guardAI.animator;
        
        alertMeterLength = guardAI.timeSeenToAlert;
    }
}
