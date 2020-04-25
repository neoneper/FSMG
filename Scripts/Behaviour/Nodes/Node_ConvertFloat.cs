using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



using XNode; namespace FSMG
{

    [NodeWidth(200), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Convert/Float")]
    public class Node_ConvertFloat : Node
    {
        public enum RoundType { nothing, round, ceiling, floor }

        [Input(connectionType = ConnectionType.Override)]
        public GenericNumber inputValue;

        [NodeEnum]
        public RoundType round = RoundType.round;

        [Output]
        public float outPutValue = 0;

        public override object GetValue(NodePort port)
        {
            object inputvalue = GetInputValue<object>("inputValue", this.inputValue);

            decimal result = 0;

            decimal.TryParse(inputvalue.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result);

            float returnResult = 0;

            switch (round)
            {
                case RoundType.nothing:
                    returnResult = (float)result;
                    break;
                case RoundType.round:
                    returnResult = (float)Math.Round(result);
                    break;
                case RoundType.ceiling:
                    returnResult = (float)Math.Ceiling(result);
                    break;
                case RoundType.floor:
                    returnResult = (float)Math.Floor(result);
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