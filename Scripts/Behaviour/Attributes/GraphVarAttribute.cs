using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XNode; namespace FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class GraphVarAttribute : PropertyAttribute
    {
        private string callbackname = "";
        private bool useNodeEnum = false;
        private GraphVarType _varType = GraphVarType.Unknown;
        public bool UseNodeEnum { get { return useNodeEnum; } }
        public string CallbackName { get { return callbackname; } }
        public GraphVarType VarType { get { return _varType; } }

        public GraphVarAttribute(bool nodeEnum = false, GraphVarType varType = GraphVarType.Unknown, string callback = "")
        {
            useNodeEnum = nodeEnum;
            callbackname = callback;
            _varType = varType;
        }
    }
}