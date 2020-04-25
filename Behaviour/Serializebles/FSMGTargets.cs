using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSMG.Components;
using FSMG;

using XNode; namespace FSMG
{
    [Serializable]
    public enum TargetType
    {
        Default = 0
    }

    [Serializable]
    public class TargetLocal
    {
        public TargetType targetType;
        public FSMTargetBehaviour fsmTarget;

        public override string ToString()
        {
            
            return targetType.ToString();
        }
    }

    [Serializable]
    public enum TargetLocalType
    {
        local,
        global
    }

    [Serializable]
    public class TargetListGlobal : SerializableDictionaryBase<string, TargetType>
    {

    }
    [Serializable]
    public class TargetListLocal : SerializableDictionaryBase<string, TargetLocal>
    {

    }
}