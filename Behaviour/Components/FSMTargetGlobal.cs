using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode.FSMG.Components
{
    [AddComponentMenu("FSM/TargetGlobal")]
    public class FSMTargetGlobal : FSMTargetBehaviour
    {
        [SerializeField, FSMTargets]
        private string target = UndefinedTag;

        public override string targetName
        {
            get
            {
                return target;
            }
        }
    }
}