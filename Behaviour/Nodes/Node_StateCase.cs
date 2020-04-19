using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace XNode.FSMG
{

    [CreateNodeMenu("States/Case")]
    public class Node_StateCase : NodeBase_State
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

        private NodePort InStatePort { get { return GetInputPort("inStates"); } }
        private NodePort InActionsPort { get { return GetInputPort("inActions"); } }     

        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_State inStates;
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Action inActions;

        protected override void Init()
        {
            if (dynamics == null)
                dynamics = new List<DynamicDecision>();
            base.Init();
        }
        public override void Execute(FSMBehaviour fsm)
        {
            //Executa ações
            ExecuteActions(fsm);

            //Executa decisões de caso e vai para o proximo estado
            ExecuteDecision(fsm);

        }

        private void ExecuteActions(FSMBehaviour fsm)
        {
            List<NodeBase_Action> actions = GetActions();
            foreach (NodeBase_Action action in actions)
            {
                action.Execute(fsm);
            }

            actions.Clear();
            actions = null;
        }
        private void ExecuteDecision(FSMBehaviour fsm)
        {
            foreach (DynamicDecision d in dynamics)
                d.ExecuteDecisions(fsm);

        }
        private List<NodeBase_Action> GetActions()
        {
            List<NodeBase_Action> actions = new List<NodeBase_Action>();
            List<NodePort> inputActionsPort = InActionsPort.GetConnections();

            foreach (NodePort port in inputActionsPort)
            {
                if (port.node != null)
                {
                    actions.Add((NodeBase_Action)port.node);
                }
            }

            return actions;
        }

        [ContextMenu("Add Case")]
        void AddOrInpuPort()
        {

            NodePort inPort = AddDynamicInput(typeof(NodeBase_Decision), ConnectionType.Multiple, TypeConstraint.Strict, "inDecisions_" + (dynamics.Count).ToString());
            NodePort outPort = AddDynamicOutput(typeof(NodeBase_State), ConnectionType.Override, TypeConstraint.Strict, "outState_" + (dynamics.Count).ToString());
            dynamics.Add(new DynamicDecision(inPort, outPort));

        }

    }
}