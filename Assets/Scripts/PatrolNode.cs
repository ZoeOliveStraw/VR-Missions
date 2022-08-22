using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : MonoBehaviour
{
    public GuardAI myGuard;
    public GameObject patrolNodePrefab;
    public GameObject parentObject;
    public PatrolNode nextNode;
    public PatrolNode firstNode;
    public PatrolNode previousNode;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere( transform.position,0.1f);
        
        if (nextNode)
        {
            Gizmos.DrawLine(transform.position, nextNode.GetPosition());
        }
        else if (firstNode)
        {
            Gizmos.DrawLine(transform.position, firstNode.GetPosition());
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public PatrolNode GetNextNode()
    {
        if (nextNode)
        {
            return nextNode;
        }
        else
        {
            return firstNode; 
        }
    }

    private void Start()
    {
        if (previousNode)
        {
            previousNode.nextNode = this;
        }
    }
}
