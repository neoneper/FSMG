using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using XNode; namespace FSMG
{

    [NodeWidth(150), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Operations/Float")]
    public class Node_OpFloat : NodeBase_Operation<float>
    {


        public override object GetValue(NodePort port)
        {
            float result = 0;

            result = Calculate();

            return result;

        }

        public override float Sum()
        {
            float a = GetInputValue<float>("valueA", this.valueA);
            float b = GetInputValue<float>("valueB", this.valueB);
            return a + b;

        }
        public override float Div()
        {
            float a = GetInputValue<float>("valueA", this.valueA);
            float b = GetInputValue<float>("valueB", this.valueB);
            return a / b;

        }
        public override float Mult()
        {
            float a = GetInputValue<float>("valueA", this.valueA);
            float b = GetInputValue<float>("valueB", this.valueB);
            return a * b;

        }
        public override float Sub()
        {
            float a = GetInputValue<float>("valueA", this.valueA);
            float b = GetInputValue<float>("valueB", this.valueB);
            return a - b;

        }

    }


}