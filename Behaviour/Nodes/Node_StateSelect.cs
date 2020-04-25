using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace FSMG
{

    [CreateNodeMenu("States/Select")]
    public class Node_StateSelect : NodeBase_State
    {
        private NodePort InStatePort { get { return GetInputPort("inStates"); } }
        private NodePort InActionsPort { get { return GetInputPort("inActions"); } }
        private NodePort InDecisionsPort { get { return GetInputPort("inDecisions"); } }
        private NodePort OutStateTruePort { get { return GetOutputPort("outStateTrue"); } }
        private NodePort OutStateFalsePort { get { return GetOutputPort("outStateFalse"); } }

        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_State inStates;
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Action inActions;
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never)]
        public NodeBase_Decision inDecisions;

        [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        public NodeBase_State outStateTrue;
        [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        public NodeBase_State outStateFalse;

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

            if (decisionResult)
            {
                NodeBase_State nextTrueState = GetNextTrueState();
                if (nextTrueState)
                    nextTrueState.SetCurrentState();
            }
            else
            {                
                NodeBase_State nextFalseState = GetNextFalseState();
                if (nextFalseState)
                    nextFalseState.SetCurrentState();
            }

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
        private NodeBase_State GetNextTrueState()
        {
            NodeBase_State result = null;
            NodePort outStateTruePort = OutStateTruePort;

            if (outStateTruePort.ConnectionCount == 0)
                return result;

            result = (NodeBase_State)outStateTruePort.Connection.node;

            return result;

        }
        private NodeBase_State GetNextFalseState()
        {
            NodeBase_State result = null;
            NodePort outStateFalsePort = OutStateFalsePort;
            if (outStateFalsePort.ConnectionCount == 0)
                return result;

            result = (NodeBase_State)outStateFalsePort.Connection.node;

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