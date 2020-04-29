﻿using FSMG.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSMG;
using System.Linq;
using Unity.Collections;
using System;

using XNode;
namespace FSMG
{
    [CreateAssetMenu(fileName = "New State Graph", menuName = "FSMG/Graphs/State Graph")]
    [RequireNode(type: typeof(Node_StateRoot))]
    public class Graph_State : NodeGraph
    {
        public delegate void DelegateOnGraphStateChanged();
        public event DelegateOnGraphStateChanged OnStateChangedEvent;

        private float currentState_elapsedTime = 0;
        private FSMBehaviour last_fsm_executed = null;

        //Nó atual do grafico para ser atualizado pelo FSM
        private NodeBase_State _currentState;
        //Nó raiz, primeiro nó a ser atualizado pelo FSM quando iniciado
        private NodeBase_State _rootState;
        public FSMBehaviour LastFSMExecute { get { return last_fsm_executed; } }
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

            last_fsm_executed = fsm;
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

        [SerializeField, SD_DrawKeyAsLabel(), SD_DrawOptions(false, true, true)]
        private TagVarList variables = null;
        [SerializeField]
        private TargetListGlobal targets = null;

        public TagVarList VariableTags
        {
            get
            {
                TagVarList tags = new TagVarList();
                tags.AddRange(variables);
                return tags;
            }
        }
        public List<string> VariableNames
        {
            get
            {
                return variables.Keys.ToList();
            }
        }
        public List<string> IntVariableNames
        {
            get
            {
                return variables.Where(r => r.Value == GraphVarType.Integer).Select(r => r.Key).ToList();
            }
        }
        public List<string> FloatVariableNames
        {
            get
            {
                return variables.Where(r => r.Value == GraphVarType.Float).Select(r => r.Key).ToList();
            }
        }
        public List<string> DoubleVariableNames
        {
            get
            {
                return variables.Where(r => r.Value == GraphVarType.Double).Select(r => r.Key).ToList();
            }
        }
        public List<string> BoolVariableNames
        {
            get
            {
                return variables.Where(r => r.Value == GraphVarType.Boolean).Select(r => r.Key).ToList();
            }
        }

        public bool TryGetIntVar(string varName, out IntVar intVar, GraphVarLocalType localType)
        {
            bool result = false;

            switch (localType)
            {
                case GraphVarLocalType.Local:
                    result = last_fsm_executed.Variables.TryGetIntVar(varName, out intVar);
                    break;
                case GraphVarLocalType.Global:
                    result = FSMGSettings.Instance.Variables.TryGetIntVar(varName, out intVar);
                    break;
                default:
                    intVar = null;
                    break;
            }

            return result;

        }
        public bool TryGetFloatVar(string varName, out FloatVar floatVar, GraphVarLocalType localType)
        {
            bool result = false;

            switch (localType)
            {
                case GraphVarLocalType.Local:
                    result = last_fsm_executed.Variables.TryGetFloatVar(varName, out floatVar);
                    break;
                case GraphVarLocalType.Global:
                    result = FSMGSettings.Instance.Variables.TryGetFloatVar(varName, out floatVar);
                    break;
                default:
                    floatVar = null;
                    break;
            }

            return result;
        }
        public bool TryGetDoubeVar(string varName, out DoubleVar doubleVar, GraphVarLocalType localType)
        {
            bool result = false;

            switch (localType)
            {
                case GraphVarLocalType.Local:
                    result = last_fsm_executed.Variables.TryGetDoubleVar(varName, out doubleVar);
                    break;
                case GraphVarLocalType.Global:
                    result = FSMGSettings.Instance.Variables.TryGetDoubleVar(varName, out doubleVar);
                    break;
                default:
                    doubleVar = null;
                    break;
            }

            return result;
        }
        public bool TryGetBoolVar(string varName, out BoolVar boolVar, GraphVarLocalType localType)
        {
            bool result = false;

            switch (localType)
            {
                case GraphVarLocalType.Local:
                    result = last_fsm_executed.Variables.TryGetBoolVar(varName, out boolVar);
                    break;
                case GraphVarLocalType.Global:
                    result = FSMGSettings.Instance.Variables.TryGetBoolVar(varName, out boolVar);
                    break;
                default:
                    boolVar = null;
                    break;
            }

            return result;
        }
        public bool TryGetTarget(string targetName, out List<FSMTargetBehaviour> fsmTargets, TargetLocalType localType)
        {
            return last_fsm_executed.TryGetFSMTarget(targetName, out fsmTargets, localType);
        }

