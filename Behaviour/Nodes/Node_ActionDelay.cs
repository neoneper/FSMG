using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNode.FSMG.Components;

namespace XNode.FSMG
{
    [CreateNodeMenu("Actions/Delay")]
    public class Node_ActionDelay : NodeBase_Action
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [Input(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)]
        public NodeBase_Action inputAction;

        [Input(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)]
        public float delay;

        [Output]
        public NodeBase_Action outAction = null;

        private float lastDelayTime = 0;

        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "Delay";

            isAlreadyRename = true;

            base.Init();
        }
        public override void Execute(FSMBehaviour fsm)
        {
            NodePort inputPort = GetInputPort("inputAction");
            if (inputPort.Connection == null)
                return;
            if (inputPort.Connection.node == null)
                return;

            
            float waitDelay = GetInputValue<float>("delay", this.delay);

            if (Time.time > lastDelayTime + waitDelay)
            {
                NodeBase_Action inputActNode = (NodeBase_Action)inputPort.Connection.node;
                inputActNode.Execute(fsm);

                lastDelayTime = Time.time;
            }

        }

       

    }
}