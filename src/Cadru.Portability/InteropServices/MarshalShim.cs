using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Cadru.Portability.InteropServices
{
    public static class MarshalShim
    {
        public static T PtrToStructure<T>(IntPtr ptr)
        {
#if NET40 || NET45 || DOTNET5_1 || WP80
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
#elif PCL || SL50
            throw new PlatformNotSupportedException();
#else
            return Marshal.PtrToStructure<T>(ptr);
#endif
        }

        public static int SizeOf<T>()
        {
#if NET40 || NET45 || DOTNET5_1 || SL50 || WP80
            return Marshal.SizeOf(typeof(T));
#elif PCL
            throw new PlatformNotSupportedException();
#else
            return Marshal.SizeOf<T>();
#endif
        }
    }
}
