using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSMG.Components
{
    /// <summary>
    /// Base component to create FSM components in behaviours. 
    /// Create their AI controllers (Agents), using this base.
    /// </summary>
    public abstract class FSMBehaviour : MonoBehaviour
    {
        //Utilizado em runtime quando o componente é iniciado pela primeira vez.
        //Quando falso o componente irá criar uma instancia do gráfico original para trabalhar.
        //Uma vez criado a instancia esta variavel se torna verdadeira e impede novas instancais.
        private bool isGraphInstantied = false;

        [SerializeField, GraphState(callback: "OnGraphChangedInEditor")]
        private Graph_State _graph = null;

        [SerializeField, SD_DrawOptions(false, false), SD_DrawKeyAsLabel]
        private TargetListLocal targets = null;
        [SerializeField]
        private FSMVariableWorkBase variables;
        public FSMVariableWorkBase Variables { get { return variables; } }

        private bool readyToWork = false;

        /// <summary>
        /// Utilzie esta váriavel para fazer com que ações e decisões e outros componentes
        /// saibam quando devem esperar para trabalhar.  
        /// </summary>
        public bool isReady
        {
            get { return readyToWork; }
            set { readyToWork = value; }
        }
        /// <summary>
        /// Gráfico de estados ao qual este componente pertence. 
        /// Controladores FSM podem trabalhar apenas com um gráfico.
        /// Trocar o gráfico irá remover todas as variaveis e seus valores contidos no antigo gráfico.
        /// </summary>
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


        /// <summary>
        /// Procura e retorna todos os trajetos com o nome especificado
        /// </summary>
        /// <param name="targetName">Nome dos trajetos a serem procurados</param>
        /// <param name="fsmTarget">lista que recebe os resultados</param>
        /// <param name="localType">Informa ao sistema de busca se o trajeto é local <seealso cref="FSMTargetLocal"/>
        /// ou se é global <see cref="FSMTarget"/>
        /// </param>
        /// <returns>Verdadeiro se encontrado</returns>
        public bool TryGetFSMTarget(string targetName, out List<FSMTargetBehaviour> fsmTargets, TargetLocalType localType)
        {

            switch (localType)
            {
                case TargetLocalType.global:
                    FSManager.Instance.TryGetFSMTarget(targetName, out fsmTargets);
                    break;
                case TargetLocalType.local:
                    fsmTargets = targets.Where(pair => pair.Key == targetName).Select(pair => pair.Value.fsmTarget).ToList();
                    break;
                default:
                    fsmTargets = new List<FSMTargetBehaviour>(0);
                    break;

            }

            return fsmTargets.Count > 0;
        }
        /// <summary>
        /// Sincroniza as variaveis e trajetos do componente com as variaveis do gráfico de estados
        /// </summary>
        public void SyncVariablesAndTargets()
        {
            SyncTargets();

            if (graph == null)
                return;

            TagVarList graphVariables = graph.VariableTags;

            TagVarList myTagVariables = Variables.VariableTags;

            //Remove variaveis que estejam somente no componente mas que nao estejam no grafico
            foreach (string variable in myTagVariables.Keys)
            {
                if (graphVariables.ContainsKey(variable) == false)
                {
                    Variables.RemoveVariable(variable, myTagVariables[variable]);
                }
            }

            myTagVariables.Clear();
            myTagVariables = null;

            //Adiciona variaveis marcadas no grafico que não existam no FSM
            foreach (string variable in graphVariables.Keys)
                Variables.AddVariable(variable, graphVariables[variable]);

            graphVariables.Clear();
            graphVariables = null;

        }
        public bool CheckIfCountDownElapsed(float duration)
        {
            if (graph == null)
                return false;

            return (graph.CurrentStateElapsedTime >= duration);
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
                ClearVariablesAndTargets();
                return;
            }

            SyncVariablesAndTargets();
        }
        /// <summary>
        /// Apaga todos os valores contidos nas variaveis de gráfico. Int, Float, Double e Bool e também trajetos
        /// </summary>
        protected void ClearVariablesAndTargets()
        {
            Variables.ClearAllVariables();
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

    }
}