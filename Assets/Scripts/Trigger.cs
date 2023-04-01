using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Trigger : MonoBehaviour
{
    private bool hasBeenTriggered;
    [SerializeField] private string tagToCheck;
    [SerializeField] private bool destroyPickup;
    [SerializeField] private bool spinPickups;
    [SerializeField] private float spinSpeed;
    [SerializeField] private List<GameObject> pickup;
    [SerializeField] private UnityEvent eventsOnTrigger;
    [SerializeField] private List<GameObject> objectToDestroy;

    private void Update()
    {
        if (spinPickups)
        {
            foreach (var obj in pickup)
            {
                obj.transform.RotateAround(transform.position, transform.up, Time.deltaTime * spinSpeed);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToCheck))
        {
            if (!hasBeenTriggered)
            {
                hasBeenTriggered = true;
                eventsOnTrigger.Invoke();
                foreach(var obj in objectToDestroy) Destroy(obj);
                if (destroyPickup)
                {
                    foreach(var obj in pickup) Destroy(obj);
                }
            }
        }
    }
}
