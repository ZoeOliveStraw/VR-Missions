using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSounds : MonoBehaviour
{
    [SerializeField] private float footstepVolume = 0.75f;
    [SerializeField] private float voiceVolume = 1f;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> footSteps;
    [SerializeField] private AudioClip suspicionNoise;
    [SerializeField] private AudioClip alertNoise;
    // Start is called before the first frame update

    private void PlayRandomFootstep()
    {
        if(footSteps.Count > 0) source.PlayOneShot(footSteps[Random.Range(0,footSteps.Count)], footstepVolume);
    }

    private void PlayAlertNoise()
    {
        source.PlayOneShot(alertNoise, voiceVolume);
    }

    private void PlaySuspicionNoise()
    {
        source.PlayOneShot(suspicionNoise, voiceVolume);
    }

}
