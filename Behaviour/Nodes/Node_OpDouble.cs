using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XNode.FSMG
{

    [NodeWidth(150), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Operations/Double")]
    public class Node_OpDouble : NodeBase_Operation<double>
    {


        public override object GetValue(NodePort port)
        {
            double result = 0;

            result = Calculate();

            return result;

        }

        public override double Sum()
        {
            double a = GetInputValue<double>("valueA", this.valueA);
            double b = GetInputValue<double>("valueB", this.valueB);
            return a + b;

        }
        public override double Div()
        {
            double a = GetInputValue<double>("valueA", this.valueA);
            double b = GetInputValue<double>("valueB", this.valueB);
            return a / b;

        }
        public override double Mult()
        {
            double a = GetInputValue<double>("valueA", this.valueA);
            double b = GetInputValue<double>("valueB", this.valueB);
            return a * b;

        }
        public override double Sub()
        {
            double a = GetInputValue<double>("valueA", this.valueA);
            double b = GetInputValue<double>("valueB", this.valueB);
            return a - b;

        }

    }


}