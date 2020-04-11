using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



namespace XNode.FSMG
{

    [CreateNodeMenu("Variables/Get/Double")]
    public class Node_VariableGetDouble : NodeBase_VariableGet<double>
    {

        public override GraphVarType GetGraphVarType()
        {
            return GraphVarType.Double;
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