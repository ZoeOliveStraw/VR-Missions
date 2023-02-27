using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class GS_SeesPlayer : State
{
    private GuardVision guardVision;
    private GuardAI guardAI;
    private NavMeshAgent navAgent;
    private Animator animator;
    private float alertMeter;
    private float alertMeterLength;
    private AudioSource audioSource;

    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private float waitBeforeReturningToPatrol;
    [SerializeField] private Slider alertMeterSlider;
    [SerializeField] private Image bellImage;
    [SerializeField] private List<AudioClip> barks;

    public override void OnStateEnter()
    {
        alertMeter = 0;
        Initialize();
        animator.SetBool("Suspicious", true);
        navAgent.isStopped = true;
        PlayRandomBark();
    }
    
    /// <summary>
    /// Set component references if they haven't been set already
    /// </summary>
    private void Initialize()
    {
        if (!guardVision) guardVision = GetComponent<GuardVision>();
        if (!guardAI) guardAI = GetComponent<GuardAI>();
        if (!navAgent) navAgent = GetComponent<NavMeshAgent>();
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        if (!animator) animator = guardAI.animator;
        
        alertMeterLength = guardAI.timeSeenToAlert;
        
        //initialize the alert meter above the guard
        alertMeterSlider.gameObject.SetActive(true);
        bellImage.gameObject.SetActive(true);
        alertMeterSlider.minValue = 0;
        alertMeterSlider.maxValue = alertMeterLength;
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
            alertMeter += guardVision.GetAmountPlayerIsVisible();
            alertMeterSlider.value = alertMeter;
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
    /// Rotate the guard towards the player around the Y axis
    /// </summary>
    private void TurnTowardsPlayer()
    {
        Vector3 dir = guardVision.playerPosition - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }
    
    /// <summary>
    /// Play a random bark from the audio source
    /// </summary>
    private void PlayRandomBark()
    {
        audioSource.PlayOneShot(barks[Random.Range(0,barks.Count)]);
    }

    public override void OnStateExit()
    {
        navAgent.isStopped = false;
        StopAllCoroutines();
        alertMeterSlider.gameObject.SetActive(false);
        bellImage.gameObject.SetActive(false);
    }
}
