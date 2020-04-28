using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using FSMG.Components;
namespace FSMG
{
    public abstract class NodeBase_VariableGet<T> : Node where T : IComparable<T>
    {

        [Output]
        public T outValue = default(T);

        public abstract string GetVariableName();

        public virtual T GetVariableValue()
        {
            T result = default(T);
            string variableName = GetVariableName();

            Graph_State graphState = (Graph_State)graph;

            if (string.IsNullOrEmpty(variableName) || variableName == FSMGUtility.StringTag_Undefined)
                return result;

            if (Application.isEditor && Application.isPlaying == false)
                return result;

            switch (typeof(T).ToGraphType())
            {
                case GraphVarType.Boolean:
                    BoolVar boolVar = null;
                    if (graphState.TryGetBoolVar(variableName, out boolVar)) { result = (T)Convert.ChangeType(boolVar.value, typeof(T)); }
                    break;
                case GraphVarType.Double:
                    DoubleVar doubleVar = null;
                    if (graphState.TryGetDoubeVar(variableName, out doubleVar)) { result = (T)Convert.ChangeType(doubleVar.value, typeof(T)); }
                    break;
                case GraphVarType.Float:
                    FloatVar floatVar = null;
                    if (graphState.TryGetFloatVar(variableName, out floatVar)) { result = (T)Convert.ChangeType(floatVar.value, typeof(T)); }
                    break;
                case GraphVarType.Integer:
                    IntVar intVar = null;
                    if (graphState.TryGetIntVar(variableName, out intVar)) { result = (T)Convert.ChangeType(intVar.value, typeof(T)); }
                    break;
            }

            return result;
        }
    }
}