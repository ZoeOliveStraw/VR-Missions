using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GS_Alert : State
{
    private GuardVision myGuardVision;
    private GuardAI myGuardAI;
    private NavMeshAgent navAgent;
    private AudioSource audioSource;
    
    private Vector3 lastKnownPlayerPosition;
    private Vector3 currentTarget;
    private Vector3 actualPlayerPosition;
    private float currentDistanceFromPlayerToTarget;

    [SerializeField] private float minDistanceToTarget = 1f;
    [SerializeField] private float attackDistance;
    [SerializeField] private float updateNavTargetDistance = 0.2f;
    [SerializeField] private List<AudioClip> barks;
    [SerializeField] private TextMeshProUGUI exclamation;

    public override void OnStateEnter()
    {
        if(!myGuardAI) myGuardAI = GetComponent<GuardAI>();
        if(!myGuardVision) myGuardVision = GetComponent<GuardVision>();
        if(!navAgent) navAgent = GetComponent<NavMeshAgent>();
        if (!audioSource) audioSource = GetComponent<AudioSource>();

        navAgent.speed = myGuardAI.alertMoveSpeed;
        StartAlert();
        PlayRandomBark();
        exclamation.gameObject.SetActive(true);
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
    
    private void PlayRandomBark()
    {
        audioSource.PlayOneShot(barks[Random.Range(0,barks.Count)]);
    }

    public override void OnStateExit()
    {
        //haha exiting the state cool
        exclamation.gameObject.SetActive(false);
    }
}
