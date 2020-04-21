using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode.FSMG;


namespace XNode.FSMG.Components
{
    public static class FSMTargetExtensions
    {
        public static float GetDistance(this FSMTargetBehaviour target, Transform transform)
        {
            return Vector3.Distance(target.transform.position, transform.position);
        }
        public static bool Compare(this FSMTargetBehaviour target, string stringCompare)
        {
            if (target == null)
                return false;

            return target.targetName == stringCompare;
        }
    }

    /// <summary>
    /// Base component to create FSMTarget components in behaviours
    /// </summary>
    public abstract class FSMTargetBehaviour : MonoBehaviour
    {
        public static string UndefinedTag { get { return XNode.FSMG.FSMGUtility.StringTag_Undefined; } }
                
        public virtual string targetName { get { return UndefinedTag; } }

        public virtual bool IsUndefindedTarget { get { return targetName == UndefinedTag; } }
       
    }
}