        public List<string> GetGlobalVariablesName(GraphVarType vartype)
        {
            List<string> variablesName = null;
            switch (vartype)
            {
                case GraphVarType.Integer:
                    variablesName = FSMGSettings.Instance.Variables.Int_VariableNames.ToList();
                    break;
                case GraphVarType.Float:
                    variablesName = FSMGSettings.Instance.Variables.Float_VariableNames.ToList();
                    break;
                case GraphVarType.Double:
                    variablesName = FSMGSettings.Instance.Variables.Double_VariableNames.ToList();
                    break;
                case GraphVarType.Boolean:
                    variablesName = FSMGSettings.Instance.Variables.Bool_VariableNames.ToList();
                    break;
            }

            return variablesName;
        }
        public List<string> GetLocalVariablesName(GraphVarType vartype)
        {
            List<string> variablesName = null;
            switch (vartype)
            {
                case GraphVarType.Integer:
                    variablesName = IntVariableNames;
                    break;
                case GraphVarType.Float:
                    variablesName = FloatVariableNames;
                    break;
                case GraphVarType.Double:
                    variablesName = DoubleVariableNames;
                    break;
                case GraphVarType.Boolean:
                    variablesName = BoolVariableNames;
                    break;
            }

            return variablesName;
        }
        public List<string> GetGlobalTargetsName()
        {
            return FSMGSettings.Instance.TargetNames.ToList();
        }
        public List<string> GetLocalTargetsName()
        {
            return targets.Keys.ToList();
        }

        public List<string> GetVariablesName(GraphVarType vartype, GraphVarLocalType localType)
        {
            if (localType == GraphVarLocalType.Global)
                return GetGlobalVariablesName(vartype);
            else
                return GetLocalVariablesName(vartype);

        }
        public List<string> GetTargetsName(TargetLocalType localType)
        {
            if (localType == TargetLocalType.global)
                return GetGlobalTargetsName();
            else
                return GetLocalTargetsName();

        }

        public GraphVarAddErrorsType AddTagVariable(string varName, GraphVarType varType)
        {
            GraphVarAddErrorsType result = GraphVarAddErrorsType.none;

            if (varName == FSMGUtility.StringTag_Undefined)
                return GraphVarAddErrorsType.invalidName;

            if (variables.ContainsKey(varName) == true)
            {
                result = GraphVarAddErrorsType.graph_already_exists;
            }
            else
            {
                variables.Add(varName, varType);
            }

            return result;
        }

        public override NodeGraph Copy()
        {
            Graph_State cp = (Graph_State)base.Copy();

            int indexOfRoot = nodes.IndexOf(RootState);
            int indexOfCurrent = nodes.IndexOf(CurrentState);

            //Confirmando os nos current e root, pois por algum motivos eles perdem a referencia quando sao instanciados
            cp._currentState = (NodeBase_State)cp.nodes[indexOfRoot];
            cp._rootState = (NodeBase_State)cp.nodes[indexOfCurrent];


            //Troca a referencia dos estados que são referencia nos nós de JUMP STATE.
            List<Node> jumps = cp.nodes.FindAll(r => r is Node_StateJump);
            foreach (Node j in jumps)
            {
                Node_StateJump nj = (Node_StateJump)j;
                nj.goToState = (NodeBase_State)cp.nodes[nodes.IndexOf(nj.goToState)];
            }

            //Renomeia os nos instanciados pois por algum motivo eles perdem o nome quando sao instanciado
            for (int i = 0; i < cp.nodes.Count; i++)
                cp.nodes[i].name = nodes[i].name;

            return cp;

        }
        public override Node AddNode(Type type)
        {
            //So pode haver 1 RootNode no grafico
            if (type == typeof(Node_StateRoot))
            {
                if (nodes.Exists(r => r.GetType() == typeof(Node_StateRoot)))
                {
                    return base.AddNode(typeof(Node_StateTransition));
                }
            }

            return base.AddNode(type);
        }

        public override void RemoveNode(Node node)
        {
            base.RemoveNode(node);
        }

        public Graph_State Instance
        {
            get
            {
                NodeGraph ncp = Copy();
                return ncp as Graph_State;
            }
        }

    }

}