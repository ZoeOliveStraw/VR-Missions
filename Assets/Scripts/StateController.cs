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
        }
        currentState = state;
        currentState.OnStateEnter(this.gameObject);
    }
}
