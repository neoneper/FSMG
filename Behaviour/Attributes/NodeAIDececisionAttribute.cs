using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XNode; namespace FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class NodeAIDececisionAttribute : PropertyAttribute
    {
        private string callbackname;
        private bool useNodeEnum;
        public bool UseNodeEnum { get { return useNodeEnum; } }
        public string CallbackName { get { return callbackname; } }

        public NodeAIDececisionAttribute(bool nodeEnum = false, string callback="")
        {
            useNodeEnum = nodeEnum;
            callbackname = callback;
        }
    }
}