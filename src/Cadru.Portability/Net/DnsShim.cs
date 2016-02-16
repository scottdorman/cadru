using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
#if WP80
using Microsoft.Phone.Net.NetworkInformation;
using System.Net;
#else
using System.Net;
#endif


namespace Cadru.Portability.InteropServices
{
    public static class DnsShim
    {
        //#if WP80
        //        public static async Task<IPEndPoint> GetHostEntryAsync(string hostNameOrAddress)
        //        {
        //            DeviceNetworkInformation.ResolveHostNameAsync(new DnsEndPoint(hostNameOrAddress, 0),
        //                (NameResolutionResult result) => result.IPEndPoints
        //        }
        //#endif

#if PCL
        public static Task<string> GetHostNameByDnsAsync(string hostNameOrAddress)
        {
            return Task.Factory.StartNew(() => EnvironmentShim.GetMachineName());
        }
#else
        public static async Task<string> GetHostNameByDnsAsync(string hostNameOrAddress)
        {
#if DNXCORE50
            return (await Dns.GetHostEntryAsync(hostNameOrAddress)).HostName;
#elif WP80 || WPA81 || SL50 || DOTNET5_1
            return await Task.Factory.StartNew(() => EnvironmentShim.GetMachineName());
#elif NET40
            return await Task.Factory.StartNew(() => Dns.GetHostEntry(hostNameOrAddress).HostName);
#else
            return await Task.FromResult(Dns.GetHostEntry(hostNameOrAddress).HostName);
#endif
        }
#endif
    }
}
