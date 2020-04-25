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
        public override GraphVarType GetGraphVarType()
        {
            return GraphVarType.Integer;
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