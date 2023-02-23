using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private List<string> tagsToCheck;
    [SerializeField] private UnityEvent onHit;

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in tagsToCheck)
        {
            if (other.transform.CompareTag(tag))
            {
                Debug.Log("Hit!");
                onHit.Invoke();
                break;
            }
        }
    }
}
