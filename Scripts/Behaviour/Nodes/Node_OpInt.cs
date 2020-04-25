using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

using XNode; namespace FSMG
{

    [NodeWidth(150), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Operations/Integer")]
    public class Node_OpInt : NodeBase_Operation<int>
    {
        public override object GetValue(NodePort port)
        {
            int result = Calculate();
            return result;
        }

        public override int Sum()
        {
           
            int a = GetInputValue<int>("valueA", this.valueA);
            int b = GetInputValue<int>("valueB", this.valueB);
            return a + b;

        }
        public override int Div()
        {
            int a = GetInputValue<int>("valueA", this.valueA);
            int b = GetInputValue<int>("valueB", this.valueB);
            return a / b;

        }
        public override int Mult()
        {
            int a = GetInputValue<int>("valueA", this.valueA);
            int b = GetInputValue<int>("valueB", this.valueB);
            return a * b;

        }
        public override int Sub()
        {
            int a = GetInputValue<int>("valueA", this.valueA);
            int b = GetInputValue<int>("valueB", this.valueB);
            return a - b;

        }
    }


}