using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



namespace XNode.FSMG
{

    [CreateNodeMenu("Variables/Get/Float")]
    public class Node_VariableGetFloat : NodeBase_VariableGet<float>
    {

        public override GraphVarType GetGraphVarType()
        {
            return GraphVarType.Float;
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