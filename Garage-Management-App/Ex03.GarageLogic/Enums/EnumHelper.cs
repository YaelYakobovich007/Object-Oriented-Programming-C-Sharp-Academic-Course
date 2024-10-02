using System;
using System.Text;

namespace Ex03.GarageLogic.Enums
{
    public static class EnumHelper
    {
        public static T Parse<T>(int i_Value) where T : Enum
        {
            if(!Enum.IsDefined(typeof(T), i_Value))
            {
                throw new ArgumentException(string.Format("{0} is not a valid value for {1}", i_Value, FormatEnumName(typeof(T).Name)));
            }

            return (T)(object)i_Value;
        }

        public static T Parse<T>(string i_Value) where T : Enum
        {
            if(int.TryParse(i_Value, out int numericValue))
            {
                return Parse<T>(numericValue);
            }
            else
            {
                throw new ArgumentException(string.Format("{0} is not a valid numeric value for {1}", i_Value, FormatEnumName(typeof(T).Name)));
            }
        }

        public static string GetEnumDescriptions(Type i_EnumDescription)
        {
            StringBuilder enumBuilder = new StringBuilder();

            foreach(var value in Enum.GetValues(i_EnumDescription))
            {
                enumBuilder.AppendFormat("{0}. {1}{2}", (int)value, FormatEnumName(value.ToString()), Environment.NewLine);
            }

            return enumBuilder.ToString();
        }

        public static string FormatEnumName(string i_EnumName)
        {
            StringBuilder formattedName = new StringBuilder();
            int consecutiveUpperCount = 0;

            if(i_EnumName.StartsWith("e"))
            {
                i_EnumName = i_EnumName.Substring(1);
            }

            foreach(char currentChar in i_EnumName)
            {
                if(char.IsUpper(currentChar))
                {
                    consecutiveUpperCount++;

                    if(formattedName.Length > 0 && consecutiveUpperCount == 1)
                    {
                        formattedName.Append(' ');
                    }
                }
                else
                {
                    consecutiveUpperCount = 0;
                }

                formattedName.Append(currentChar);
            }

            return formattedName.ToString();
        }
    }
}
