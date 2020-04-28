using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMG
{
    public interface IVariable<T> where T: IComparable
    {
        void SetValue(T value);
    }
}