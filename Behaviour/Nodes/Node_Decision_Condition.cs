using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.AI;
namespace FSMG
{
    [CreateNodeMenu("Decisions/Condition")]
    public class Node_Decision_Condition : NodeBase_Decision
    {


        [Input(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)]
        public bool inputCondition = false;

        [Output(typeConstraint = TypeConstraint.Strict)]
        public NodeBase_Decision outDecision;

     
        public override bool Execute(FSMBehaviour fsm)
        {
            if (Application.isEditor && Application.isPlaying == false)
                return false;

            return CheckDecision(fsm);
        }     

        private bool CheckDecision(FSMBehaviour fsm)
        {

            bool result = GetInputValue<bool>("inputCondition", this.inputCondition);
            return result;
        }

    }
}