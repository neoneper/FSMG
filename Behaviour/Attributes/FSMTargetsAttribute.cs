using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode.FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class FSMTargetsAttribute : PropertyAttribute
    {
        private string callbackname;
        private bool useNodeEnum;
        private bool filterEnnable = true;

        public bool UseNodeEnum { get { return useNodeEnum; } }
        public string CallbackName { get { return callbackname; } }
        public bool IsFilterEnnable { get { return filterEnnable; } }

        public FSMTargetsAttribute(bool nodeEnum = false, bool filterIsEnnable=true, string callback="")
        {
            filterEnnable = filterIsEnnable;
            useNodeEnum = nodeEnum;
            callbackname = callback;
        }
    }
}