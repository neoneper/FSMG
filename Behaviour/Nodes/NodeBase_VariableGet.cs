using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNode.FSMG.Components;

namespace XNode.FSMG
{
    public abstract class NodeBase_VariableGet<T> : Node where T : IComparable<T>
    {


        [SerializeField, NodeEnumCallback("OnLocalTypeChanged"), Callback("")]
        private GraphVarLocalType localType = GraphVarLocalType.Local;

        [SerializeField, GraphVar(nodeEnum: true, getNamesFunction: "GetVariableNames")]
        private string variableName = "";

        [Output]
        public T outValue = default(T);

        public GraphVarLocalType LocalType { get { return localType; } }

        public abstract List<string> GetVariableNames();
        public abstract GraphVarType GetGraphVarType();

        public virtual T GetVariableValue()
        {
            dynamic result = default(T);
            Graph_State graphState = (Graph_State)graph;

            if (string.IsNullOrEmpty(variableName) || variableName == FSMGUtility.StringTag_Undefined)
                return result;

            if (Application.isEditor && Application.isPlaying == false && localType == GraphVarLocalType.Local)
                return result;

            switch (GetGraphVarType())
            {
                case GraphVarType.Boolean:
                    BoolVar boolVar = null;
                    if (graphState.TryGetBoolVar(variableName, out boolVar, localType)) { result = boolVar.value; }
                    break;
                case GraphVarType.Double:
                    DoubleVar doubleVar = null;
                    if (graphState.TryGetDoubeVar(variableName, out doubleVar, localType)) { result = doubleVar.value; }
                    break;
                case GraphVarType.Float:
                    FloatVar floatVar = null;
                    if (graphState.TryGetFloatVar(variableName, out floatVar, localType)) { result = floatVar.value; }
                    break;
                case GraphVarType.Integer:
                    IntVar intVar = null;
                    if (graphState.TryGetIntVar(variableName, out intVar, localType)) { result = intVar.value; }
                    break;
            }

            return result;
        }

        //INodeNoodles: Label das conexões
        public virtual string GetNoodleLabel()
        {
            return this.name;
        }
        public virtual INodeNoodleLabelActiveType GetNoodleLabelActive()
        {
            return INodeNoodleLabelActiveType.Selected;
        }

        protected virtual void OnLocalTypeChanged()
        {
            variableName = "";
        }

    }
}