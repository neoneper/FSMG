using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode.FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class GraphStateAttribute : PropertyAttribute
    {
        private string callbackname;
        private bool useNodeEnum;
        public bool UseNodeEnum { get { return useNodeEnum; } }
        public string CallbackName { get { return callbackname; } }

        public GraphStateAttribute(bool nodeEnum = false, string callback="")
        {
            useNodeEnum = nodeEnum;
            callbackname = callback;
        }
    }
}