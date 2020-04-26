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
    public class Node_TargetGet : Node
    {
        [SerializeField, NodeEnum]
        private TargetLocalType localType = TargetLocalType.local;

        [SerializeField, GraphVar(true, "GetTargetsName")]
        private string targetName = "";

        [Output]
        public FSMTargetBehaviour outTarget;

        protected List<string> GetTargetsName()
        {
            return ((Graph_State)graph).GetTargetsName(localType);
        }

        public override object GetValue(NodePort port)
        {
            return GetTarget();
        }
        public void SetTarget(string targetName, TargetLocalType localType)
        {
            this.localType = localType;
            this.targetName = targetName;
        }
        private FSMTargetBehaviour GetTarget()
        {
            if (string.IsNullOrEmpty(targetName) || targetName.Equals(FSMGUtility.StringTag_Undefined))
                return null;

            if (Application.isEditor && Application.isPlaying == false)
                return null;

            FSMTargetBehaviour result = null;

            ((Graph_State)graph).TryGetTarget(targetName, out result, localType);

            return result;
        }

    }


}