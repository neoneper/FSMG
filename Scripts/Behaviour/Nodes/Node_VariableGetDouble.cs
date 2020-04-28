using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



using XNode; namespace FSMG
{

    [CreateNodeMenu("Variables/Get/Double")]
    public class Node_VariableGetDouble : NodeBase_VariableGet<double>
    {

        [SerializeField, GraphVar(true, GraphVarType.Double)]
        private string variableName = FSMGUtility.StringTag_Undefined;

        public override string GetVariableName() { return variableName; }
        public override object GetValue(NodePort port)
        {
            return base.GetVariableValue();
        }
    }


}