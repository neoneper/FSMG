using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG;

[CreateAssetMenu(menuName = "FSMG/AI/Actions/Test")]
public class AI_ActionTest : AI_ActionBase
{
    [SerializeField]
    private string response = "ActionTest";

    public override void Execute(FSMBehaviour fsm)
    {
        Debug.Log(response + " by:" + this.name);
    }

   
}
