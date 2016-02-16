namespace Cadru.Portability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public static class MemberInfoExtensions
    {
 #if INTROSPECTIONSHIM
        public static MethodInfo GetGetMethod(this PropertyInfo type)
        {
            return type.GetGetMethod();
        }
#endif

        public static object GetValue(this PropertyInfo type, object obj)
        {
            return type.GetValue(obj);
        }

        public static object GetValue(this PropertyInfo type, object obj, object[] index)
        {
            return type.GetValue(obj, index);
        }

#if INTROSPECTIONSHIM && !(DNX || WP)
        public static object[] GetCustomAttributes(this MemberInfo type, Type attributeType, bool inherit = true)
        {
            return type.CustomAttributes.Where(a => a.AttributeType == attributeType).ToArray();
        }
#endif

#if NET40
        public static T GetCustomAttribute<T>(this MemberInfo type, bool inherit = false) where T : Attribute
        {
            return (T)type.GetCustomAttributes(typeof(T), inherit).SingleOrDefault();
        }
#endif
    }
}
