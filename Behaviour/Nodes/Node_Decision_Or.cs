using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.AI;

namespace XNode.FSMG
{
    [CreateNodeMenu("Decisions/Or")]
    public class Node_Decision_Or : NodeBase_Decision
    {
        public class DynamicDecision
        {
            public DynamicDecision(NodePort port)
            {
                this.port = port;
            }

            public NodePort port = null;
            public bool GetDecisionResult(FSMBehaviour fsm)
            {
                bool result = false;
                List<NodePort> connectionPorts = port.GetConnections();
                List<NodeBase_Decision> list = new List<NodeBase_Decision>();

                foreach (NodePort p in connectionPorts)
                {
                    list.Add((NodeBase_Decision)p.node);
                }


                if (list.Count <= 0)
                { result = false; }
                else
                { result = !list.Exists(r => r.Execute(fsm) == false); }

                //Clear garbage
                connectionPorts.Clear();
                list.Clear();
                connectionPorts = null;
                list = null;

                return result;

            }
        }
        private List<DynamicDecision> dynamics;

        //All decisions need be true
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Decision inputDecisions;

        //Or all Decisions need be true
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Decision inputOrDecisions;

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
            List<NodeBase_Decision> decisions = GetAllInputDecisions();
            List<NodeBase_Decision> orDecisions = GetAllOrInputDecisions();

            bool resultA = !decisions.Exists(r => r.Execute(fsm) == false);
            bool resultB = !orDecisions.Exists(r => r.Execute(fsm) == false);
            bool resultC = dynamics.Count <= 0 ? false : !dynamics.Exists(r => r.GetDecisionResult(fsm) == false);


            return resultA || resultB || resultC;


        }

        [ContextMenu("Add Or Input")]
        void AddOrInpuPort()
        {

            NodePort port = AddDynamicInput(typeof(NodeBase_Decision), ConnectionType.Multiple, TypeConstraint.Strict, "inputDecisions_" + (dynamics.Count + 3).ToString());
            dynamics.Add(new DynamicDecision(port));

        }

        List<NodeBase_Decision> GetAllInputDecisions()
        {
            NodePort inputPort = GetInputPort("inputDecisions");
            List<NodePort> connectionPorts = inputPort.GetConnections();

            List<NodeBase_Decision> result = new List<NodeBase_Decision>();

            foreach (NodePort p in connectionPorts)
            {
                result.Add((NodeBase_Decision)p.node);
            }

            return result;

        }
        List<NodeBase_Decision> GetAllOrInputDecisions()
        {
            NodePort inputPort = GetInputPort("inputOrDecisions");
            List<NodePort> connectionPorts = inputPort.GetConnections();

            List<NodeBase_Decision> result = new List<NodeBase_Decision>();

            foreach (NodePort p in connectionPorts)
            {
                result.Add((NodeBase_Decision)p.node);
            }

            return result;
        }

    }
}