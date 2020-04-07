using UnityEngine;
using System.Collections;
using System;
using XNode.FSMG.SerializableDictionary;

namespace XNode.FSMG
{
    [Serializable]
    public class IntVar
    {
        public int min = int.MinValue;
        public int max = int.MaxValue;
        public int value = 0;

        public void SetValue(int val)
        {
            if (val < min)
                value = min;
            else if (val > max)
                value = max;
            else
                value = val;

        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
    [Serializable]
    public class FloatVar
    {
        public float min = float.MinValue;
        public float max = float.MaxValue;
        public float value = 0;

        public void SetValue(float val)
        {
            if (val < min)
                value = min;
            else if (val > max)
                value = max;
            else
                value = val;

        }


        public override string ToString()
        {
            return value.ToString();
        }
    }
    [Serializable]
    public class DoubleVar
    {
        public double min = float.MinValue;
        public double max = float.MaxValue;
        public double value = 0;

        public void SetValue(double val)
        {
            if (val < min)
                value = min;
            else if (val > max)
                value = max;
            else
                value = val;

        }
        public override string ToString()
        {
            return value.ToString();
        }
    }
    [Serializable]
    public class BoolVar
    {
        public bool value = false;

        public void SetValue(bool val)
        {
            value = val;
        }
        public override string ToString()
        {
            return value.ToString();
        }
    }

    [Serializable]
    public class TagVar
    {
        GraphVarType varType = GraphVarType.Boolean;

        public override string ToString()
        {
            string result = "";
            switch (varType)
            {
                case GraphVarType.Boolean:
                    result = "Boolean";
                    break;
                case GraphVarType.Double:
                    result = "Double";
                    break;
                case GraphVarType.Float:
                    result = "Float";
                    break;
                case GraphVarType.Integer:
                    result = "Integer";
                    break;

            }

            return result;
        }
    }

    [Serializable]
    public class IntVarList : SerializableDictionaryBase<string, IntVar>
    {

    }
    [Serializable]
    public class FloatVarList : SerializableDictionaryBase<string, FloatVar>
    {


    }
    [Serializable]
    public class DoubleVarList : SerializableDictionaryBase<string, DoubleVar>
    {

    }
    [Serializable]
    public class BoolVarList : SerializableDictionaryBase<string, BoolVar>
    {

    }
    [Serializable]
    public class TagVarList : SerializableDictionaryBase<string, GraphVarType>
    {

    }
    [Serializable]
    public enum GraphVarType
    {
        Integer,
        Float,
        Double,
        Boolean
    }

    [Serializable]
    public enum GraphAddVarErrorsType
    {
        none,
        unknown,                //Desconhecido
        fsm_already_exists,     //Ja existe no FSM component
        graph_already_exists,   //Ja existe no Gráfico
        invalidName             //Nome inválido
    }


}
