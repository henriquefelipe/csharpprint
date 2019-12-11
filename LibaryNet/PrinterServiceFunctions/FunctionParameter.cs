using System;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class FunctionParameter
    {
        public string Name { get; private set; }

        public object Value { get; private set; }

        public FunctionParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        internal string GetString()
        {
            return Value.ToString();
        }

        internal int GetInt()
        {
            return int.Parse(GetString());
        }

        public bool GetBool()
        {
            return bool.Parse(GetString());
        }

        public decimal GetDecimal()
        {
            return decimal.Parse(GetString());
        }

        public double GetDouble()
        {
            return double.Parse(GetString());
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(GetString());
        }

        public char GetChar()
        {
            return char.Parse(GetString());
        }
    }
}
