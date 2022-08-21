using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateController : MonoBehaviour
{
    protected State currentState;

    protected void LoadState(State state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }
}
