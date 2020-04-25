using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace FSMG
{

    [CreateNodeMenu("States/Start"), NodeTint("#1DBF00")]
    public class Node_StateRoot : NodeBase_State
    {

        [Output(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
        public NodeBase_State outState;

        protected override void Init()
        {
            Graph.SetRootState(this);
            Graph.SetCurrentState(this);
            base.Init();
        }

        public override void Execute(FSMBehaviour fsm)
        {

            NodePort outPort = GetOutputPort("outState");
            if(outPort.ConnectionCount > 0)
            {
                NodeBase_State next = (NodeBase_State)outPort.Connection.node;
                next.SetCurrentState();
            }

        }
    }
}