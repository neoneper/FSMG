using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FSMG;


using XNode; namespace FSMG.Components
{
    public static class FSMTargetExtensions
    {
        public static float GetDistance(this FSMTargetBehaviour target, Transform transform)
        {
            return Vector3.Distance(target.transform.position, transform.position);
        }
        public static bool Compare(this FSMTargetBehaviour target, string stringCompare)
        {
            if (target == null)
                return false;

            return target.targetName == stringCompare;
        }
    }

    /// <summary>
    /// Esta é a base para implementar componentes de trajeto para o gráfico de estados.
    /// FSMTarget, é como um gráfico entende qualquer objeto no mundo. Você poderá utilizar as referencias 
    /// destes trajetos espalhados pelo mundo para implementar ações e decisões no gráfico de estados, gerenciando
    /// o fluxo de comportamento baseado em objetos que estão no mundo. Por exemplo: Você pode adicionar algum tipo
    /// de FSMTarget a algum objeto no mundo e então ter uma ação no gráfico que faz com que seu Controlador FSM, <see cref="FSMBehaviour"/>, 
    /// Siga-o. Algo como GoToTarget.
    /// </summary>
    public abstract class FSMTargetBehaviour : MonoBehaviour
    {
        public static string UndefinedTag { get { return FSMG.FSMGUtility.StringTag_Undefined; } }
                
        /// <summary>
        /// nome do trajeto referenciado na lista global <seealso cref="FSMGSettings.TargetNames"/> ou em uma lista local
        /// de algum gráfico <see cref="Graph_State.GetLocalTargetsName()"/>.
        /// Você poderá utilizar o sistema automático de busca que será apresentado no Inspector como uma lista PopPup.
        /// Para isto utilize o attributo "[FSMTarget]", para capturar automatica a lista de trajetos globais ou então
        /// utilize o mesmo Atributo mas com propriedades para definição de lista manual, onde você poderá fornecer
        /// manualmente a lista de nomes de trajetos e ele se encarregará de manter o formato Popup para você na inspector.
        /// </summary>
        public virtual string targetName { get { return UndefinedTag; } }

        /// <summary>
        /// Retorna verdadeiro se o <see cref="targetName"/> for nullo, vazio ou igual ao nome "Undefined". Veja também, <see cref="UndefinedTag"/>
        /// </summary>
        public virtual bool IsUndefindedTarget { get { return targetName == UndefinedTag || string.IsNullOrEmpty(targetName); } }
       
    }
}