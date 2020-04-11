using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



namespace XNode.FSMG
{

    [NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Conditional/Integer")]
    public class Node_ConditionalInt : Node
    {
        public enum ConditionType
        {
            equal,
            bigger,
            smaller,
            bigger_equal,
            smaller_equal
        }

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int inputValueA;
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int inputValueB;

        [NodeEnum]
        public ConditionType condition = ConditionType.equal;

        [Output(typeConstraint = TypeConstraint.Strict)]
        public bool outPutValue = false;

        public override object GetValue(NodePort port)
        {

            int a = GetInputValue<int>("inputValueA", this.inputValueA);
            int b = GetInputValue<int>("inputValueB", this.inputValueB);

            bool result = CheckCondition(a, b);

            return result;
        }

        private bool CheckCondition(int a, int b)
        {
            bool result = false;

            switch (condition)
            {
                case ConditionType.bigger:
                    result = a > b;
                    break;
                case ConditionType.bigger_equal:
                    result = a.Equals(b) || a > b;
                    break;
                case ConditionType.equal:
                    result = a.Equals(b);
                    break;
                case ConditionType.smaller:
                    result = a < b;
                    break;
                case ConditionType.smaller_equal:
                    result = a.Equals(b) || a < b;
                    break;

            }
            return result;
        }



    }


}