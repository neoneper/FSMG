using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.AI;

namespace XNode.FSMG
{
    [CreateNodeMenu("Decisions/Case")]
    public class Node_Decision_Case : NodeBase_Decision
    {
        public class DynamicDecision
        {
            public DynamicDecision(NodePort inputPort, NodePort outputPort)
            {
                this.intpuPort = inputPort;
                this.outputPort = outputPort;
            }

            public NodePort intpuPort = null;
            public NodePort outputPort = null;

            public void ExecuteDecisions(FSMBehaviour fsm)
            {
                if (outputPort.ConnectionCount <= 0)
                    return;

                bool result = GetDecisionResult(fsm);

                //Transition to other state
                if (result == false && outputPort.ConnectionCount <= 0)
                    return;

                NodeBase_State state = (NodeBase_State)outputPort.Connection.node;
                state.SetCurrentState();

            }


            private bool GetDecisionResult(FSMBehaviour fsm)
            {
                if (intpuPort.ConnectionCount <= 0)
                    return false;

                bool result = false;

                List<NodePort> connectionPorts = intpuPort.GetConnections();
                List<NodeBase_Decision> list = new List<NodeBase_Decision>();

                foreach (NodePort p in connectionPorts)
                {
                    list.Add((NodeBase_Decision)p.node);
                }

                result = !list.Exists(r => r.Execute(fsm) == false);

                //Clear garbage
                connectionPorts.Clear();
                list.Clear();
                connectionPorts = null;
                list = null;

                return result;

            }
        }
        private List<DynamicDecision> dynamics;


        [Output(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Decision output;


        protected override void Init()
        {
            if (dynamics == null)
                dynamics = new List<DynamicDecision>();
            base.Init();
        }

        public override bool Execute(FSMBehaviour fsm)
        {

            foreach (DynamicDecision d in dynamics)
                d.ExecuteDecisions(fsm);

            return false;

        }

        [ContextMenu("Add InputOutput")]
        void AddOrInpuPort()
        {

            NodePort inPort = AddDynamicInput(typeof(NodeBase_Decision), ConnectionType.Multiple, TypeConstraint.Strict, "inDecisions_" + (dynamics.Count + 3).ToString());
            NodePort outPort = AddDynamicOutput(typeof(NodeBase_State), ConnectionType.Override, TypeConstraint.Strict, "outState_" + (dynamics.Count + 3).ToString());
            dynamics.Add(new DynamicDecision(inPort, outPort));

        }


    }
}