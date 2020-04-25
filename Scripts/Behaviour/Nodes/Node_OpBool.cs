using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using XNode; namespace FSMG
{

    [NodeWidth(190), NodeTint("#ff7b47")]
    [CreateNodeMenu("Math/Operations/Bool")]
    public class Node_OpBool : Node
    {

        public enum BoolOperations
        {
            And,
            Or,
            AndNot,
            Equal,
            Different,
            OrNot
        }

        [Input]
        public bool valueA;
        [NodeEnum]
        public BoolOperations operation = BoolOperations.Equal;
        [Input] public bool valueB;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public bool resultValue;

        public virtual bool Calculate()
        {
            bool result = false;

            switch (operation)
            {
                case BoolOperations.And:
                    result = And();
                    break;
                case BoolOperations.AndNot:
                    result = AndNot();
                    break;
                case BoolOperations.Or:
                    result = Or();
                    break;
                case BoolOperations.Equal:
                    result = Equal();
                    break;
                case BoolOperations.Different:
                    result = Different();
                    break;
                case BoolOperations.OrNot:
                    result = OrNot();
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        /// <summary>
        /// O operador AND é usado para comparar dois valores booleanos. 
        /// Retorna true se ambos os operandos forem verdadeiros.
        /// </summary>
        /// <returns></returns>
        public bool And()
        {

            bool a = GetInputValue("valueA", this.valueA);
            bool b = GetInputValue("valueB", this.valueB);

            return a & b;

        }
        /// <summary>
        /// O operador NOT é um operador unário, pois opera em um único operando.
        /// O operador NOT inverte o valor de um valor booleano. 
        /// Se o valor original for verdadeiro, o valor retornado será falso;
        /// se o valor original for falso, o valor de retorno será verdadeiro. 
        /// A operação NOT é frequentemente conhecida como complemento binário.
        /// Nesta Operação o resultado sera equivalnte a NOT(A And B) 
        /// </summary>
        /// <returns>Not (A and B)</returns>
        public bool AndNot()
        {
            bool a = GetInputValue("valueA", this.valueA);
            bool b = GetInputValue("valueB", this.valueB);

            return !(a & b);
        }
        /// <summary>
        /// O operador NOT é um operador unário, pois opera em um único operando.
        /// O operador NOT inverte o valor de um valor booleano. 
        /// Se o valor original for verdadeiro, o valor retornado será falso;
        /// se o valor original for falso, o valor de retorno será verdadeiro. 
        /// A operação NOT é frequentemente conhecida como complemento binário.
        /// Nesta Operação o resultado sera equivalnte a NOT(A Or B) 
        /// </summary>
        /// <returns>Not (A Or B)</returns>
        public bool OrNot()
        {
            bool a = GetInputValue("valueA", this.valueA);
            bool b = GetInputValue("valueB", this.valueB);

            return !(a | b);
        }
        /// <summary>
        /// O operador OR é semelhante a AND, pois é usado para comparar dois valores booleanos. 
        /// O operador OR retorna true se um dos operandos for verdadeiro.
        /// </summary>
        /// <returns>True or False</returns>
        public bool Or()
        {

            bool a = GetInputValue("valueA", this.valueA);
            bool b = GetInputValue("valueB", this.valueB);

            return a | b;
        }
        public bool Equal()
        {
            bool a = GetInputValue("valueA", this.valueA);
            bool b = GetInputValue("valueB", this.valueB);

            return a == b;
        }
        public bool Different()
        {
            bool a = GetInputValue("valueA", this.valueA);
            bool b = GetInputValue("valueB", this.valueB);

            return a != b;
        }

        public override object GetValue(NodePort port)
        {
            bool result = Calculate();
            return result;
        }



    }


}