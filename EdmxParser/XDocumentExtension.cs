using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace EdmxParser
{
    public static class XDocumentExtension
    {
        static TypeConverter converter;

        static XDocumentExtension()
        {
            converter = TypeDescriptor.GetConverter(typeof(string));
        }

        public static string AttributeValue(this XElement xElement, string name)
        {
            return xElement.Attribute(name).Value;
        }

        public static TOutput AttributeValue<TOutput>(this XElement xElement, string name)
        {
            
            var value = xElement.Attribute(name).Value;

            if (typeof(TOutput).Equals(typeof(bool)))
                return (TOutput)(object)Convert.ToBoolean(value);
            else if (typeof(TOutput).Equals(typeof(int)))
                return (TOutput)(object)Convert.ToInt32(value);
            else if (typeof(TOutput).Equals(typeof(long)))
                return (TOutput)(object)Convert.ToInt64(value);
            else if (typeof(TOutput).Equals(typeof(float)))
                return (TOutput)(object)Convert.ToSingle(value);
            else if (typeof(TOutput).Equals(typeof(double)))
                return (TOutput)(object)Convert.ToDouble(value);
            else if (typeof(TOutput).Equals(typeof(decimal)))
                return (TOutput)(object)Convert.ToDecimal(value);
            else if (typeof(TOutput).Equals(typeof(DateTime)))
                return (TOutput)(object)Convert.ToDateTime(value);

            return (TOutput)converter.ConvertTo(value, typeof(TOutput));
        }
    }
}
