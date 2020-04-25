using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace FSMG
{

    [CreateNodeMenu("States/Transition")]
    public class Node_StateTransition : NodeBase_State
    {
        private NodePort InStatePort { get { return GetInputPort("inStates"); } }
        private NodePort InActionsPort { get { return GetInputPort("inActions"); } }
        private NodePort InDecisionsPort { get { return GetInputPort("inDecisions"); } }
        private NodePort OutStatePort { get { return GetOutputPort("outState"); } }


        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_State inStates;
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Action inActions;
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Decision inDecisions;
        [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        public NodeBase_State outState;

        public override void Execute(FSMBehaviour fsm)
        {
            //Executa ações
            ExecuteActions(fsm);
            //Executa decisões e muda de estado
            bool decisionResult = ExecuteDecisions(fsm);
            GoToNextState(decisionResult);
        }

        private void GoToNextState(bool decisionResult)
        {
            if (decisionResult == false)
                return;

            NodeBase_State nextState = GetNextState();
            if (nextState == null)
                return;

            nextState.SetCurrentState();

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
        private bool ExecuteDecisions(FSMBehaviour fsm)
        {
            List<NodeBase_Decision> decisions = GetDecisions();
            return !decisions.Exists(r => r.Execute(fsm) == false);
        }
        private NodeBase_State GetNextState()
        {
            NodeBase_State result = null;
            NodePort outStatePort = OutStatePort;
            if (outStatePort.ConnectionCount == 0)
                return result;

            result = (NodeBase_State)outStatePort.Connection.node;

            return result;

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
        private List<NodeBase_Decision> GetDecisions()
        {
            List<NodeBase_Decision> decisions = new List<NodeBase_Decision>();
            List<NodePort> inputDecisionsPort = InDecisionsPort.GetConnections();
            foreach (NodePort port in inputDecisionsPort)
            {
                if (port.node != null)
                {
                    decisions.Add((NodeBase_Decision)port.node);
                }
            }

            return decisions;
        }


    }
}