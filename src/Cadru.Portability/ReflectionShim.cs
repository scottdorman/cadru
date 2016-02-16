#if !(INTROSPECTIONSHIM)
namespace System.Reflection
{
    public static class IntrospectionExtensionsShim
    {
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }
    }
}
#endif