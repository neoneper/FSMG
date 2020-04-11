﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XNode.FSMG
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class NodeEnumCallbackAttribute : PropertyAttribute
    {
        public string callbackName = "";

        public NodeEnumCallbackAttribute(string callback = "")
        {
            callbackName = callback;
        }
    }
}