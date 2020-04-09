using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



namespace XNode.FSMG
{

    [NodeWidth(200), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Convert/Double")]
    public class Node_ConvertDouble : Node
    {
        public enum RoundType { nothing, round, ceiling, floor }

        [Input(connectionType = ConnectionType.Override)]
        public GenericNumber inputValue;

        [NodeEnum]
        public RoundType round = RoundType.round;

        [Output]
        public double outPutValue = 0;

        public override object GetValue(NodePort port)
        {
            object inputvalue = GetInputValue<object>("inputValue", this.inputValue);

            decimal result = 0;

            decimal.TryParse(inputvalue.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result);

            double returnResult = 0;

            switch (round)
            {
                case RoundType.nothing:
                    returnResult = (double)result;
                    break;
                case RoundType.round:
                    returnResult = (double)Math.Round(result);
                    break;
                case RoundType.ceiling:
                    returnResult = (double)Math.Ceiling(result);
                    break;
                case RoundType.floor:
                    returnResult = (double)Math.Floor(result);
                    break;
            }

            return returnResult;
        }

        [Serializable]
        public class GenericNumber
        {

        }
    }


}