using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSMG.Components;

using XNode; namespace FSMG
{
    /// <summary>
    /// Base para criar todas as suas decisões do tipo scriptableObject.
    /// Elas serão executado dentro do nó <see cref="Node_DecisionCustom"/>.
    /// Este scriptableObject sera a ponte entre nosso FSM (<see cref="FSMBehaviour"/>)e o nosso grafico, <seealso cref="Graph_State"/>.
    /// </summary>
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