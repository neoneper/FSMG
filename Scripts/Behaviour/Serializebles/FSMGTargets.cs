using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSMG.Components;
using FSMG;

using XNode; namespace FSMG
{
    [Serializable]
    public enum TargetLocalType
    {
        global = 0,
        local = 1
    }

    [Serializable]
    public class TargetLocal
    {
        public TargetLocalType targetType;
        public FSMTargetBehaviour fsmTarget;

        public override string ToString()
        {            
            return targetType.ToString();
        }
    }
   
    [Serializable]
    public class TargetListGlobal : SerializableDictionaryBase<string, TargetLocalType>
    {

    }
    
    [Serializable]
    public class TargetListLocal : SerializableDictionaryBase<string, TargetLocal>
    {

    }
}