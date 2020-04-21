
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode.FSMG;

namespace XNode.FSMG
{
    public class FSMGSettings : ScriptableObject
    {
        
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

        public string[] TargetsName
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(targets.Keys);
                return result.ToArray();
            }
        }
        public string[] Int_VariablesName
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(intVars.Keys);
                return result.ToArray();
            }
        }
        public string[] Float_VariablesName
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(floatVars.Keys);
                return result.ToArray();
            }
        }
        public string[] Double_VariablesName
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(doubleVars.Keys);
                return result.ToArray();
            }
        }
        public string[] Bool_VariablesName
        {
            get
            {
                List<string> result = new List<string>() { FSMGUtility.StringTag_Undefined };
                result.AddRange(boolVars.Keys);
                return result.ToArray();
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
        public bool TryGetDoubeVar(string variable, out DoubleVar doubleVar)
        {
            return doubleVars.TryGetValue(variable, out doubleVar);
        }
        public bool TryGetBoolVar(string variable, out BoolVar boolVar)
        {
            return boolVars.TryGetValue(variable, out boolVar);
        }

    }
}