using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSMG.Components
{
    public class FSManager : MonoBehaviour
    {
        private static FSManager instance;
        public static FSManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("FSManager", new System.Type[] { typeof(FSManager) });
                    instance = go.GetComponent<FSManager>();
                }
                return instance;
            }
        }


        //Lista de trajetos globais já instanciados no mundo.
        //ésta lista será populada somente uma vez, quando requisitado pelo seu metodo publico GET,
        private List<FSMTarget> _globalTargets = null;

        /// <summary>
        /// Uma Lista de trajetos globais já instanciados no mundo.
        /// </summary>
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

        /// <summary>
        /// Lista de todos os trajetos com o nome espesificado 
        /// </summary>
        /// <param name="targetName">Nome do trajeto a ser procurado</param>
        /// <param name="fsmTarget">variavel para alocação, se encontrado</param>
        /// <param name="localType">Informa ao sistema de busca se o trajeto é local <seealso cref="FSMTargetLocal"/>
        /// ou se é global <see cref="FSMTarget"/>
        /// </param>
        /// <returns>Verdadeiro se encontrado</returns>
        public bool TryGetFSMTarget(string targetName, out List<FSMTargetBehaviour> targetsGlobal)
        {
            targetsGlobal = globalTargets.Where(r => r.targetName == targetName && r.IsUndefindedTarget == false).Select(r => r.ToGeneric()).ToList();
            return targetsGlobal.Count > 0;
        }



    }
}