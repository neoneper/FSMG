using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG.Components;

namespace XNode.FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class FSMTargetsAttribute : PropertyAttribute
    {
        private string callbackname;
        private bool useNodeEnum;
        private bool filterEnnable = true;
        private string getListFunction = "";
        private bool useOwnList = false;

        public bool UseNodeEnum { get { return useNodeEnum; } }
        public string CallbackName { get { return callbackname; } }
        public bool IsFilterEnnable { get { return filterEnnable; } }
        public string GetListFunctionName { get { return getListFunction; } }
        public bool IsUseOwnListTargets { get { return useOwnList; } }

        public FSMTargetsAttribute(bool nodeEnum = false, bool filterIsEnnable = true, bool useOwnList = false, string getListFunction = "", string callback = "")
        {
            filterEnnable = filterIsEnnable;
            useNodeEnum = nodeEnum;
            callbackname = callback;
            this.useOwnList = useOwnList;
            this.getListFunction = getListFunction;
        }
    }
}