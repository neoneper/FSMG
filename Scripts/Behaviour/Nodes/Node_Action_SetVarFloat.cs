using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XNode;
using FSMG.Components;

namespace FSMG
{
    [CreateNodeMenu("Variables/Set/Float")]
    public class Node_Action_SetVarFloat : NodeBase_Action
    {
        //usado somente para renomear o nó assim que é criado, para evitar o nome feio vindo do arquivo.cs
        private bool isAlreadyRename = false;

        [SerializeField, GraphVar(true, GraphVarType.Float)]
        private string varName = "";
        [Input]
        public float inputValue = 0;

        [Output]
        public NodeBase_Action outAction = null;
        
        public override void Execute(FSMBehaviour fsm)
        {
            if (Application.isEditor && Application.isPlaying == false)
                return;
            
            Graph_State graph_state = (Graph_State)graph;
            FloatVar floatVar = null;
            if (graph_state.TryGetFloatVar(varName, out floatVar))
            {
                floatVar.value = GetInputValue<float>("inputValue", this.inputValue);
            }            
        }
    }
}