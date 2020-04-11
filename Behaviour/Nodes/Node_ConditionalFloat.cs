using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



namespace XNode.FSMG
{

    [NodeWidth(200), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Conditional/Float")]
    public class Node_ConditionalFloat : Node
    {


        [Input(connectionType = ConnectionType.Override)]
        public float inputValueA;
        [Input(connectionType = ConnectionType.Override)]
        public float inputValueB;

        [NodeEnum]
        public NearlyConditionType condition = NearlyConditionType.equal;
        [NodeEnum]
        public EpsilonType precision = EpsilonType.Decimal;

        [Output]
        public bool outPutValue = false;

        public override object GetValue(NodePort port)
        {
            float a = GetInputValue<float>("inputValueA", this.inputValueA);
            float b = GetInputValue<float>("inputValueB", this.inputValueB);

            bool result = condition.CheckNearlyCondition(a, b, precision);

            return result;
        }



    }


}