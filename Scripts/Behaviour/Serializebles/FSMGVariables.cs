
using System;
using System.Collections.Generic;
using System.Linq;
using FSMG;

namespace FSMG
{
    public static class GraphVarExtensions
    {
        public static GraphVarType ToGraphType(this Type t)
        {
            GraphVarType varType = GraphVarType.Unknown;

            if (t == typeof(int))
                return GraphVarType.Integer;
            if (t == typeof(float))
                return GraphVarType.Float;
            if (t == typeof(double))
                return GraphVarType.Double;
            if (t == typeof(bool))
                return GraphVarType.Boolean;

            return varType;
        }
    }
    /// <summary>
    /// Utilizado pelos gráfico de estados e também pelos controladores FSM para armazenar valores <see cref="int"/>.
    /// </summary>
    [Serializable]
    public class IntVar : IVariable<int>
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
    public class FloatVar : IVariable<float>
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
    public class DoubleVar : IVariable<double>
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
    public class BoolVar : IVariable<bool>
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
    public class IntVarList : SerializableDictionaryBase<string, IntVar>, ISDVariables<int, IntVar>
    {
        public void RemoveVariable(string varname) { this.Remove(varname); }
        public List<string> GetVariableNames() { return this.Keys.ToList(); }
        public TagVarList GetVariableTags()
        {
            TagVarList varlist = new TagVarList();

            foreach (string varname in Keys)
                varlist.Add(varname, GraphVarType.Integer);

            return varlist;
        }
        public IntVar SetOrCreatelValue(string varname, int value)
        {
            IntVar result = null;
            if (TryGetValue(varname, out result))
            {
                result.value = value;
            }
            else
            {
                result = new IntVar() { value = value };
                this.Add(varname, result);
            }

            return result;
        }
        public bool TryAddVariable(string varname, out IntVar variable)
        {
            IntVar result = null;

            if (ContainsKey(varname) == false)
            {
                result = new IntVar() { value = 0 };
                Add(varname, result);
            }

            variable = result;
            return result != null;
        }
        public bool TryGetRealValue(string varname, out int result)
        {
            IntVar tmp = null;

            if (this.TryGetValue(varname, out tmp))
            {
                result = tmp.value;
            }
            else
            {
                result = 0;
            }

            return tmp != null;
        }
        public bool TrySetVarRealValue(string varname, int value)
        {
            IntVar tmp = null;
            if (this.TryGetValue(varname, out tmp))
            {
                tmp.SetValue(value);
            }

            return tmp != null;
        }

    }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação para o tipo de variavel <seealso cref="FloatVar"/>,
    /// O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class FloatVarList : SerializableDictionaryBase<string, FloatVar>, ISDVariables<float, FloatVar>
    {
        public void RemoveVariable(string varname) { this.Remove(varname); }
        public FloatVar SetOrCreatelValue(string varname, float value)
        {
            FloatVar result = null;
            if (TryGetValue(varname, out result))
            {
                result.value = value;
            }
            else
            {
                result = new FloatVar() { value = value };
                this.Add(varname, result);
            }

            return result;
        }
        public bool TryAddVariable(string varname, out FloatVar variable)
        {
            FloatVar result = null;

            if (ContainsKey(varname) == false)
            {
                result = new FloatVar() { value = 0 };
                Add(varname, result);
            }

            variable = result;
            return result != null;
        }
        public List<string> GetVariableNames() { return this.Keys.ToList(); }
        public TagVarList GetVariableTags()
        {
            TagVarList varlist = new TagVarList();

            foreach (string varname in Keys)
                varlist.Add(varname, GraphVarType.Float);

            return varlist;
        }
        public bool TryGetRealValue(string varname, out float result)
        {
            FloatVar tmp = null;

            if (this.TryGetValue(varname, out tmp))
            {
                result = tmp.value;
            }
            else
            {
                result = 0;
            }

            return tmp != null;
        }
        public bool TrySetVarRealValue(string varname, float value)
        {
            FloatVar tmp = null;
            if (this.TryGetValue(varname, out tmp))
            {
                tmp.SetValue(value);
            }

            return tmp != null;
        }
    }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação para o tipo de variavel <seealso cref="DoubleVar"/>,
    /// O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class DoubleVarList : SerializableDictionaryBase<string, DoubleVar>, ISDVariables<double, DoubleVar>
    {
        public void RemoveVariable(string varname) { this.Remove(varname); }
        public DoubleVar SetOrCreatelValue(string varname, double value)
        {
            DoubleVar result = null;
            if (TryGetValue(varname, out result))
            {
                result.value = value;
            }
            else
            {
                result = new DoubleVar() { value = value };
                this.Add(varname, result);
            }

            return result;
        }
        public bool TryAddVariable(string varname, out DoubleVar variable)
        {
            DoubleVar result = null;

            if (ContainsKey(varname) == false)
            {
                result = new DoubleVar() { value = 0 };
                Add(varname, result);
            }

            variable = result;
            return result != null;
        }
        public TagVarList GetVariableTags()
        {
            TagVarList varlist = new TagVarList();

            foreach (string varname in Keys)
                varlist.Add(varname, GraphVarType.Double);

            return varlist;
        }
        public List<string> GetVariableNames() { return this.Keys.ToList(); }
        public bool TryGetRealValue(string varname, out double result)
        {
            DoubleVar tmp = null;

            if (this.TryGetValue(varname, out tmp))
            {
                result = tmp.value;
            }
            else
            {
                result = 0;
            }

            return tmp != null;
        }
        public bool TrySetVarRealValue(string varname, double value)
        {
            DoubleVar tmp = null;
            if (this.TryGetValue(varname, out tmp))
            {
                tmp.SetValue(value);
            }

            return tmp != null;
        }
    }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação para o tipo de variavel <seealso cref="BoolVar"/>,
    /// O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class BoolVarList : SerializableDictionaryBase<string, BoolVar>, ISDVariables<bool, BoolVar>
    {
        public void RemoveVariable(string varname) { this.Remove(varname); }
        public BoolVar SetOrCreatelValue(string varname, bool value)
        {
            BoolVar result = null;
            if (TryGetValue(varname, out result))
            {
                result.value = value;
            }
            else
            {
                result = new BoolVar() { value = value };
                this.Add(varname, result);
            }

            return result;
        }
        public bool TryAddVariable(string varname, out BoolVar variable)
        {
            BoolVar result = null;

            if (ContainsKey(varname) == false)
            {
                result = new BoolVar() { value = false };
                Add(varname, result);
            }

            variable = result;
            return result != null;
        }
        public TagVarList GetVariableTags()
        {
            TagVarList varlist = new TagVarList();

            foreach (string varname in Keys)
                varlist.Add(varname, GraphVarType.Boolean);

            return varlist;
        }
        public List<string> GetVariableNames() { return this.Keys.ToList(); }
        public bool TryGetRealValue(string varname, out bool result)
        {
            BoolVar tmp = null;

            if (this.TryGetValue(varname, out tmp))
            {
                result = tmp.value;
            }
            else
            {
                result = false;
            }

            return tmp != null;
        }
        public bool TrySetVarRealValue(string varname, bool value)
        {
            BoolVar tmp = null;
            if (this.TryGetValue(varname, out tmp))
            {
                tmp.SetValue(value);
            }

            return tmp != null;
        }
    }
    /// <summary>
    /// Um tipo de dicinário com sistema de reordenação que guarda o nome de uma váriavel e seu tipo,
    /// Utilizado para ajudar outros sistemas de procura a encontrar uma variavel diretamente em uma 
    /// lista correspondente ao seu tipo. O Nome da variavel é a chave do dicionário.
    /// </summary>
    [Serializable]
    public class TagVarList : SerializableDictionaryBase<string, GraphVarType>
    {

        public List<string> TagNames { get { return this.Keys.ToList(); } }
        public void AddRange(TagVarList otherList)
        {
            if (otherList == null)
                return;

            foreach(string key in otherList.Keys)
            {
                this.Add(key, otherList[key]);
            }
        }

    }

    /// <summary>
    /// Informa o tipo de uma variavel
    /// </summary>
    [Serializable]
    public enum GraphVarType
    {
        Integer,
        Float,
        Double,
        Boolean,
        Unknown
    }
    /// <summary>
    /// Utilizado para informar se uma variavel esta sendo utilizada pelo sistema global de variaveis ou
    /// por um gráfico, que neste caso podemos considerar LOCAL, pois apenas componentes que utilizam este gráfico poderão ter
    /// acesso à esta variavel.
    /// </summary>
    public enum GraphVarLocalType { Public, Private }

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
