using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using FSMG.Components;
using XNode;

namespace FSMG
{

    [NodeWidth(200), NodeTint("#7D679B")]
    [CreateNodeMenu("Targets/Get")]
    public class Node_TargetsGet : Node
    {
      
        [SerializeField, NodeEnum]
        private TargetComponentType localType = TargetComponentType.local;

        [SerializeField, GraphVar(true, "GetTargetsName")]
        private string targetName = "";

        [Output]
        public List<FSMTargetBehaviour> outTargets;

        protected List<string> GetTargetsName()
        {
            return ((Graph_State)graph).GetTargetsName(localType);
        }

        public override object GetValue(NodePort port)
        {
            return GetTargets();
        }
        public void SetTarget(string targetName, TargetComponentType localType)
        {
            this.localType = localType;
            this.targetName = targetName;
        }
        private List<FSMTargetBehaviour> GetTargets()
        {
            if (string.IsNullOrEmpty(targetName) || targetName.Equals(FSMGUtility.StringTag_Undefined))
                return null;

            if (Application.isEditor && Application.isPlaying == false)
                return null;

            List<FSMTargetBehaviour> result = null;

            ((Graph_State)graph).TryGetTarget(targetName, out result, localType);

            return result;
        }

    }


}