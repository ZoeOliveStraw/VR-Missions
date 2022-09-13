using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlertState", menuName = "Guard States/Alert", order = 1)]
public class GS_Alert : State
{
    private GuardVision myGuardVision;
    private GuardAI myGuardAI;
    
    public override void OnStateEnter(GameObject myOwner)
    {
        base.OnStateEnter(myOwner);
        
        myGuardAI = owner.GetComponent<GuardAI>();
        myGuardVision = owner.GetComponent<GuardVision>();
        
        
    }
    public override void UpdateState()
    {
        
    }
    
    public override void OnStateExit()
    {
        
    }
}
