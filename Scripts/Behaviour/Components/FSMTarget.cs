
using UnityEngine;

namespace FSMG.Components
{
    [AddComponentMenu("FSM/Target")]
    public class FSMTarget : FSMTargetBehaviour
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


        public FSMTargetBehaviour ToGeneric() { return (FSMTargetBehaviour)this; }
    }
}