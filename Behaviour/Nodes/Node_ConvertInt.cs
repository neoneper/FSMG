using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



namespace XNode.FSMG
{

    [NodeWidth(200), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Convert/Integer")]
    public class Node_ConvertInt : Node
    {
        public enum RoundType { round, ceiling, floor }

        [Input(connectionType = ConnectionType.Override)]
        public GenericNumber inputValue;

        [NodeEnum]
        public RoundType round = RoundType.round;

        [Output]
        public int outPutValue = 0;

        public override object GetValue(NodePort port)
        {
            object inputvalue = GetInputValue<object>("inputValue", this.inputValue);

            decimal result = 0;

            decimal.TryParse(inputvalue.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result);

            int returnResult = 0;

            switch (round)
            {
                case RoundType.round:
                    returnResult = (int)Math.Round(result);
                    break;
                case RoundType.ceiling:

                    returnResult = (int)Math.Ceiling(result);

                    break;
                case RoundType.floor:
                    returnResult = (int)Math.Floor(result);

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