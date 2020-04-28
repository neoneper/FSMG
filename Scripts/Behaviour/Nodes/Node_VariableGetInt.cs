using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



using XNode; namespace FSMG
{

    [CreateNodeMenu("Variables/Get/Int")]
    public class Node_VariableGetInt : NodeBase_VariableGet<int>
    {
        [SerializeField,GraphVar(true, GraphVarType.Integer)]
        private string variableName = FSMGUtility.StringTag_Undefined;

        public override string GetVariableName() { return variableName; }

        public override object GetValue(NodePort port)
        {
            return base.GetVariableValue();
        }

        
    }


}