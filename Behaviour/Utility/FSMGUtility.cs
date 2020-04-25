using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XNode; namespace FSMG
{
    public class FSMGUtility
    {
        public static string StringTag_None
        {
            get
            {
                return "None";
            }
        }
        public static string StringTag_Undefined
        {
            get
            {
                return "Undefined";
            }
        }
        public static string StringTag_Nothing
        {
            get
            {
                return "Nothing";
            }
        }
        public static string StringTag_Default
        {
            get
            {
                return "Default";
            }
        }
    }

    public enum EpsilonType
    {
        Decimal,
        Currency,
        Exchange,
        Scientific,
        Physical,
        Quantum

    }
    public enum NearlyConditionType
    {
        equal,
        bigger,
        smaller,
        bigger_equal,
        smaller_equal
    }

    public static class NumbersExtensions
    {

        public static bool NearlyEqual(this float a, float b, float epsilon)
        {
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == b)
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || absA + absB < float.MinValue)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * float.MinValue);
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }
        public static bool NearlyEqual(this double a, double b, double epsilon)
        {
            const double MinNormal = 2.2250738585072014E-308d;
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double diff = Math.Abs(a - b);

            if (a.Equals(b))
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || absA + absB < MinNormal)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * MinNormal);
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }
        public static double GetEpsilonRound(this EpsilonType precision)
        {
            double result = 0.01f;
            switch (precision)
            {
                case EpsilonType.Decimal:
                    result = 0.001f;
                    break;
                case EpsilonType.Currency:
                    result = 0.0001f;
                    break;
                case EpsilonType.Exchange:
                    result = 0.00001f;
                    break;
                case EpsilonType.Physical:
                    result = 0.000001f;
                    break;
                case EpsilonType.Quantum:
                    result = 0.0000001f;
                    break;

            }

            return result;

        }

        public static bool CheckNearlyCondition(this NearlyConditionType condition, double a, double b, EpsilonType precision)
        {
            bool result = false;

            switch (condition)
            {
                case NearlyConditionType.bigger:
                    result = a > b;
                    break;
                case NearlyConditionType.bigger_equal:
                    result = a.NearlyEqual(b, precision.GetEpsilonRound()) || a > b;
                    break;
                case NearlyConditionType.equal:
                    result = a.NearlyEqual(b, precision.GetEpsilonRound());
                    break;
                case NearlyConditionType.smaller:
                    result = a < b;
                    break;
                case NearlyConditionType.smaller_equal:
                    result = a.NearlyEqual(b, precision.GetEpsilonRound()) || a < b;
                    break;
            }
            return result;
        }
        public static bool CheckNearlyCondition(this NearlyConditionType condition, float a, float b, EpsilonType precision)
        {
            bool result = false;

            switch (condition)
            {
                case NearlyConditionType.bigger:
                    result = a > b;
                    break;
                case NearlyConditionType.bigger_equal:
                    result = a.NearlyEqual(b, (float)precision.GetEpsilonRound()) || a > b;
                    break;
                case NearlyConditionType.equal:
                    result = a.NearlyEqual(b, (float)precision.GetEpsilonRound());
                    break;
                case NearlyConditionType.smaller:
                    result = a < b;
                    break;
                case NearlyConditionType.smaller_equal:
                    result = a.NearlyEqual(b, (float)precision.GetEpsilonRound()) || a < b;
                    break;
            }
            return result;
        }


    }



}