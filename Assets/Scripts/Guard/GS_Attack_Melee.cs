using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class GS_Attack_Melee : State
{
    private GuardAI myGuardAI;
    private Animator animator;
    private NavMeshAgent navAgent;
    private GuardVision guardVision;

    [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float rotationSpeed = 100;

    public override void OnStateEnter()
    {
        owner = this.gameObject;
        if(!myGuardAI) myGuardAI = owner.GetComponent<GuardAI>();
        if(!animator) animator = myGuardAI.animator;
        if(!navAgent) navAgent = GetComponent<NavMeshAgent>();
        if (!guardVision) guardVision = GetComponent<GuardVision>();

        navAgent.isStopped = true;
        StartCoroutine(Attack());
    }

    private void Update()
    {
        TurnTowardsPlayer();
    }

    private IEnumerator Attack()
    {
        float halfAttackLength = attackAnimation.length / 2;
        animator.Play(attackAnimation.name);
        yield return new WaitForSeconds(halfAttackLength);
        CheckForPlayer();
        yield return new WaitForSeconds(halfAttackLength);
        myGuardAI.EnterAlertState();
    }

    private void CheckForPlayer()
    {
        var colliders = Physics.OverlapSphere(attackTransform.position,1f);

        foreach (Collider col in colliders)
        {
            if (col.transform.CompareTag("Player"))
            {
                Debug.Log("player hit!");
            }
        }
    }
    
    private void TurnTowardsPlayer()
    {
        Vector3 dir = guardVision.playerPosition - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    public override void OnStateExit()
    {
        navAgent.isStopped = false;
    }
}
