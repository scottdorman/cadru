using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadru.Portability
{
    public static class EnvironmentShim
    {
        public static string GetMachineName()
        {
#if NET40 || NET45
            return Environment.MachineName;
//            return Environment.GetEnvironmentVariable("COMPUTERNAME");
#else
            return String.Empty;
#endif
        }
    }
}
