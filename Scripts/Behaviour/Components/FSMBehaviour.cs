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
        [SerializeField, SD_DrawOptions(false, false), SD_DrawKeyAsLabel]
        private IntVarList intVars = null;
        [SerializeField, SD_DrawOptions(false, false), SD_DrawKeyAsLabel]
        private FloatVarList floatVars = null;
        [SerializeField, SD_DrawOptions(false, false), SD_DrawKeyAsLabel]
        private DoubleVarList doubleVars = null;
        [SerializeField, SD_DrawOptions(false, false), SD_DrawKeyAsLabel]
        private BoolVarList boolVars = null;
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
        /// Procura e retorna em caso de sucesso uma variavel de gráfico do tipo <see cref="IntVar"/>
        /// </summary>
        /// <param name="variable">Nome da variavel</param>
        /// <param name="intVar">Se encontrado o objeto resultante <see cref="IntVar"/> será alocado nele.</param>
        /// <returns>Verdadeiro se encontrado, falso se não encontrado</returns>
        public bool TryGetIntValue(string variable, out IntVar intVar)
        {
            return intVars.TryGetValue(variable, out intVar);
        }
        /// <summary>
        /// Procura e retorna em caso de sucesso uma variavel de gráfico do tipo <see cref="FloatVar"/>
        /// </summary>
        /// <param name="variable">Nome da variavel</param>
        /// <param name="floatVar">Se encontrado o objeto resultante <see cref="FloatVar"/> será alocado nele.</param>
        /// <returns>Verdadeiro se encontrado, falso se não encontrado</returns>
        public bool TryGetFloatValue(string variable, out FloatVar floatVar)
        {
            return floatVars.TryGetValue(variable, out floatVar);
        }
        /// <summary>
        /// Procura e retorna em caso de sucesso uma variavel de gráfico do tipo <see cref="DoubleVar"/>
        /// </summary>
        /// <param name="variable">Nome da variavel</param>
        /// <param name="doubleVar">Se encontrado o objeto resultante <see cref="DoubleVar"/> será alocado nele.</param>
        /// <returns>Verdadeiro se encontrado, falso se não encontrado</returns>
        public bool TryGetDoubeValue(string variable, out DoubleVar doubleVar)
        {
            return doubleVars.TryGetValue(variable, out doubleVar);
        }
        /// <summary>
        /// Procura e retorna em caso de sucesso uma variavel de gráfico do tipo <see cref="BoolVar"/>
        /// </summary>
        /// <param name="variable">Nome da variavel</param>
        /// <param name="boolVar">Se encontrado o objeto resultante <see cref="BoolVar"/> será alocado nele.</param>
        /// <returns>Verdadeiro se encontrado, falso se não encontrado</returns>
        public bool TryGetBooleanValue(string variable, out BoolVar boolVar)
        {
            return boolVars.TryGetValue(variable, out boolVar);
        }
        /// <summary>
        /// Adiciona uma variavel ao gráfico caso éla ainda não exista.
        /// Tenha em mente que esta variavel sera adicionada ao gráfico ao qual este componente pertence,
        /// isto significa que todos os FSM que utilizem este mesmo gráfico também terão sua própria versão 
        /// desta variavel. Caso você remova utizando <seealso cref="RemoveVariable(string, GraphVarType)"/>
        /// todos os componentes <seealso cref="FSMBehaviour"/> ficarão sem ela.
        /// </summary>
        /// <param name="varName">Nome da variavel</param>
        /// <param name="varType">Tipo da variavel</param>
        /// <returns><see cref="GraphVarAddErrorsType.none"/> caso seja armazenada com sucesso. </returns>
        public GraphVarAddErrorsType AddVariable(string varName, GraphVarType varType)
        {
            GraphVarAddErrorsType result = GraphVarAddErrorsType.none;

            if (varName == FSMGUtility.StringTag_Undefined)
            {
                result = GraphVarAddErrorsType.invalidName;
                return result;
            }

            //Primeiro verifico se a variavel ja nao esta presente no COmponente.
            if (CheckIfVarExist(varName))
            {
                result = GraphVarAddErrorsType.fsm_already_exists;
                return result;
            }

            //Depois verifico se a variavel já esta marcada nó gráfico
            result = graph.AddTagVariable(varName, varType);

            if (result != GraphVarAddErrorsType.none) return result;


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
        /// <summary>
        /// Remove variavel do gráfico.
        /// Tenha em mente que todos os <see cref="FSMBehaviour"/> que utilizam este gráfico também ficarão
        /// sem esta variável.
        /// </summary>
        /// <param name="varName">Nome da variavel</param>
        /// <param name="varType">Tipo da variavel</param>
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
        /// <summary>
        /// Verifica se a variavel existe no gráfico.
        /// </summary>
        /// <param name="varName">Nome da variavel a ser pesquiasda</param>
        /// <returns>falso se não encontrada, verdadeiro se encontrado</returns>       
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
        /// <summary>
        /// Dicionário de lista reordenável contendo todas as variaveis do gráfico criadas ate o momento em formato
        /// <seealso cref="TagVar"/>
        /// </summary>
        /// <returns><seealso cref="TagVarList"/> contendo todas as variaveis do gráfico em formato <seealso cref="TagVar"/></returns>
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
            intVars.Clear();
            floatVars.Clear();
            doubleVars.Clear();
            boolVars.Clear();
            targets.Clear();
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