using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XNode; namespace FSMG.Components
{
    [AddComponentMenu("FSM/TargetLocal")]
    public class FSMTargetLocal : FSMTargetBehaviour
    {
        [SerializeField, GraphState]
        private Graph_State graph=null;

        [SerializeField, FSMTargets(useOwnList: true, getListFunction: "GetLocalTargets")]
        private string target = UndefinedTag;

        public override string targetName
        {
            get
            {
                return target;
            }
        }

        protected List<string> GetLocalTargets()
        {
            if (graph != null)
                return graph.GetLocalTargetsName();
            else
                return new List<string>(0);
        }
    }
}