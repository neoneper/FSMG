using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XNode; namespace FSMG
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class NodeLabelAttribute : PropertyAttribute
    {
        private GUIContent label;
        public GUIContent Label { get { return label; } }
        public NodeLabelAttribute(string _label)
        {
            label = new GUIContent(_label);
        }
    }
}