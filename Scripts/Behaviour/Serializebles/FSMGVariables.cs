
using System;
using FSMG;

namespace FSMG
{
    /// <summary>
    /// Utilizado pelos gráfico de estados e também pelos controladores FSM para armazenar valores <see cref="int"/>.
    /// </summary>
    [Serializable]
    public class IntVar
    {
        public int min = int.MinValue;
        public int max = int.MaxValue;
        public int value = 0;

        public void SetValue(int val)
        {
            if (val < min)
                value = min;
            else if (val > max)
                value = max;
            else
                value = val;

        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
    /// <summary>
    /// Utilizado pelos gráfico de estados e também pelos controladores FSM para armazenar valores <see cref="float"/>
    /// </summary>
    [Serializable]
    public class FloatVar
    {
        public float min = float.MinValue;
        public float max = float.MaxValue;
        public float value = 0;

        public void SetValue(float val)
        {
            if (val < min)
                value = min;
            else if (val > max)
                value = max;
            else
                value = val;

        }


        public override string ToString()
        {
            return value.ToString();
        }
    }
    /// <summary>
    /// Utilizado pelos gráfico de estados e também pelos controladores FSM para armazenar valores <see cref="double"/>
    /// </summary>
    [Serializable]
    public class DoubleVar
    {
        public double min = float.MinValue;
        public double max = float.MaxValue;
        public double value = 0;

        public void SetValue(double val)
        {
            if (val < min)
                value = min;
            else if (val > max)
                value = max;
            else
                value = val;

        }
        public override string ToString()
        {
            return value.ToString();
        }
    }
    /// <summary>
    /// Utilizado pelos gráfico de estados e também pelos controladores FSM para armazenar valores <see cref="bool"/>
    /// </summary>
    [Serializable]
    public class BoolVar
    {
        public bool value = false;

        public void SetValue(bool val)
        {
            value = val;
        }
        public override string ToString()
        {
            return value.ToString();
        }
    }
    /// <summary>
    /// Utilizado pelos gráfico de estados e também pelos controladores FSM para armazenar apenas o tipo de uma várivel <seealso cref="GraphVarType"/>
    /// </summary>
    [Serializable]
    public class TagVar
    {
        GraphVarType varType = GraphVarType.Boolean;

        public override string ToString()
        {
            string result = "";
            switch (varType)
            {
                case GraphVarType.Boolean:
                    result = "Boolean";
                    break;
                case GraphVarType.Double:
                    result = "Double";
                    break;
                case GraphVarType.Float:
                    result = "Float";
                    break;
                case GraphVarType.Integer:
                    result = "Integer";
                    break;

            }

            return result;
        }
    }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação para o tipo de variavel <seealso cref="IntVar"/>,
    /// O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class IntVarList : SerializableDictionaryBase<string, IntVar> { }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação para o tipo de variavel <seealso cref="FloatVar"/>,
    /// O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class FloatVarList : SerializableDictionaryBase<string, FloatVar> { }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação para o tipo de variavel <seealso cref="DoubleVar"/>,
    /// O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class DoubleVarList : SerializableDictionaryBase<string, DoubleVar> { }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação para o tipo de variavel <seealso cref="BoolVar"/>,
    /// O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class BoolVarList : SerializableDictionaryBase<string, BoolVar> { }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação que guarda o nome de uma váriavel e seu tipo,
    /// Utilizado para ajudar outros sistemas de procura a encontrar uma variavel diretamente em uma 
    /// lista correspondente ao seu tipo. O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class TagVarList : SerializableDictionaryBase<string, GraphVarType> { }

    /// <summary>
    /// Informa o tipo de uma variavel
    /// </summary>
    [Serializable]
    public enum GraphVarType
    {
        Integer,
        Float,
        Double,
        Boolean
    }
    /// <summary>
    /// Utilizado para informar se uma variavel esta sendo utilizada pelo sistema global de variaveis ou
    /// por um gráfico, que neste caso podemos considerar LOCAL, pois apenas componentes que utilizam este gráfico poderão ter
    /// acesso à esta variavel.
    /// </summary>
    public enum GraphVarLocalType { Local, Global }

    /// <summary>
    /// Utilziado para informar qual o tipo de retorno alguma função teve na tentative de adicionar uma variavel a alguma lista.
    /// Tenha em mente que o valor <seealso cref="GraphVarAddErrorsType.none"/>, significa que não houver erro algum!
    /// </summary>
    [Serializable]
    public enum GraphVarAddErrorsType
    {
        none,
        unknown,                //Desconhecido
        fsm_already_exists,     //Ja existe no FSM component
        graph_already_exists,   //Ja existe no Gráfico
        invalidName             //Nome inválido
    }


}
