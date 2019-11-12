using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Library.Enumerador
{
    public class GenericEnum<T>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public string Conteudo { get; set; }
        public string Auxiliar { get; set; }

        public static GenericEnum<T>[] EnumList()
        {
            List<GenericEnum<T>> list = new List<GenericEnum<T>>();

            foreach (FieldInfo fieldInfo in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                DescriptionAttribute[] atributos = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                System.Type t = Enum.GetUnderlyingType(typeof(T));
                var ge = new GenericEnum<T>();
                ge.Name = atributos[0].Description;
                ge.Value = Convert.ChangeType(fieldInfo.GetValue(fieldInfo), t);
                ge.ID = Convert.ToInt32(ge.Value);
                list.Add(ge);
            }

            return list.ToArray();
        }

        public static string GetDescription(int pValue)
        {
            foreach (FieldInfo fieldInfo in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                DescriptionAttribute[] atributos = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                int value = (int)fieldInfo.GetValue(fieldInfo);
                if (value == pValue)
                {
                    return atributos[0].Description;
                }

            }
            return "";
        }

        public static string GetDescription(object pValue)
        {
            if (pValue == null)
                return string.Empty;

            foreach (FieldInfo fieldInfo in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                DescriptionAttribute[] atributos = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                object value = Convert.ChangeType(fieldInfo.GetValue(fieldInfo), pValue.GetType());
                if (value.Equals(pValue))
                {
                    return atributos[0].Description;
                }
            }

            return string.Empty;
        }

 
    }   
}
