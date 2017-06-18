namespace Cadru.Data.Excel
{
    using Cadru.Extensions;
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class UnboxT<T>
    {
        /// <summary>
        /// Defines the Unbox = Create(typeof (T))
        /// </summary>
        internal static readonly Func<object, T> Unbox = Create(typeof(T));

        private static Func<object, T> Create(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsValueType)
            {
                if (typeInfo.IsNullable())
                {
                    return (Func<object, T>)typeof(UnboxT<T>).GetTypeInfo().DeclaredMethods.SingleOrDefault(m => m.Name == "NullableField" && m.IsStatic && !m.IsPublic).MakeGenericMethod(type.GenericTypeArguments[0]).CreateDelegate(typeof(Func<object, T>));
                }

                if (typeInfo.IsBoolean())
                {
                    return BooleanField;
                }

                if (typeInfo.IsDate() || typeInfo.IsDateOffset())
                {
                    return DateTimeField;
                }

                return ValueField;
            }

            return ReferenceField;
        }

        private static T ReferenceField(object value)
        {
            return ((DBNull.Value == value) ? default(T) : (T)Convert.ChangeType(value, typeof(T)));
        }

        private static T DateTimeField(object value)
        {
            if (DBNull.Value == value)
            {
                throw new InvalidCastException();
            }

            object result = null;
            if (DateTime.TryParse(value.ToString(), out DateTime parsedResult))
            {
                result = parsedResult;
            }
            else
            {
                if (Double.TryParse(value.ToString(), out double serialDateValue))
                {
#if NET45
                    result = DateTime.FromOADate(serialDateValue);
#else
                    var num = (long)((serialDateValue * 86400000.0) + ((serialDateValue >= 0.0) ? 0.5 : -0.5));
                    if (num < 0L)
                    {
                        num -= (num % 0x5265c00L) * 2L;
                    }

                    num += 0x3680b5e1fc00L;
                    num -= 62135596800000L;

                    result = new DateTime(num);

#endif
                }
            }

            return (T)result;
        }

        private static T BooleanField(object value)
        {
            if (DBNull.Value == value)
            {
                throw new InvalidCastException();
            }

            object result = false;
            if (value.ToString().TryParseAsBoolean(out bool temp))
            {
                result = temp;
            }

            return (T)result;
        }

        private static T ValueField(object value)
        {
            if (DBNull.Value == value)
            {
                throw new InvalidCastException();
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        private static Nullable<TElem> NullableField<TElem>(object value) where TElem : struct
        {
            if (DBNull.Value == value || value == null)
            {
                return default(TElem?);
            }

            return new Nullable<TElem>(UnboxT<TElem>.Unbox(value));
        }
    }
}