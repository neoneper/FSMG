using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.FSMG;

namespace XNode.FSMG.Components
{

    public static class FSMTargetExtensions
    {
        public static float GetDistance(this FSMTarget target, Transform transform)
        {
            return Vector3.Distance(target.transform.position, transform.position);
        }
        public static bool Compare(this FSMTarget target, string stringCompare)
        {
            if (target == null)
                return false;

            return target.targetName == stringCompare;
        }
    }

    [AddComponentMenu("FSM/Target")]
    public class FSMTarget : MonoBehaviour
    {

        public static string UndefinedTag { get { return XNode.FSMG.FSMGUtility.StringTag_Undefined; } }

        [FSMTargets]
        public string targetName = UndefinedTag;

        public bool IsUndefindedTarget { get { return targetName == UndefinedTag; } }

    }
}