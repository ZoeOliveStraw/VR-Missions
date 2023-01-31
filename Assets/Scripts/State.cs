using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State : MonoBehaviour
{
    [HideInInspector] public GameObject owner;
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
}
