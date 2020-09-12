//------------------------------------------------------------------------------
// <copyright file="UnboxT.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using Cadru.Extensions;

namespace Cadru.Data
{
    internal static class UnboxT<T>
    {
        internal static readonly DateTime baseDate = new DateTime(1899, 12, 30);
        internal static readonly Func<object, T> Unbox = Create(typeof(T));

        private static T BooleanField(object value)
        {
            if (DBNull.Value == value)
            {
                throw new InvalidCastException();
            }

            object result = false;
            if (BooleanExtensions.TryParseAsBoolean(value?.ToString(), out var temp))
            {
                result = temp;
            }

            return (T)result;
        }

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

        private static T DateTimeField(object value)
        {
            if (DBNull.Value == value)
            {
                throw new InvalidCastException();
            }

            object? result;
            if (Double.TryParse(value.ToString(), out var serialDateValue))
            {
                try
                {
                    result = baseDate.AddDays(serialDateValue);
                }
                catch
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                DateTime.TryParse(value.ToString(), out var parsedResult);
                result = parsedResult;
            }

            return (T)result;
        }

        private static Nullable<TElem> NullableField<TElem>(object value) where TElem : struct
        {
            if (DBNull.Value == value || value == null)
            {
                return default(TElem?);
            }

            try
            {
                return new Nullable<TElem>(UnboxT<TElem>.Unbox(value));
            }
            catch
            {
                return default(TElem?);
            }
        }

        private static T ReferenceField(object value)
        {
            return (DBNull.Value == value) ? default! : (T)Convert.ChangeType(value, typeof(T));
        }

        private static T ValueField(object value)
        {
            if (DBNull.Value == value)
            {
                throw new InvalidCastException();
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}