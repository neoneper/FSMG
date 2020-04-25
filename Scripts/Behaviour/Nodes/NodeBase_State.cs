using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace FSMG
{

    public abstract class NodeBase_State : Node, INodeNoodleLabel
    {
        /// <summary>
        /// Gráfico de estados ao qual este nó percente. Cast de <seealso cref="NodeGraph"/>
        /// </summary>
        public Graph_State Graph { get { return (Graph_State)graph; } }
        /// <summary>
        /// Informa se este estado é o estado atual do grafico <seealso cref="Graph_State.CurrentState"/>
        /// </summary>
        public bool IsCurrentState { get { return Graph.CurrentState == this; } }
        /// <summary>
        /// Informa se este estado é o estado Inicial (Default) do grafico. <seealso cref="Graph_State.RootState"/>
        /// </summary>
        public bool IsRootState { get { return Graph.RootState == this; } }

        /// <summary>
        /// Executa as ações <seealso cref="AI_ActionBase"/> e decisões <seealso cref="AI_DecisionBase"/> deste estado,
        /// e muda automaticamente para um proximo estado válido caso haja alguma decisão verdadeira e uma transição valida.
        /// </summary>
        /// <param name="fsm"></param>
        public abstract void Execute(FSMBehaviour fsm);

        /// <summary>
        /// Atualiza o estado atual do grafico: <seealso cref="Graph_State.CurrentState"/>, para este estado.
        /// Cao seja efetivada a mudança o evento <seealso cref=" OnCurrentState"/> sera chamado.
        /// </summary>
        public virtual void SetCurrentState()
        {
            if (((Graph_State)graph).SetCurrentState(this))
            {
                OnCurrentState();
            }

        }

        /// <summary>
        /// Disparado quando <seealso cref="SetCurrentState"/> é executado para trocar o atual nó de atualização do grafico.
        /// Tenha em mente que o nó devera ter sido modificado efetivamente no grafico para este evento ser chamado.
        /// caso contrario ele nunca seja chamado.
        /// </summary>
        protected virtual void OnCurrentState() { }

        public override object GetValue(NodePort port)
        {
            return this;
        }

        //INodeNoodles: Label das conexões
        public virtual string GetNoodleLabel(NodePort port)
        {
            return this.name;
        }
        public virtual INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            return INodeNoodleLabelActiveType.Selected;
        }

        //Funções somente para testes em modo editor. com eleas e possivel setar manualmente um nó atual e default
        [ContextMenu("SetCurrent")]
        public void SetGraphCurrent()
        {
            Graph.SetCurrentState(this);
        }
    }
}