using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG.Components;
using XNode.FSMG.SerializableDictionary;

namespace XNode.FSMG
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
        public FSMTarget fsmTarget;

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