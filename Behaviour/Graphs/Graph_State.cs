using XNode.FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG.SerializableDictionary;
using System.Linq;

namespace XNode.FSMG
{
    [CreateAssetMenu(fileName = "New State Graph", menuName = "FSMG/Graphs/State Graph")]
    public class Graph_State : NodeGraph
    {

        public delegate void DelegateOnGraphStateChanged();
        public event DelegateOnGraphStateChanged OnStateChangedEvent;

        private float currentState_elapsedTime = 0;

        //Nó atual do grafico para ser atualizado pelo FSM
        private NodeBase_State _currentState;
        //Nó raiz, primeiro nó a ser atualizado pelo FSM quando iniciado
        private NodeBase_State _rootState;

        public float CurrentStateElapsedTime
        {
            get { return currentState_elapsedTime; }
        }

        /// <summary>
        /// Atual nó de estados que será atualizado pelo <seealso cref="FSMBehaviour"/>
        /// </summary>
        public NodeBase_State CurrentState { get { return _currentState; } }

        /// <summary>
        /// Nó raiz, representa o primeiro nó a ser atualizado pelo <seealso cref="FSMBehaviour"/> quando o mesmo for inciado
        /// </summary>
        public NodeBase_State RootState { get { return _rootState; } }

        /// <summary>
        /// Atualiza o <seealso cref="CurrentState"/> para executar ações e decisões. Modifica automaticamente o CurrentState 
        /// caso haja uma decisão verdadeira e um estado de transição válido
        /// </summary>
        /// <param name="fsm">Finite State Machine</param>
        public void UpdateGraph(FSMBehaviour fsm)
        {
            if (_currentState == null)
            {
                return;
            }

            currentState_elapsedTime += Time.deltaTime;
            _currentState.Execute(fsm);
        }

        public void InitGraph(FSMBehaviour fsm)
        {
            currentState_elapsedTime = 0;

            if (RootState == null)
            {
                XNode.Node node = nodes.FirstOrDefault(r => r is NodeBase_State);
                if (node != null)
                {
                    _rootState = (NodeBase_State)node;
                    _currentState = _rootState;
                }
            }

            if (RootState != null)
                _currentState = RootState;

        }

        /// <summary>
        /// Força o <seealso cref="CurrentState"/> para o estado do parametro.
        /// </summary>
        /// <param name="state">Novo estado para <see cref="CurrentState"/></param>
        /// <returns>Verdadeiro: Se o <seealso cref="CurrentState"/> foi modificado para um estado diferente do anterior</returns>
        public bool SetCurrentState(NodeBase_State state)
        {
            if (state == _currentState)
                return false;
            if (state == null)
                return false;

            currentState_elapsedTime = 0;
            _currentState = state;

            if (OnStateChangedEvent != null)
                OnStateChangedEvent.Invoke();

            return true;
        }
        public bool SetRootState(NodeBase_State state)
        {
            if (state == _rootState)
                return false;
            if (state == null)
                return false;

            _rootState = state;
            return true;
        }
        //=======================================================================================//
        //VARIAVEIS CUSTOMIZADAS
        //======================================================================================//
        [SerializeField, DrawKeyAsLabel(), DrawOptions(false, true, true)]
        private TagVarList variables = null;

        public void GetVariables(out TagVarList toList)
        {
            TagVarList tmp = new TagVarList();
            tmp.CopyFrom(variables);
            toList = tmp;
        }

        public GraphAddVarErrorsType AddTagVariable(string varName, GraphVarType varType)
        {
            GraphAddVarErrorsType result = GraphAddVarErrorsType.none;

            if (varName == FSMGUtility.StringTag_Undefined)
                return GraphAddVarErrorsType.invalidName;

            if (variables.ContainsKey(varName) == true)
            {
                result = GraphAddVarErrorsType.graph_already_exists;
            }
            else
            {
                variables.Add(varName, varType);
            }

            return result;
        }

        public Graph_State Instance
        {
            get
            {
                return (Graph_State)this.Copy();
            }
        }

    }

}