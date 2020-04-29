using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace FSMG
{
    [Serializable]
    public class FSMVariableWorkBase
    {
        [SerializeField]
        private IntVarList intVars = null;
        [SerializeField]
        private FloatVarList floatVars = null;
        [SerializeField]
        private DoubleVarList doubleVars = null;
        [SerializeField]
        private BoolVarList boolVars = null;

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
        
        public bool AddVariable(string variable, GraphVarType varType)
        {
            bool result = false;

            switch (varType)
            {
                case GraphVarType.Boolean:
                    BoolVar bv = null;
                    result = boolVars.TryAddVariable(variable, out bv);
                    break;
                case GraphVarType.Double:
                    DoubleVar dv = null;
                    result = doubleVars.TryAddVariable(variable, out dv);
                    break;
                case GraphVarType.Float:
                    FloatVar fv = null;
                    result = floatVars.TryAddVariable(variable, out fv);
                    break;
                case GraphVarType.Integer:
                    IntVar iv = null;
                    result = intVars.TryAddVariable(variable, out iv);
                    break;
                case GraphVarType.Unknown:
                    break;
            }

            return result;
        }
        public bool AddVariable(string variable, out VarBase varbase, GraphVarType varType)
        {
            bool result = false;

            switch (varType)
            {
                case GraphVarType.Boolean:
                    BoolVar bv = null;
                    result = boolVars.TryAddVariable(variable, out bv);
                    varbase = bv;
                    break;
                case GraphVarType.Double:
                    DoubleVar dv = null;
                    result = doubleVars.TryAddVariable(variable, out dv);
                    varbase = dv;
                    break;
                case GraphVarType.Float:
                    FloatVar fv = null;
                    result = floatVars.TryAddVariable(variable, out fv);
                    varbase = fv;
                    break;
                case GraphVarType.Integer:
                    IntVar iv = null;
                    result = intVars.TryAddVariable(variable, out iv);
                    varbase = iv;
                    break;
                case GraphVarType.Unknown:
                    varbase = null;
                    break;
                default:
                    varbase = null;
                    break;

            }

            return result;
        }
        public bool AddIntVariable(string variable)
        {
            IntVar intvar = null;
            return intVars.TryAddVariable(variable, out intvar);
        }
        public bool AddFloatVariable(string variable)
        {
            FloatVar floatVar = null;
            return floatVars.TryAddVariable(variable, out floatVar);
        }
        public bool AddDoubleVariable(string variable)
        {
            DoubleVar doublevar = null;
            return doubleVars.TryAddVariable(variable, out doublevar);
        }
        public bool AddBoolVariable(string variable)
        {
            BoolVar boolvar = null;
            return boolVars.TryAddVariable(variable, out boolvar);
        }
        public bool AddIntVariable(string variable, out IntVar intVar)
        {
            return intVars.TryAddVariable(variable, out intVar);
        }
        public bool AddFloatVariable(string variable, out FloatVar floatVar)
        {
            return floatVars.TryAddVariable(variable, out floatVar);
        }
        public bool AddDoubleVariable(string variable, out DoubleVar doubleVar)
        {
            return doubleVars.TryAddVariable(variable, out doubleVar);
        }
        public bool AddBoolVariable(string variable, out BoolVar boolVar)
        {
            return boolVars.TryAddVariable(variable, out boolVar);
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
        public bool SetOrAddValue<T, Y>(string varName, Y value, out T variable) where T : VarBase where Y : IComparable
        {
            VarBase result = null;
            Exist(varName, out result);

            switch (result.GetType().ToGraphType())
            {
                case GraphVarType.Boolean:

                    if (result == null)
                        result = boolVars.SetOrCreatelValue(varName, Convert.ToBoolean(value));
                    else
                        result.SetVale(value);
                    break;
                case GraphVarType.Double:
                    if (result == null)
                        result = doubleVars.SetOrCreatelValue(varName, Convert.ToDouble(value));
                    else
                        result.SetVale(value);
                    break;
                case GraphVarType.Float:
                    if (result == null)
                        result = floatVars.SetOrCreatelValue(varName, (float)Convert.ToDouble(value));
                    else
                        result.SetVale(value);
                    break;
                case GraphVarType.Integer:
                    if (result == null)
                        result = intVars.SetOrCreatelValue(varName, Convert.ToInt32(value));
                    else
                        result.SetVale(value);
                    break;
                case GraphVarType.Unknown:
                    result = default(T);
                    break;
            }


            variable = (T)result;
            return result != null;
        }
        public bool TryAddVariable<T>(string varName, out T variable) where T : VarBase
        {
            bool result = false;
            VarBase exitObject = null;

            if (Exist(varName, out exitObject) == false)
            {
                switch (result.GetType().ToGraphType())
                {
                    case GraphVarType.Boolean:

                        BoolVar outBoolVar = null;
                        result = boolVars.TryAddVariable(varName, out outBoolVar);
                        variable = (T)(VarBase)outBoolVar;
                        break;
                    case GraphVarType.Double:
                        DoubleVar outDoubleVar = null;
                        result = doubleVars.TryAddVariable(varName, out outDoubleVar);
                        variable = (T)(VarBase)outDoubleVar;
                        break;
                    case GraphVarType.Float:
                        FloatVar outFloatVar = null;
                        result = floatVars.TryAddVariable(varName, out outFloatVar);
                        variable = (T)(VarBase)outFloatVar;
                        break;
                    case GraphVarType.Integer:
                        IntVar outIntVar = null;
                        result = intVars.TryAddVariable(varName, out outIntVar);
                        variable = (T)(VarBase)outIntVar;
                        break;
                    case GraphVarType.Unknown:
                        variable = null;
                        break;
                    default:
                        variable = null;
                        break;
                }
            }
            else
            {
                variable = null;
            }

            return result;
        }
        public bool TryFindVariable<T>(string varName, out VarBase variable) where T : class
        {
            bool result = false;

            if (intVars.ContainsKey(varName))
            {
                result = true;
                variable = intVars[varName];
            }
            else if (floatVars.ContainsKey(varName))
            {
                floatVars.Remove(varName);
                result = true;
                variable = floatVars[varName];
            }
            else if (doubleVars.ContainsKey(varName))
            {
                doubleVars.Remove(varName);
                result = true;
                variable = doubleVars[varName];
            }
            else if (boolVars.ContainsKey(varName))
            {
                boolVars.Remove(varName);
                result = true;
                variable = boolVars[varName];
            }
            else
            {
                variable = default(VarBase);
            }

            return result;
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
        public bool Exist(string varname, out VarBase resultObject)
        {
            bool result = false;

            result = boolVars.ContainsKey(varname);
            if (result)
            {
                resultObject = boolVars[varname];
                return result;
            }

            result = doubleVars.ContainsKey(varname);
            if (result)
            {
                resultObject = doubleVars[varname];
                return result;
            }

            result = floatVars.ContainsKey(varname);
            if (result)
            {
                resultObject = floatVars[varname];
                return result;
            }

            result = intVars.ContainsKey(varname);
            if (result)
            {
                resultObject = intVars[varname];
                return result;
            }

            resultObject = null;
            return result;
        }
        public bool Contains(string varname)
        {
            VarBase result = null;
            return Exist(varname, out result);
        }
        public void ClearAllVariables()
        {
            intVars.Clear();
            floatVars.Clear();
            doubleVars.Clear();
            boolVars.Clear();
        }
    }
}