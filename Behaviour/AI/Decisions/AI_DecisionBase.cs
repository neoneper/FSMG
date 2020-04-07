using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG.Components;

namespace XNode.FSMG
{
    public abstract class AI_DecisionBase : ScriptableObject
    {
        [SerializeField, GraphState(callback: "OnGraphChangeInEditor")]
        private Graph_State graph = null;

        public Graph_State Graph { get { return graph; } }

        public abstract bool Execute(FSMBehaviour fsm);

        protected virtual void OnGraphChangeInEditor(Graph_State old, Graph_State newg)
        {

        }
    }
}