using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG.SerializableDictionary;

namespace XNode.FSMG
{
    [Serializable]
    public enum TargetType
    {
        Default = 0
    }
    
    [Serializable]
    public class TargetList : SerializableDictionaryBase<string, TargetType>
    {

    }
}