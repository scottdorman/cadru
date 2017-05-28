namespace Cadru.Data.Excel
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref=SafeConverter />
    /// </summary>
    public static class SafeConverter
    {
        public static object Convert(object value, Type desiredType, object defaultValue = null)
        {
            return CoerceValue(desiredType, value, defaultValue);
        }

        public static T Convert<T>(object value)
        {
            return (T)CoerceValue(typeof(T), value, default(T));
        }

        private static object CoerceValue(Type desiredType, object value, object defaultValue)
        {
            var returnValue = defaultValue;
            if (value != null)
            {
            }

            if (value == null)
                return defaultValue;
            var desiredTypeInfo = desiredType.GetTypeInfo();
            var valueType = value.GetType();
            if (desiredTypeInfo.IsAssignableFrom(valueType.GetTypeInfo()))
                return value;
            if (desiredTypeInfo.IsGenericType)
            {
                if (desiredType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    if (valueType == typeof(string) && string.IsNullOrEmpty(value.ToString()))
                        return null;
            }

            desiredType = GetNullableUnderlyingType(desiredType);
            try
            {
                if (desiredType == typeof(string))
                    return value.ToString();
                if (desiredType == typeof(bool) && Equals(value, "0"))
                    return false;
                if (desiredType == typeof(bool) && Equals(value, "1"))
                    return true;
                return System.Convert.ChangeType(value, desiredType);
            }
            catch
            {
                //var cnv = TypeDescriptor.GetConverter(desiredType);
                //if (cnv.CanConvertFrom(valueType))
                //    return cnv.ConvertFrom(value);
                return value;
            }
        }

        private static Type GetNullableUnderlyingType(Type type)
        {
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(type);
            return type;
        }
    }
}