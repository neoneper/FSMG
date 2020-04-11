using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode.FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class CallbackAttribute : PropertyAttribute
    {
        private string callbackname;  
        public string CallbackName { get { return callbackname; } }

        public CallbackAttribute( string callback="")
        {
           
            callbackname = callback;
        }
    }
}