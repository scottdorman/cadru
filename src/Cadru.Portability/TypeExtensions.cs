namespace Cadru.Portability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public static class TypeExtensions
    {
        public static bool IsEnum(this Type type)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().IsEnum;
#else
            return type.IsEnum;
#endif
        }

        public static bool IsGenericType(this Type type)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().IsGenericType;
#else
            return type.IsGenericType;
#endif
        }

        public static bool IsVisible(this Type type)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().IsVisible;
#else
            return type.IsVisible;
#endif
        }

        public static FieldInfo[] GetDeclaredFields(this Type type)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().DeclaredFields.ToArray();
#else
            return type.GetFields(Constants.DeclaredOnlyLookup);
#endif
        }

        public static FieldInfo GetDeclaredField(this Type type, string name)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().GetDeclaredField(name);
#else
            return type.GetField(name, Constants.DeclaredOnlyLookup);
#endif
        }

        public static IEnumerable<Type> GetInterfaces(this Type type)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().ImplementedInterfaces;
#else
            return type.GetInterfaces();
#endif
        }

        public static MethodInfo[] GetDeclaredMethods(this Type type)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().DeclaredMethods.ToArray();
#else
            return type.GetMethods(Constants.DeclaredOnlyLookup);
#endif
        }

        public static PropertyInfo[] GetDeclaredProperties(this Type type)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().DeclaredProperties.ToArray();
#else
            return type.GetProperties(Constants.DeclaredOnlyLookup);
#endif
        }

        public static PropertyInfo GetDeclaredProperty(this Type type, string name)
        {
#if INTROSPECTIONSHIM
            return type.GetTypeInfo().GetDeclaredProperty(name);
#else
            return type.GetProperty(name, Constants.DeclaredOnlyLookup);
#endif
        }
    }
}
