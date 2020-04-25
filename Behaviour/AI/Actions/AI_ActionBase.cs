using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XNode; namespace FSMG
{
    /// <summary>
    /// Base para criar todas as suas ações do tipo scriptableObject.
    /// Elas serão executado dentro do nó <see cref="Node_ActionCustom"/>.
    /// Este scriptableObject sera a ponte entre nosso FSM (<see cref="FSMBehaviour"/>)e o nosso grafico, <seealso cref="Graph_State"/>.
    /// </summary>
    public abstract class AI_ActionBase : ScriptableObject
    {
        [SerializeField, GraphState(callback: "OnGraphChangeInEditor")]
        private Graph_State graph = null;

        /// <summary>
        /// This is the state graph to which this action belongs. 
        /// If a graph is defined for this action, it can only be seen by the graph to which it belongs. 
        /// If no graph is defined, this action can be used by any state graph.
        /// </summary>
        public Graph_State Graph { get { return graph; } }

        public abstract void Execute(FSMBehaviour fsm);
        
        protected virtual void OnGraphChangeInEditor(Graph_State old, Graph_State newg)
        {

        }

    }
}