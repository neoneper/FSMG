using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



using XNode; namespace FSMG
{

    [CreateNodeMenu("Variables/Get/Float")]
    public class Node_VariableGetFloat : NodeBase_VariableGet<float>
    {

        [SerializeField, GraphVar(true, GraphVarType.Float)]
        private string variableName = FSMGUtility.StringTag_Undefined;

        public override string GetVariableName() { return variableName; }
        public override object GetValue(NodePort port)
        {
            return base.GetVariableValue();
        }
    }


}