using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XNode;
using XNode.FSMG.Components;

namespace XNode.FSMG
{
    [CreateNodeMenu("Variables/Set/Int")]
    public class Node_Action_SetVarInt : NodeBase_Action
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [SerializeField, NodeEnumCallback("OnLocalTypeChanged")]
        private GraphVarLocalType localType = GraphVarLocalType.Local;

        [SerializeField, GraphVar(true, "GeVariableNames")]
        private string varName = "";
        [Input]
        public int inputValue = 0;

        [Output]
        public NodeBase_Action outAction = null;


        public List<string> GeVariableNames()
        {
            Graph_State graph_state = (Graph_State)graph;
            return graph_state.GetVariablesName(GraphVarType.Integer, localType);
        }

        public override void Execute(FSMBehaviour fsm)
        {
            if (Application.isEditor && Application.isPlaying == false)
                return;

            Graph_State graph_state = (Graph_State)graph;
            IntVar intvar = null;
            if (graph_state.TryGetIntVar(varName, out intvar, localType))
            {
                intvar.value = GetInputValue<int>("inputValue", this.inputValue);
            }
            
        }
        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "SetInt";

            isAlreadyRename = true;

            base.Init();
        }


        protected void OnLocalTypeChanged()
        {
            varName = "";
        }


    }
}