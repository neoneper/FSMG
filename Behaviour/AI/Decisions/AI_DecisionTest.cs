using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG;
using XNode.FSMG.Components;

[CreateAssetMenu(menuName = "FSMG/AI/Decisions/Test")]
public class AI_DecisionTest : AI_DecisionBase
{
    [SerializeField]
    private bool response = false;

    public override bool Execute(FSMBehaviour fsm)
    {
        return response;
    }
}
