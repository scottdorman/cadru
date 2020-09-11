//------------------------------------------------------------------------------
// <copyright file="ExtendedNetworkInformation.cs"
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
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

using Cadru.Net.NetworkInformation;
using Cadru.Net.NetworkInformation.Interop;

namespace Cadru.Networking
{
    /// <summary>
    /// Provides information about a computer or computers on a domain.
    /// </summary>
    public static class ExtendedNetworkInformation
    {
        /// <summary>
        /// Gets an array of <see cref="ServerInfo"/> instances for all
        /// available domains.
        /// </summary>
        /// <returns>
        /// An array of <see cref="ServerInfo"/> instances for all available domains.
        /// </returns>
        [SecurityCritical]
        public static ServerInfo[] GetDomains()
        {
            return GetServerList(ServerTypes.AllDomains, null);
        }

        /// <summary>
        /// Gets the fully qualified hostname of the local computer.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing the fully qualified hostname of
        /// the local computer.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public async static Task<string> GetHostNameAsync()
        {
            string? hostName;

            try
            {
                var computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                var domainName = computerProperties.DomainName;

                if (String.IsNullOrEmpty(domainName) || String.Compare(domainName, "localdomain", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    hostName = computerProperties.HostName;
                }
                else
                {
                    hostName = String.Format(CultureInfo.CurrentUICulture, "{0}.{1}", computerProperties.HostName, computerProperties.DomainName);
                }
            }
            catch (NetworkInformationException)
            {
                try
                {
                    var hostInfo = await Dns.GetHostEntryAsync(Environment.MachineName);
                    hostName = hostInfo.HostName;
                }
                catch (SocketException)
                {
                    hostName = Environment.MachineName;
                }
            }

            return hostName ?? Environment.MachineName;
        }

        /// <summary>
        /// Gets the IP addresses of the local computer.
        /// </summary>
        /// <returns>
        /// An <see cref="IPAddress"/> array containing the IP addresses of the
        /// local computer.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public async static Task<IPAddress[]> GetIPAddressesAsync()
        {
            var ipAddress = Array.Empty<IPAddress>();

            try
            {
                var nics = NetworkInterface.GetAllNetworkInterfaces();
                if (nics != null && nics.Length >= 1)
                {
                    var adapter = nics[0];
                    var adapterProperties = adapter.GetIPProperties();
                    var uniCast = adapterProperties.UnicastAddresses;

                    if (uniCast != null && uniCast.Count > 0)
                    {
                        ipAddress = new IPAddress[uniCast.Count];
                        for (var i = 0; i < uniCast.Count; i++)
                        {
                            ipAddress[i] = uniCast[0].Address;
                        }
                    }
                    else
                    {
                        ipAddress = await GetIPAddressesFromDnsAsync();
                    }
                }
            }
            catch (NetworkInformationException)
            {
                ipAddress = await GetIPAddressesFromDnsAsync();
            }

            return ipAddress;
        }

        /// <summary>
        /// Gets a <see cref="ServerInfo"/> instance representing the local computer.
        /// </summary>
        /// <returns>
        /// A <see cref="ServerInfo"/> instance representing the local computer.
        /// </returns>
        public static ServerInfo GetServerInfo()
        {
            return GetServerInfoInternal(null);
        }

        /// <summary>
        /// Gets a <see cref="ServerInfo"/> instance representing the named computer.
        /// </summary>
        /// <param name="serverName">The name of the computer.</param>
        /// <returns>
        /// A <see cref="ServerInfo"/> instance representing the named computer.
        /// </returns>
        public static ServerInfo GetServerInfo(string serverName)
        {
            Contracts.Requires.NotNull(serverName, "serverName");

            if (!serverName.StartsWith(@"\\", StringComparison.OrdinalIgnoreCase))
            {
                serverName = String.Concat(@"\\", serverName);
            }

            return GetServerInfoInternal(serverName);
        }

        /// <summary>
        /// Gets an array of <see cref="ServerInfo"/> instances of the specified
        /// server type for the given domain.
        /// </summary>
        /// <param name="serverType">
        /// A bitwise combination of enumeration values that defines what server
        /// types to search.
        /// </param>
        /// <param name="domain">
        /// The name of the domain to search, or <see langword="null"/> to
        /// search the primary domain.
        /// </param>
        /// <returns>
        /// An array of <see cref="ServerInfo"/> instances of the specified
        /// server type for the given domain.
        /// </returns>
        [SecurityCritical]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        public static ServerInfo[] GetServerList(ServerTypes serverType, string? domain)
        {
            ServerInfo[] serverList;
            var pBuf = IntPtr.Zero;

            try
            {
                var entriesRead = 0;
                var totalEntries = 0;

                var result = SafeNativeMethods.NetServerEnum(null, 101, out pBuf, -1, ref entriesRead, ref totalEntries, (uint)serverType, domain, IntPtr.Zero);
                if (result != Constants.ERROR_SUCCESS && result != Constants.ERROR_MORE_DATA && entriesRead <= 0)
                {
                    throw new Win32Exception(result);
                }

                serverList = new ServerInfo[entriesRead];
                var ptr = pBuf.ToInt32();

                SERVER_INFO_101 serverInfo;
                for (var i = 0; i < entriesRead; i++)
                {
                    serverInfo = Marshal.PtrToStructure<SERVER_INFO_101>(new IntPtr(ptr));
                    ptr += Marshal.SizeOf(serverInfo);
                    serverList[i] = new ServerInfo(serverInfo);
                }
            }
            finally
            {
                FreeBuffer(ref pBuf);
            }

            return serverList;
        }

        private static void FreeBuffer(ref IntPtr buffer)
        {
            try
            {
                var result = SafeNativeMethods.NetApiBufferFree(buffer);
                if (result != Constants.ERROR_SUCCESS)
                {
                    throw new Win32Exception(result);
                }
            }
            finally
            {
                buffer = IntPtr.Zero;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private async static Task<IPAddress[]> GetIPAddressesFromDnsAsync()
        {
            IPAddress[] ipAddress;
            try
            {
                var hostInfo = await Dns.GetHostEntryAsync(await GetHostNameAsync());
                ipAddress = hostInfo.AddressList;
            }
            catch (SocketException)
            {
                ipAddress = new IPAddress[] { IPAddress.None };
            }

            return ipAddress;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        private static ServerInfo GetServerInfoInternal(string? serverName)
        {
            ServerInfo server;
            var pBuf = IntPtr.Zero;

            try
            {
                var result = SafeNativeMethods.NetServerGetInfo(serverName, 101, out pBuf);
                if (result != Constants.ERROR_SUCCESS && result != Constants.ERROR_MORE_DATA)
                {
                    throw new Win32Exception(result);
                }

                var ptr = pBuf.ToInt32();
                SERVER_INFO_101 serverInfo;
                serverInfo = Marshal.PtrToStructure<SERVER_INFO_101>(new IntPtr(ptr));
                server = new ServerInfo(serverInfo);
            }
            finally
            {
                FreeBuffer(ref pBuf);
            }

            return server;
        }
    }
}