using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateController : MonoBehaviour
{
    protected State currentState;

    protected void LoadState(State state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
            currentState.enabled = false;
        }
        currentState = state;
        currentState.enabled = true;
        currentState.OnStateEnter();
    }
}
