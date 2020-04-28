
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FSMG
{
    public class FSMGSettings : SingletonScriptableObject<FSMGSettings>
    {

        [SerializeField]
        private TagVarList systemVariables = null;
        [SerializeField]
        private TargetListGlobal systemTargets = null;

        [SerializeField]
        private TargetListGlobal targets = null;
        [SerializeField]
        private IntVarList intVars = null;
        [SerializeField]
        private FloatVarList floatVars = null;
        [SerializeField]
        private DoubleVarList doubleVars = null;
        [SerializeField]
        private BoolVarList boolVars = null;

        public List<string> TargetNames
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(targets.Keys);
                return result
;
            }
        }
        public List<string> Int_VariableNames
        {
            get
            {
                return intVars.GetVariableNames();
            }
        }
        public List<string> Float_VariableNames
        {
            get
            {
                return floatVars.GetVariableNames();
            }
        }
        public List<string> Double_VariableNames
        {
            get
            {
                return doubleVars.GetVariableNames();
            }
        }
        public List<string> Bool_VariableNames
        {
            get
            {
                return boolVars.GetVariableNames();
            }
        }
        public List<string> VariableNames
        {
            get
            {
                List<string> list = new List<string>();
                list.AddRange(intVars.GetVariableNames());
                list.AddRange(floatVars.GetVariableNames());
                list.AddRange(doubleVars.GetVariableNames());
                list.AddRange(boolVars.GetVariableNames());
                return list;
            }
        }
        public TagVarList VariableTags
        {
            get
            {
                TagVarList list = new TagVarList();
                list.AddRange(intVars.GetVariableTags());
                list.AddRange(floatVars.GetVariableTags());
                list.AddRange(doubleVars.GetVariableTags());
                list.AddRange(boolVars.GetVariableTags());
                return list;

            }
        }

        public bool TryGetIntVar(string variable, out IntVar intVar)
        {
            return intVars.TryGetValue(variable, out intVar);
        }
        public bool TryGetFloatVar(string variable, out FloatVar floatVar)
        {
            return floatVars.TryGetValue(variable, out floatVar);
        }
        public bool TryGetDoubleVar(string variable, out DoubleVar doubleVar)
        {
            return doubleVars.TryGetValue(variable, out doubleVar);
        }
        public bool TryGetBoolVar(string variable, out BoolVar boolVar)
        {
            return boolVars.TryGetValue(variable, out boolVar);
        }
        public bool TryGetIntValue(string variable, out int result)
        {
            return intVars.TryGetRealValue(variable, out result);
        }
        public bool TryGetFloatValue(string variable, out float result)
        {
            return floatVars.TryGetRealValue(variable, out result);
        }
        public bool TryGetDoubeVaule(string variable, out double result)
        {
            return doubleVars.TryGetRealValue(variable, out result);
        }
        public bool TryGetBoolValue(string variable, out bool result)
        {
            return boolVars.TryGetRealValue(variable, out result);
        }
        public bool SetOrAddValue<T, Y>(string varName, Y value, out T variable) where T : class where Y : IComparable
        {
            T result = null;

            if (Exist(varName) == false)
            {
                switch (typeof(ValueTuple).ToGraphType())
                {
                    case GraphVarType.Boolean:
                        result = (T)Convert.ChangeType(boolVars.SetOrCreatelValue(varName, Convert.ToBoolean(value)), typeof(T));
                        break;
                    case GraphVarType.Double:
                        result = (T)Convert.ChangeType(doubleVars.SetOrCreatelValue(varName, Convert.ToDouble(value)), typeof(T));
                        break;
                    case GraphVarType.Float:
                        result = (T)Convert.ChangeType(floatVars.SetOrCreatelValue(varName, (float)Convert.ToDouble(value)), typeof(T));
                        break;
                    case GraphVarType.Integer:
                        result = (T)Convert.ChangeType(intVars.SetOrCreatelValue(varName, (int)Convert.ToInt32(value)), typeof(T));
                        break;
                    case GraphVarType.Unknown:
                        result = default(T);
                        break;
                }
            }
            variable = result;
            return result != null;
        }
        public void RemoveVariable(string varName)
        {
            if (intVars.ContainsKey(varName))
            {
                intVars.Remove(varName);
            }
            else if (floatVars.ContainsKey(varName))
            {
                floatVars.Remove(varName);
            }
            else if (doubleVars.ContainsKey(varName))
            {
                doubleVars.Remove(varName);
            }
            else if (boolVars.ContainsKey(varName))
            {
                boolVars.Remove(varName);
            }
        }
        public void RemoveVariable(string varName, GraphVarType varType)
        {
            switch (varType)
            {
                case GraphVarType.Boolean:
                    boolVars.RemoveVariable(varName);
                    break;
                case GraphVarType.Double:
                    doubleVars.RemoveVariable(varName);
                    break;
                case GraphVarType.Float:
                    floatVars.RemoveVariable(varName);
                    break;
                case GraphVarType.Integer:
                    intVars.RemoveVariable(varName);
                    break;
            }
        }
        public bool Exist(string varname)
        {
            bool result = false;

            result = intVars.ContainsKey(varname);
            if (result)
                return result;

            result = floatVars.ContainsKey(varname);
            if (result)
                return result;

            result = doubleVars.ContainsKey(varname);
            if (result)
                return result;

            result = boolVars.ContainsKey(varname);

            return result;
        }
    }
}