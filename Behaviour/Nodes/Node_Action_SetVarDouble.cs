using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XNode;
using XNode.FSMG.Components;

namespace XNode.FSMG
{
    [CreateNodeMenu("Variables/Set/Double")]
    public class Node_Action_SetVarDouble : NodeBase_Action
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [SerializeField, NodeEnumCallback("OnLocalTypeChanged")]
        private GraphVarLocalType localType = GraphVarLocalType.Local;

        [SerializeField, GraphVar(true, "GeVariableNames")]
        private string varName = "";
        [Input]
        public double inputValue = 0;

        [Output]
        public NodeBase_Action outAction = null;


        public List<string> GeVariableNames()
        {
            Graph_State graph_state = (Graph_State)graph;
            return graph_state.GetVariablesName(GraphVarType.Double, localType);
        }

        public override void Execute(FSMBehaviour fsm)
        {
            if (Application.isEditor && Application.isPlaying == false)
                return;

            Graph_State graph_state = (Graph_State)graph;
            DoubleVar doubleVar = null;
            if (graph_state.TryGetDoubeVar(varName, out doubleVar, localType))
            {
                doubleVar.value = GetInputValue<double>("inputValue", this.inputValue);
            }
            
        }
        protected override void Init()
        {
            //Renomeia o nó na primeira vez que o nó é iniciado, isto previne o nome feio original do .cs
            if (!isAlreadyRename)
                this.name = "SetDouble";

            isAlreadyRename = true;

            base.Init();
        }


        protected void OnLocalTypeChanged()
        {
            varName = "";
        }


    }
}