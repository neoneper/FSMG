using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



using XNode; namespace FSMG
{

    [CreateNodeMenu("Variables/Get/Bool")]
    public class Node_VariableGetBool : NodeBase_VariableGet<bool>
    {
        [SerializeField, GraphVar(true, GraphVarType.Boolean)]
        private string variableName = FSMGUtility.StringTag_Undefined;

        public override string GetVariableName() { return variableName; }


        public override object GetValue(NodePort port)
        {
            return base.GetVariableValue();
        }
    }


}