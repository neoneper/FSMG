using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XNode.FSMG
{
    public abstract class NodeBase_Operation<T> : Node where T : IComparable<T>
    {

        public enum IntOperations
        {
            Sum,
            Mult,
            Div,
            Sub
        }

        [Input(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)]
        public T valueA;
        [NodeEnum]
        public IntOperations operation = IntOperations.Sum;
        [Input] public T valueB;

        [Output]
        public T resultValue;

        public virtual T Calculate()
        {
            T result = default(T);

            switch (operation)
            {
                case IntOperations.Sum:
                    result = Sum();
                    break;
                case IntOperations.Sub:
                    result = Sub();
                    break;
                case IntOperations.Div:
                    result = Div();
                    break;
                case IntOperations.Mult:
                    result = Mult();
                    break;
            }

            return result;
        }

        public abstract T Sum();
        public abstract T Div();
        public abstract T Mult();
        public abstract T Sub();

    }


}