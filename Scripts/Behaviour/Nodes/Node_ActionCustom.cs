using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using FSMG.Components;

namespace FSMG
{
    [CreateNodeMenu("Actions/Custom")]
    public class Node_ActionCustom : NodeBase_Action
    {
      
        [Output]
        public NodeBase_Action outAction = null;

        [SerializeField, NodeAIAction(true)]
        private AI_ActionBase action = null;


        public override void Execute(FSMBehaviour fsm)
        {
            if (action != null)
                action.Execute(fsm);

        }       

    }
}