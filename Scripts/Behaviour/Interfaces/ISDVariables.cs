using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMG
{
    public interface ISDVariables<T, Y> where T : IComparable where Y: class
    {
        void RemoveVariable(string varname);
        bool TryAddVariable(string varname, out Y variable);
        bool TryGetRealValue(string varname, out T result);
        bool TrySetVarRealValue(string varname, T value);
        Y SetOrCreatelValue(string varname, T value);
        List<string> GetVariableNames();
        TagVarList GetVariableTags();
    }
}