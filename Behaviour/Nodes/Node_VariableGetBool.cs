using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



namespace XNode.FSMG
{

    [CreateNodeMenu("Variables/Get/Bool")]
    public class Node_VariableGetBool : NodeBase_VariableGet<bool>
    {
        public override GraphVarType GetGraphVarType()
        {
            return GraphVarType.Boolean;
        }
        public override List<string> GetVariableNames()
        {
            Graph_State graphState = (Graph_State)graph;

            return graphState.GetVariablesName(GetGraphVarType(), LocalType);
        }


        public override object GetValue(NodePort port)
        {
            return base.GetVariableValue();
        }
    }


}