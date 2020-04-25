using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace FSMG
{

    [CreateNodeMenu("States/Jump")]
    public class Node_StateJump : NodeBase_State
    {

        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
        public NodeBase_State inState;
        public NodeBase_State goToState;

        public NodeBase_State connectedState
        {
            get
            {
                NodePort inport = GetInputPort("inState");
                NodeBase_State connected = null;
                if (inport.ConnectionCount > 0)
                {
                    connected = (NodeBase_State)inport.Connection.node;
                }

                return connected;
            }
        }

        public override void Execute(FSMBehaviour fsm)
        {
            //NullReference Bugfix
            if (goToState == null)  //Nao permite estados nullos
                return;

            //StackOverflow Bufix
            if (goToState == this)  //Não permite eu mesmo como estado
                return;


            //StackOverflow Bufix
            if (goToState == connectedState) //Não permite que o proximo estado seja o estado em que eu estou conectado. 
                return;

          
            //Jump to node
            goToState.SetCurrentState();

        }
    }
}