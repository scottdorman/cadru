//------------------------------------------------------------------------------
// <copyright file="ExtendedNetworkInformation.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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

namespace Cadru.Networking
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Runtime.InteropServices;
    using System.Security;
    using Cadru.Portability.InteropServices;
    using InteropServices;

    /// <summary>
    /// Provides information about a computer or computers on a domain.
    /// </summary>
    public static class ExtendedNetworkInformation
    {
        #region fields

        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties

        #endregion

        #region methods

        #region GetDomains
        /// <summary>
        /// Gets an array of <see cref="ServerInfo"/> instances for all
        /// available domains.
        /// </summary>
        /// <returns>An array of <see cref="ServerInfo"/> instances for all
        /// available domains.</returns>
        [SecurityCritical]
        public static ServerInfo[] GetDomains()
        {
            return GetServerList(ServerTypes.AllDomains, null);
        }
        #endregion

        #region HostName
        /// <summary>
        /// Gets the fully qualified hostname of the local computer.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the fully qualified hostname of the local computer.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        public static string GetHostName()
        {
            string hostName = Environment.MachineName;

            try
            {
                IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                string domainName = computerProperties.DomainName;

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
                    IPHostEntry hostInfo = Dns.GetHostEntry(System.Environment.MachineName);
                    hostName = hostInfo.HostName;
                }
                catch (System.Net.Sockets.SocketException)
                {
                    hostName = Environment.MachineName;
                }
            }

            return hostName;
        }
        #endregion

        #region GetServerInfo

        #region GetServerInfo()
        /// <summary>
        /// Gets a <see cref="ServerInfo"/> instance representing the local computer.
        /// </summary>
        /// <returns>A <see cref="ServerInfo"/> instance representing the local computer.</returns>
        public static ServerInfo GetServerInfo()
        {
            return GetServerInfoInternal(null);
        }
        #endregion

        #region GetServerInfo(string serverName)
        /// <summary>
        /// Gets a <see cref="ServerInfo"/> instance representing the named computer.
        /// </summary>
        /// <param name="serverName">The name of the computer.</param>
        /// <returns>A <see cref="ServerInfo"/> instance representing the named computer.</returns>
        public static ServerInfo GetServerInfo(string serverName)
        {
            Contracts.Requires.NotNull(serverName, "serverName");

            if (!serverName.StartsWith(@"\\", StringComparison.OrdinalIgnoreCase))
            {
                serverName = String.Concat(@"\\", serverName);
            }

            return GetServerInfoInternal(serverName);
        }
        #endregion

        #endregion

        #region GetServerList
        /// <summary>
        /// Gets an array of <see cref="ServerInfo"/> instances of the
        /// specified server type for the given domain.
        /// </summary>
        /// <param name="serverType">A bitwise combination of enumeration values
        /// that defines what server types to search. </param>
        /// <param name="domain">The name of the domain to search, or <see langword="null"/>
        /// to search the primary domain.</param>
        /// <returns>An array of <see cref="ServerInfo"/> instances of the
        /// specified server type for the given domain.</returns>
        [SecurityCritical]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        public static ServerInfo[] GetServerList(ServerTypes serverType, string domain)
        {
            ServerInfo[] serverList;
            IntPtr pBuf = IntPtr.Zero;

            try
            {
                int entriesRead = 0;
                int totalEntries = 0;

                int result = SafeNativeMethods.NetServerEnum(null, 101, out pBuf, -1, ref entriesRead, ref totalEntries, (uint)serverType, domain, IntPtr.Zero);
                if (result != Constants.ERROR_SUCCESS && result != Constants.ERROR_MORE_DATA && entriesRead <= 0)
                {
                    throw new Win32Exception(result);
                }

                serverList = new ServerInfo[entriesRead];
                int ptr = pBuf.ToInt32();

                SERVER_INFO_101 serverInfo;
                for (int i = 0; i < entriesRead; i++)
                {
                    serverInfo = MarshalShim.PtrToStructure<SERVER_INFO_101>(new IntPtr(ptr));
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
        #endregion

        #region IPAddress
        /// <summary>
        /// Gets the IP addresses of the local computer.
        /// </summary>
        /// <returns>An <see cref="IPAddress"/> array containing the IP addresses of the local computer.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        public static IPAddress[] GetIPAddresses()
        {
            IPAddress[] ipAddress = null;

            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                if (nics != null || nics.Length >= 1)
                {
                    NetworkInterface adapter = nics[0];
                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    UnicastIPAddressInformationCollection uniCast = adapterProperties.UnicastAddresses;

                    if (uniCast != null && uniCast.Count > 0)
                    {
                        ipAddress = new IPAddress[uniCast.Count];
                        for (int i = 0; i < uniCast.Count; i++)
                        {
                            ipAddress[i] = uniCast[0].Address;
                        }
                    }
                    else
                    {
                        ipAddress = GetIPAddressesFromDns();
                    }
                }
            }
            catch (NetworkInformationException)
            {
                ipAddress = GetIPAddressesFromDns();
            }

            return ipAddress;
        }
        #endregion

        #region GetServerInfoInternal
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        private static ServerInfo GetServerInfoInternal(string serverName)
        {
            ServerInfo server;
            IntPtr pBuf = IntPtr.Zero;

            try
            {
                int result = SafeNativeMethods.NetServerGetInfo(serverName, 101, out pBuf);
                if (result != Constants.ERROR_SUCCESS && result != Constants.ERROR_MORE_DATA)
                {
                    throw new Win32Exception(result);
                }

                int ptr = pBuf.ToInt32();
                SERVER_INFO_101 serverInfo;
                serverInfo = MarshalShim.PtrToStructure<SERVER_INFO_101>(new IntPtr(ptr));
                server = new ServerInfo(serverInfo);
            }
            finally
            {
                FreeBuffer(ref pBuf);
            }

            return server;
        }
        #endregion

        #region GetIPAddressesFromDns
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        private static IPAddress[] GetIPAddressesFromDns()
        {
            IPAddress[] ipAddress = null;

            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(ExtendedNetworkInformation.GetHostName());
                ipAddress = hostInfo.AddressList;
            }
            catch (System.Net.Sockets.SocketException)
            {
                ipAddress = new IPAddress[] { IPAddress.None };
            }

            return ipAddress;
        }
        #endregion

        #region FreeBuffer
        private static void FreeBuffer(ref IntPtr buffer)
        {
            try
            {
                int result = SafeNativeMethods.NetApiBufferFree(buffer);
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
        #endregion

        #endregion
    }
}