using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace FSMG
{
    [CreateNodeMenu("Decisions/Invert"), NodeWidth(180)]
    public class Node_InvertDecision : NodeBase_Decision
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [Input(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
        public NodeBase_Decision inDecision;
        [Output(typeConstraint = TypeConstraint.Strict)]
        public NodeBase_Decision outDecision;

        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "Invert";

            isAlreadyRename = true;

            base.Init();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            NodePort inPort = GetInputPort("inDecision");
            if (inPort.Connection == null)
                return this;

            return inPort.Connection.node;
        }

        public override bool Execute(FSMBehaviour fsm)
        {
            NodePort inPort = GetInputPort("inDecision");
            if (inPort.Connection == null)
                return false;

            NodeBase_Decision nodeDecision = (NodeBase_Decision)inPort.Connection.node;
            return !nodeDecision.Execute(fsm);

        }

        public override string GetNoodleLabel(NodePort port)
        {
            NodePort inPort = GetInputPort("inDecision");
            string label = FSMGUtility.StringTag_Undefined;
            if (inPort.Connection != null)
            {
                NodeBase_Decision nodeDecision = (NodeBase_Decision)inPort.Connection.node;

                label = "(Not) - " + nodeDecision.GetNoodleLabel(inPort);
            }

            return label;
        }
        public override INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            return INodeNoodleLabelActiveType.Selected;
        }


    }
}