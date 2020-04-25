using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



using XNode; namespace FSMG
{

    [NodeWidth(200), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Conditional/Double")]
    public class Node_ConditionalDouble : Node
    {



        [Input(connectionType = ConnectionType.Override)]
        public double inputValueA;
        [Input(connectionType = ConnectionType.Override)]
        public double inputValueB;

        [NodeEnum]
        public NearlyConditionType condition = NearlyConditionType.equal;
        [NodeEnum]
        public EpsilonType precision = EpsilonType.Decimal;

        [Output]
        public bool outPutValue = false;

        public override object GetValue(NodePort port)
        {
            double a = GetInputValue<double>("inputValueA", this.inputValueA);
            double b = GetInputValue<double>("inputValueB", this.inputValueB);

            bool result = condition.CheckNearlyCondition(a, b, precision);

            return result;
        }


    }


}