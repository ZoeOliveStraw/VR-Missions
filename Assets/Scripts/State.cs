using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State : ScriptableObject
{
    [HideInInspector] public GameObject owner;
    public virtual void OnStateEnter(GameObject myOwner)
    {
        owner = myOwner;
    }
    public abstract void OnStateExit();
    public abstract void UpdateState();
}
