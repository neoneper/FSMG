using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using XNode.FSMG;
using XNode.FSMG.SerializableDictionary;

namespace XNode.FSMG.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class FSMBehaviour : MonoBehaviour
    {
        private bool isGraphInstantied = false;

        private NavMeshAgent _navMeshAgent = null;
        private Rigidbody _rigidbody = null;

        private List<FSMTarget> _globalTargets = null;

        [SerializeField, GraphState(callback: "OnGraphChangedInEditor")]
        private Graph_State _graph = null;

        [SerializeField]
        private AIAgentStats _agentStats = null;

        [SerializeField, DrawOptions(false, false), DrawKeyAsLabel]
        private TargetListLocal targets = null;
        [SerializeField, DrawOptions(false, false), DrawKeyAsLabel]
        private IntVarList intVars = null;
        [SerializeField, DrawOptions(false, false), DrawKeyAsLabel]
        private FloatVarList floatVars = null;
        [SerializeField, DrawOptions(false, false), DrawKeyAsLabel]
        private DoubleVarList doubleVars = null;
        [SerializeField, DrawOptions(false, false), DrawKeyAsLabel]
        private BoolVarList boolVars = null;

        private bool readyToWork = false;
        public bool isReady
        {
            get { return readyToWork; }
            set
            {
                navMeshAgent.enabled = value;
                readyToWork = value;
            }
        }

        public Graph_State graph
        {
            get
            {
                if (Application.isPlaying && _graph != null && isGraphInstantied == false)
                {
                    _graph = _graph.Instance;
                    isGraphInstantied = true;

                }

                return _graph;
            }
        }

        public NavMeshAgent navMeshAgent
        {
            get
            {

                if (_navMeshAgent == null)
                    _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
                if (_navMeshAgent == null)
                    _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();

                return _navMeshAgent;


            }
        }
        public Rigidbody rigidBody
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody>();

                return _rigidbody;
            }
        }
        public AIAgentStats agentStats { get { return _agentStats; } }

        public List<FSMTarget> globalTargets
        {
            get
            {
                //A ideia é popular a lista somente a primeira vez que foi requisitado por este componente.
                //Tendo em mente que a cena não tera modificações na quantidade de trajetos em runtime.
                if (_globalTargets == null)
                {
                    _globalTargets = FindObjectsOfType<FSMTarget>().ToList();
                    _globalTargets.RemoveAll(r => r.targetName == FSMGUtility.StringTag_Undefined);
                }

                return new List<FSMTarget>(_globalTargets);

            }
        }

        public bool TryGetIntValue(string variable, out IntVar intVar)
        {
            return intVars.TryGetValue(variable, out intVar);
        }
        public bool TryGetFloatValue(string variable, out FloatVar floatVar)
        {
            return floatVars.TryGetValue(variable, out floatVar);
        }
        public bool TryGetDoubeValue(string variable, out DoubleVar doubleVar)
        {
            return doubleVars.TryGetValue(variable, out doubleVar);
        }
        public bool TryGetBooleanValue(string variable, out BoolVar boolVar)
        {
            return boolVars.TryGetValue(variable, out boolVar);
        }

        public GraphAddVarErrorsType AddVariable(string varName, GraphVarType varType)
        {
            GraphAddVarErrorsType result = GraphAddVarErrorsType.none;

            if (varName == FSMGUtility.StringTag_Undefined)
            {
                result = GraphAddVarErrorsType.invalidName;
                return result;
            }

            //Primeiro verifico se a variavel ja nao esta presente no COmponente.
            if (CheckIfVarExist(varName))
            {
                result = GraphAddVarErrorsType.fsm_already_exists;
                return result;
            }

            //Depois verifico se a variavel já esta marcada nó gráfico
            result = graph.AddTagVariable(varName, varType);

            if (result != GraphAddVarErrorsType.none) return result;


            switch (varType)
            {
                case GraphVarType.Boolean:
                    boolVars.Add(varName, new BoolVar());
                    break;

                case GraphVarType.Double:
                    doubleVars.Add(varName, new DoubleVar());
                    break;

                case GraphVarType.Float:
                    floatVars.Add(varName, new FloatVar());
                    break;
                case GraphVarType.Integer:
                    intVars.Add(varName, new IntVar());
                    break;
            }

            return result;
        }
        public void RemoveVariable(string varName, GraphVarType varType)
        {
            switch (varType)
            {
                case GraphVarType.Boolean:
                    boolVars.Remove(varName);
                    break;

                case GraphVarType.Double:
                    doubleVars.Remove(varName);
                    break;

                case GraphVarType.Float:
                    floatVars.Remove(varName);
                    break;
                case GraphVarType.Integer:
                    intVars.Remove(varName);
                    break;
            }

        }
        public bool CheckIfVarExist(string varName)
        {
            bool result = false;

            if (intVars.ContainsKey(varName))
            { result = true; }
            else if (floatVars.ContainsKey(varName))
            { result = true; }
            else if (doubleVars.ContainsKey(varName))
            { result = true; }
            else if (boolVars.ContainsKey(varName))
            { result = true; }

            return result;
        }
        public TagVarList GetVariablesAsTag()
        {
            TagVarList tagVariables = new TagVarList();
            foreach (string intKey in intVars.Keys)
                tagVariables.Add(intKey, GraphVarType.Integer);
            foreach (string floatKey in floatVars.Keys)
                tagVariables.Add(floatKey, GraphVarType.Float);
            foreach (string doubleKey in doubleVars.Keys)
                tagVariables.Add(doubleKey, GraphVarType.Double);
            foreach (string boolKey in boolVars.Keys)
                tagVariables.Add(boolKey, GraphVarType.Boolean);

            return tagVariables;

        }

        public bool TryGetFSMTarget(string targetName, out FSMTarget fsmTarget, TargetLocalType localType)
        {

            switch (localType)
            {
                case TargetLocalType.global:
                    fsmTarget = globalTargets.Find(r => r.targetName.Equals(targetName));
                    break;
                case TargetLocalType.local:

                    TargetLocal localtarget = null;
                    if (targets.TryGetValue(targetName, out localtarget))
                    {
                        fsmTarget = localtarget.fsmTarget;
                    }
                    else
                    {
                        fsmTarget = null;
                    }
                    break;
                default:

                    fsmTarget = null;
                    break;

            }

            return fsmTarget != null;
        }

        /// <summary>
        /// Chamado somente em modo editor sempre que o gráfico é modificado para este componente.
        /// Tenha emmente que ao modificar o grafico as variaveis origem são perdidas e subistituidas pelas variaveis
        /// do grafico destino.
        /// </summary>
        /// <param name="oldGraph">Grafico origem</param>
        /// <param name="newGraph">Grafico destino</param>
        protected virtual void OnGraphChangedInEditor(Graph_State oldGraph, Graph_State newGraph)
        {
            if (oldGraph == newGraph)
                return;

            if (newGraph == null)
            {
                ClearVariables();
                return;
            }

            SyncVariables();
        }

        /// <summary>
        /// Apaga todos os valores contidos nas variaveis de gráfico. Int, Float, Double e Bool.
        /// </summary>
        protected void ClearVariables()
        {
            intVars.Clear();
            floatVars.Clear();
            doubleVars.Clear();
            boolVars.Clear();
        }

        /// <summary>
        /// Sincroniza as variaveis do componente com as variaveis do gráfico de estados
        /// </summary>
        public void SyncVariables()
        {
            SyncTargets();

            if (graph == null)
                return;

            TagVarList graphVariables = null;

            TagVarList myTagVariables = GetVariablesAsTag();

            _graph.GetTagVariables(out graphVariables);

            //Remove variaveis que estejam somente no componente mas que nao estejam no grafico
            foreach (string variable in myTagVariables.Keys)
            {
                if (graphVariables.ContainsKey(variable) == false)
                {
                    RemoveVariable(variable, myTagVariables[variable]);
                }

            }

            myTagVariables.Clear();
            myTagVariables = null;

            //Adiciona variaveis marcadas no grafico que não existam no FSM
            foreach (string variable in graphVariables.Keys)
            {
                GraphVarType varType = graphVariables[variable];

                switch (varType)
                {
                    case GraphVarType.Boolean:
                        if (boolVars.ContainsKey(variable) == false)
                            boolVars.Add(variable, new BoolVar());
                        break;
                    case GraphVarType.Double:
                        if (doubleVars.ContainsKey(variable) == false)
                            doubleVars.Add(variable, new DoubleVar());
                        break;
                    case GraphVarType.Float:
                        if (floatVars.ContainsKey(variable) == false)
                            floatVars.Add(variable, new FloatVar());
                        break;
                    case GraphVarType.Integer:
                        if (intVars.ContainsKey(variable) == false)
                            intVars.Add(variable, new IntVar());
                        break;
                }
            }

            graphVariables.Clear();
            graphVariables = null;

        }
        private void SyncTargets()
        {
            if (graph == null)
                return;

            List<string> graphTargets = graph.GetTargetsName(TargetLocalType.local);
            List<string> localtargets = targets.Keys.ToList();

            //Remove variaveis que estejam somente no componente mas que nao estejam no grafico
            foreach (string gt in localtargets)
            {
                if (graphTargets.Contains(gt) == false)
                {
                    targets.Remove(gt);
                }
            }
            localtargets.Clear();
            localtargets = null;

            //Adiciona variaveis marcadas no grafico que não existam no FSM
            foreach (string gt in graphTargets)
            {
                if (targets.ContainsKey(gt) == false)
                {
                    targets.Add(gt, new TargetLocal());
                }
            }

            graphTargets.Clear();
            graphTargets = null;
        }
        public bool CheckIfCountDownElapsed(float duration)
        {
            if (graph == null)
                return false;

            return (graph.CurrentStateElapsedTime >= duration);
        }

    }
}