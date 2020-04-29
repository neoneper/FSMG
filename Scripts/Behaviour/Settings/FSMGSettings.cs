
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
        private TargetListGlobal targets = null;

        [SerializeField]
        private FSMVariableWorkBase variables=null;


        public FSMVariableWorkBase Variables { get { return variables; } }
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

       
    }
}