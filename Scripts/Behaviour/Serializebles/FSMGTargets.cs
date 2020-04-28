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
        Private = 0,
        Public = 1
    }

    [Serializable]
    public class TargetComponent
    {
        public TargetLocalType targetType;
        public FSMTargetBehaviour fsmTarget;

        public override string ToString()
        {            
            return targetType.ToString();
        }
    }
   
    [Serializable]
    public class TargetListPrivate : SerializableDictionaryBase<string, TargetLocalType>
    {

    }
    
    [Serializable]
    public class TargetListPublic : SerializableDictionaryBase<string, TargetComponent>
    {

    }
}