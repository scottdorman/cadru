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
                    //return (Converter<object, T>)Delegate.CreateDelegate(
                    //    typeof(Converter<object, T>),
                    //        typeof(UnboxT<T>).GetTypeInfo()
                    //            .DeclaredMethods.SingleOrDefault(m => m.Name == "NullableField" && m.IsStatic && !m.IsPublic)
                    //            .MakeGenericMethod(type.GenericTypeArguments[0]));
                }

                if (typeInfo.IsBoolean())
                {
                    return BooleanField;
                }
                return ValueField;
            }

            return ReferenceField;
        }

        private static T ReferenceField(object value)
        {
            return ((DBNull.Value == value) ? default(T) : (T)value);
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

            return (T)value;
        }

        private static Nullable<TElem> NullableField<TElem>(object value) where TElem : struct
        {
            if (DBNull.Value == value)
            {
                return default(Nullable<TElem>);
            }

            return new Nullable<TElem>((TElem)value);
        }
    }
}