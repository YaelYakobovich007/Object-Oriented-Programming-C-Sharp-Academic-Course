using System;
using System.Text;

namespace Ex03.GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;
        private readonly string r_ObjectType;

        public ValueOutOfRangeException(string i_ErrorMessage, string i_ObjectType, float i_MaxValue, float i_MinValue) : base(i_ErrorMessage)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
            r_ObjectType = i_ObjectType;
        }

        public override string ToString()
        {
            StringBuilder errorMessageBuilder = new StringBuilder();

            errorMessageBuilder.AppendFormat("Illegal action on a {0}{1}{2}{1}Legal values are: {3} - {4}{1}", 
                r_ObjectType, Environment.NewLine, Message, r_MinValue, r_MaxValue);

            return errorMessageBuilder.ToString();
        }
    }
}
