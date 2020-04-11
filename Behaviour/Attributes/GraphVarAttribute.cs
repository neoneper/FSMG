using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode.FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class GraphVarAttribute : PropertyAttribute
    {
        private string callbackname = "";
        private bool useNodeEnum = false;
        private GraphVarLocalType graphVarLocalType;
        private GraphVarType graphVarType;
        private string getnames = "";
        public bool UseNodeEnum { get { return useNodeEnum; } }
        public string CallbackName { get { return callbackname; } }
        public string ValuesName { get { return getnames; } }

        public GraphVarAttribute(bool nodeEnum = false, string getNamesFunction = "", string callback = "")
        {
            useNodeEnum = nodeEnum;
            callbackname = callback;
            getnames = getNamesFunction;
        }
    }
}