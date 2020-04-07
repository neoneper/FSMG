using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode.FSMG
{
    public abstract class AI_ActionBase : ScriptableObject
    {
        [SerializeField, GraphState(callback: "OnGraphChangeInEditor")]
        private Graph_State graph = null;

        public Graph_State Graph { get { return graph; } }

        public abstract void Execute(FSMBehaviour fsm);


        protected virtual void OnGraphChangeInEditor(Graph_State old, Graph_State newg)
        {

        }

    }
}