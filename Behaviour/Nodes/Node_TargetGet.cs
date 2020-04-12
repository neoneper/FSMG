using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using XNode.FSMG.Components;

namespace XNode.FSMG
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
        public FSMTarget outTarget;

        protected List<string> GetTargetsName()
        {
            return ((Graph_State)graph).GetTargetsName(localType);
        }

        public override object GetValue(NodePort port)
        {
            return GetTarget();
        }



        private FSMTarget GetTarget()
        {
            if (string.IsNullOrEmpty(targetName) || targetName.Equals(FSMGUtility.StringTag_Undefined))
                return null;

            if (Application.isEditor && Application.isPlaying == false)
                return null;

            FSMTarget result = null;

            ((Graph_State)graph).TryGetTarget(targetName, out result, localType);

            return result;
        }
    }


}