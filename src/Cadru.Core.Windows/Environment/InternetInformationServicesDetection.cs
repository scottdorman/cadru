//------------------------------------------------------------------------------
// <copyright file="InternetInformationServicesDetection.cs"
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
//
// The methods used are based on the information available here:
// http://www.iis.net/learn/install/installing-iis-7/discover-installed-components
// http://technet.microsoft.com/en-us/library/cc780409(v=WS.10).aspx
//
// Additional (historical) information can be found here:
// http://technet2.microsoft.com/WindowsServer/en/library/5b36c13b-c72e-4488-8bbe-7e4228911c381033.mspx?mfr=true
// http://geekswithblogs.net/sdorman/archive/2007/03/01/107732.aspx
// http://blogs.iis.net:80/chrisad/archive/2006/09/01/Detecting-if-IIS-is-installed_2E002E002E00_.aspx

namespace Cadru
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Microsoft.Win32;

    /// <summary>
    /// Provides support for determining if a specific version of the .NET
    /// Framework runtime is installed and the service pack level for the
    /// runtime version.
    /// </summary>
    public static class InternetInformationServicesDetection
    {
        #region fields

        private const string IISRegKeyName = "Software\\Microsoft\\InetStp";
        private const string IISRegKeyValue = "MajorVersion";
        private const string IISRegKeyMinorVersionValue = "MinorVersion";
        private const string IISComponentRegKeyName = IISRegKeyName + "\\Components";
        private const string IISLegacyComponentRegKeyName = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Setup\\Oc Manager\\Subcomponents";
        private const string Netfx11RegKeyName = "Software\\Microsoft\\ASP.NET\\1.1.4322.0";
        private const string Netfx20RegKeyName = "Software\\Microsoft\\ASP.NET\\2.0.50727.0";
        private const string Netfx40RegKeyName = "Software\\Microsoft\\ASP.NET\\4.0.30319.0";
        private const string NetRegKeyValue = "DllFullPath";

        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties

        #endregion

        #region methods

        #region IsAspRegistered
        /// <summary>
        /// Determines if ASP is registered with Internet Information
        /// Services (IIS) on the local computer.
        /// </summary>
        /// <returns><see langword="true"/> if ASP is registered; otherwise
        /// <see langword="false"/>.</returns>
        public static bool IsAspRegistered()
        {
            return IsInstalled(InternetInformationServicesSubcomponent.ASP);
        }
        #endregion

        #region IsAspNetRegistered
        /// <summary>
        /// Determines if the version of ASP.NET is registered with Internet
        /// Information Services (IIS) on the local computer.
        /// </summary>
        /// <param name="frameworkVersion">One of the
        /// <see cref="FrameworkVersion"/> values.</param>
        /// <returns><see langword="true"/> if the specified ASP.NET version
        /// is registered; otherwise <see langword="false"/>.</returns>
        public static bool IsAspNetRegistered(FrameworkVersion frameworkVersion)
        {
            bool ret = false;

            switch (frameworkVersion)
            {
                case FrameworkVersion.Fx10:
                    ret = IsAspNet10Registered();
                    break;

                case FrameworkVersion.Fx11:
                    ret = IsAspNet11Registered();
                    break;

                case FrameworkVersion.Fx20:
                case FrameworkVersion.Fx30:
                case FrameworkVersion.Fx35:
                    ret = IsAspNet20Registered();
                    break;

                case FrameworkVersion.Fx35ClientProfile:
                    ret = false;
                    break;

                case FrameworkVersion.Fx35ServerCoreProfile:
                    ret = IsAspNet20Registered();
                    break;

                case FrameworkVersion.Fx40:
                    ret = IsAspNet40Registered();
                    break;

                case FrameworkVersion.Fx40ClientProfile:
                    ret = false;
                    break;

                case FrameworkVersion.Fx45:
                    ret = IsAspNet40Registered();
                    break;
            }

            return ret;
        }
        #endregion

        #region IsInstalled

        #region IsInstalled(InternetInformationServicesVersion iisVersion)
        /// <summary>
        /// Determines if the specified version of Internet Information
        /// Services (IIS) is installed on the local computer.
        /// </summary>
        /// <param name="iisVersion">One of the
        /// <see cref="InternetInformationServicesVersion"/> values.</param>
        /// <returns><see langword="true"/> if the specified Internet
        /// Information Services version is installed; otherwise
        /// <see langword="false"/>.</returns>
        public static bool IsInstalled(InternetInformationServicesVersion iisVersion)
        {
            bool ret = false;
            Version installedVersion = GetInstalledVersion();

            switch (iisVersion)
            {
                case InternetInformationServicesVersion.IIS4:
                    ret = installedVersion.Major == 4;
                    break;

                case InternetInformationServicesVersion.IIS5:
                    ret = installedVersion.Major == 5 && installedVersion.Minor == 0;
                    break;

                case InternetInformationServicesVersion.IIS51:
                    ret = installedVersion.Major == 5 && installedVersion.Minor == 1;
                    break;

                case InternetInformationServicesVersion.IIS6:
                    ret = installedVersion.Major == 6;
                    break;

                case InternetInformationServicesVersion.IIS7:
                    ret = installedVersion.Major == 7;
                    break;

                case InternetInformationServicesVersion.IIS8:
                    ret = installedVersion.Major == 8;
                    break;
            }

            return ret;
        }
        #endregion

        #region IsInstalled(InternetInformationServicesComponent subcomponent)
        /// <summary>
        /// Determines if the specified Internet Information Services (IIS)
        /// subcomponent is installed on the local computer.
        /// </summary>
        /// <param name="subcomponent">One of the
        /// <see cref="InternetInformationServicesSubcomponent"/> values.</param>
        /// <returns><see langword="true"/> if the specified Internet
        /// Information Services subcomponent is installed; otherwise
        /// <see langword="false"/>.</returns>
        /// <remarks>Subcomponents only apply to IIS versions 6 and earlier.</remarks>
        public static bool IsInstalled(InternetInformationServicesSubcomponent subcomponent)
        {
            bool ret = false;
            int majorVersion = GetInstalledVersion().Major;

            if (majorVersion < 7)
            {
                switch (subcomponent)
                {
                    case InternetInformationServicesSubcomponent.Common:
                        ret = IsSubcomponentInstalled("iis_common");
                        break;

                    case InternetInformationServicesSubcomponent.ASP:
                        ret = IsSubcomponentInstalled("iis_asp");
                        break;

                    case InternetInformationServicesSubcomponent.FTP:
                        ret = IsSubcomponentInstalled("iis_ftp");
                        break;

                    case InternetInformationServicesSubcomponent.ManagementConsole:
                        ret = IsSubcomponentInstalled("iis_inetmgr");
                        break;

                    case InternetInformationServicesSubcomponent.InternetDataConnector:
                        ret = IsSubcomponentInstalled("iis_internetdataconnector");
                        break;

                    case InternetInformationServicesSubcomponent.NNTP:
                        ret = IsSubcomponentInstalled("iis_nntp");
                        break;

                    case InternetInformationServicesSubcomponent.ServerSideIncludes:
                        ret = IsSubcomponentInstalled("iis_serversideincludes");
                        break;

                    case InternetInformationServicesSubcomponent.SMTP:
                        ret = IsSubcomponentInstalled("iis_smtp");
                        break;

                    case InternetInformationServicesSubcomponent.WebDAV:
                        ret = IsSubcomponentInstalled("iis_webdav");
                        break;

                    case InternetInformationServicesSubcomponent.WWW:
                        ret = IsSubcomponentInstalled("iis_www");
                        break;

                    case InternetInformationServicesSubcomponent.RemoteAdmin:
                        ret = IsSubcomponentInstalled("sakit_web");
                        break;

                    case InternetInformationServicesSubcomponent.BitsISAPI:
                        ret = IsSubcomponentInstalled("BitsServerExtensionsISAPI");
                        break;

                    case InternetInformationServicesSubcomponent.Bits:
                        ret = IsSubcomponentInstalled("BitsServerExtensionsManager");
                        break;

                    case InternetInformationServicesSubcomponent.FrontPageExtensions:
                        ret = IsSubcomponentInstalled("fp_extensions");
                        break;

                    case InternetInformationServicesSubcomponent.InternetPrinting:
                        ret = IsSubcomponentInstalled("inetprint ");
                        break;

                    case InternetInformationServicesSubcomponent.TSWebClient:
                        ret = IsSubcomponentInstalled("TSWebClient ");
                        break;
                }
            }

            return ret;
        }
        #endregion

        #region IsInstalled(InternetInformationServicesFeature feature)
        /// <summary>
        /// Determines if the specified Internet Information Services (IIS)
        /// feature is installed and enabled on the local computer.
        /// </summary>
        /// <param name="feature">One of the
        /// <see cref="InternetInformationServicesFeature"/> values.</param>
        /// <returns><see langword="true"/> if the specified Internet
        /// Information Services feature is installed; otherwise
        /// <see langword="false"/>.</returns>
        /// <remarks>Features only apply to IIS versions 7 and later.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity",
            Justification = "This method appears to be complex because of it's length, but it's almost all just a switch statement which then calls out to other methods to do the work.")]
        public static bool IsInstalled(InternetInformationServicesFeature feature)
        {
            bool ret = false;
            int majorVersion = GetInstalledVersion().Major;

            if (majorVersion >= 7)
            {
                switch (feature)
                {
                    case InternetInformationServicesFeature.ApplicationInitialization:
                        ret = IsFeatureInstalled("AppWarmUp");
                        break;

                    case InternetInformationServicesFeature.NetFxExtensibility:
                        ret = IsFeatureInstalled("NetFxExtensibility", "NetFxEnvironment");
                        break;

                    case InternetInformationServicesFeature.NetFxExtensibility45:
                        ret = IsFeatureInstalled("NetFxExtensibility45");
                        break;

                    case InternetInformationServicesFeature.ASP:
                        ret = IsFeatureInstalled("ASP", "ASPBinaries");
                        break;

                    case InternetInformationServicesFeature.AspNet:
                        ret = IsFeatureInstalled("ASPNET");
                        break;

                    case InternetInformationServicesFeature.AspNet45:
                        ret = IsFeatureInstalled("ASPNET45");
                        break;

                    case InternetInformationServicesFeature.CGI:
                        ret = IsFeatureInstalled(new string[] { "CGI", "CGIBinaries", "FastCgi", "FastCgiBinaries" });
                        break;

                    case InternetInformationServicesFeature.ISAPIExtensions:
                        ret = IsFeatureInstalled("ISAPIExtensions", "ISAPIExtensionsBinaries");
                        break;

                    case InternetInformationServicesFeature.ISAPIFilters:
                        ret = IsFeatureInstalled("ISAPIFilter", "ISAPIFilterBinaries");
                        break;

                    case InternetInformationServicesFeature.ServerSideIncludes:
                        ret = IsFeatureInstalled("ServerSideInclude", "ServerSideIncludeBinaries");
                        break;

                    case InternetInformationServicesFeature.WebSockets:
                        ret = IsFeatureInstalled("WebSockets");
                        break;

                    case InternetInformationServicesFeature.DefaultDocument:
                        ret = IsFeatureInstalled("DefaultDocument", "DefaultDocumentBinaries");
                        break;

                    case InternetInformationServicesFeature.DirectoryBrowsing:
                        ret = IsFeatureInstalled("DirectoryBrowse", "DirectoryBrowseBinaries");
                        break;

                    case InternetInformationServicesFeature.HttpErrors:
                        ret = IsFeatureInstalled("HttpErrors", "HttpErrorsBinaries");
                        break;

                    case InternetInformationServicesFeature.HttpRedirection:
                        ret = IsFeatureInstalled("HttpRedirect", "HttpRedirectBinaries");
                        break;

                    case InternetInformationServicesFeature.StaticContent:
                        ret = IsFeatureInstalled("StaticContent", "StaticContentBinaries");
                        break;

                    case InternetInformationServicesFeature.WebDAV:
                        ret = IsFeatureInstalled("WebDAV", "WebDAVBinaries");
                        break;

                    case InternetInformationServicesFeature.Webserver:
                        ret = IsFeatureInstalled(new string[] { "W3SVC", "CachingBase", "CachingBaseBinaries", "Caching", "HttpCache", "HttpCacheBinaries" });
                        break;

                    case InternetInformationServicesFeature.CustomLogging:
                        ret = IsFeatureInstalled("CustomLogging", "CustomLoggingBinaries");
                        break;

                    case InternetInformationServicesFeature.HttpLogging:
                        ret = IsFeatureInstalled("HttpLogging", "HttpLoggingBinaries");
                        break;

                    case InternetInformationServicesFeature.ODBCLogging:
                        ret = IsFeatureInstalled("ODBCLogging", "ODBCLoggingBinaries");
                        break;

                    case InternetInformationServicesFeature.RequestMonitor:
                        ret = IsFeatureInstalled("RequestMonitor", "RequestMonitorBinaries");
                        break;

                    case InternetInformationServicesFeature.Tracing:
                        ret = IsFeatureInstalled("HttpTracing", "HttpTracingBinaries");
                        break;

                    case InternetInformationServicesFeature.LoggingTools:
                        ret = IsFeatureInstalled("LoggingLibraries");
                        break;

                    case InternetInformationServicesFeature.DynamicContentCompression:
                        ret = IsFeatureInstalled("HttpCompressionDynamic", "HttpCompressionDynamicBinaries");
                        break;

                    case InternetInformationServicesFeature.StaticContentCompression:
                        ret = IsFeatureInstalled("HttpCompressionStatic", "HttpCompressionStaticBinaries");
                        break;

                     case InternetInformationServicesFeature.Authorization:
                        ret = IsFeatureInstalled("Authorization", "AuthorizationBinaries");
                        break;

                    case InternetInformationServicesFeature.BasicAuthentication:
                        ret = IsFeatureInstalled("BasicAuthentication", "BasicAuthenticationBinaries");
                        break;

                    case InternetInformationServicesFeature.ClientCertificateMappingAuthentication:
                        ret = IsFeatureInstalled("ClientCertificateMappingAuthentication", "ClientCertificateMappingAuthenticationBinaries");
                        break;

                    case InternetInformationServicesFeature.CentralizedSSLCertificateSupport:
                        ret = IsFeatureInstalled("CertProvider");
                        break;

                    case InternetInformationServicesFeature.DigestAuthentication:
                        ret = IsFeatureInstalled("DigestAuthentication", "DigestAuthenticationBinaries");
                        break;

                    case InternetInformationServicesFeature.IISCertificateMappingAuthentication:
                        ret = IsFeatureInstalled("IISCertificateMappingAuthentication", "IISCertificateMappingAuthenticationBinaries");
                        break;

                    case InternetInformationServicesFeature.IPSecurity:
                        ret = IsFeatureInstalled("IPSecurity", "IPSecurityBinaries");
                        break;

                    case InternetInformationServicesFeature.RequestFiltering:
                        ret = IsFeatureInstalled("RequestFiltering", "RequestFilteringBinaries");
                        break;

                    case InternetInformationServicesFeature.WindowsAuthentication:
                        ret = IsFeatureInstalled("WindowsAuthentication", "WindowsAuthenticationBinaries");
                        break;

                   case InternetInformationServicesFeature.ManagementConsole:
                        ret = IsFeatureInstalled("ManagementConsole", "WASConfigurationAPI");
                        break;

                    case InternetInformationServicesFeature.ManagementScriptingTools:
                        ret = IsFeatureInstalled("ManagementScriptingTools", "WASConfigurationAPI");
                        break;

                    case InternetInformationServicesFeature.ManagementService:
                        ret = IsFeatureInstalled("AdminService", "WASConfigurationAPI");
                        break;

                    case InternetInformationServicesFeature.MetabaseCompatibility:
                        ret = IsFeatureInstalled("Metabase");
                        break;

                    case InternetInformationServicesFeature.WMICompatibility:
                        ret = IsFeatureInstalled("WMICompatibility", "Metabase");
                        break;

                    case InternetInformationServicesFeature.LegacyScripts:
                        ret = IsFeatureInstalled("LegacyScripts");
                        break;

                    case InternetInformationServicesFeature.LegacySnapin:
                        ret = IsFeatureInstalled(new string[] { "LegacySnapin", "ADSICompatability", "Metabase" });
                        break;

                    case InternetInformationServicesFeature.FTP:
                        ret = IsFeatureInstalled(majorVersion >= 8 ? "FTPSvc" : "FTPServer");
                        break;

                    case InternetInformationServicesFeature.FTPExtensibility:
                        ret = IsFeatureInstalled("FTPExtensibility");
                        break;
                }
            }

            return ret;
        }
        #endregion

        #endregion

        #region GetInstalledVersion
        /// <summary>
        /// Gets the installed IIS version.
        /// </summary>
        /// <returns>A <see cref="Version"/> representing the installed IIS
        /// version or a <see cref="Version"/> representing 0.0.0.0 if
        /// IIS is not installed.</returns>
        private static Version GetInstalledVersion()
        {
            int majorVersion = -1;
            int minorVersion = -1;
            Version version = new Version(0,0,0,0);

            if (GetRegistryValue(RegistryHive.LocalMachine, IISRegKeyName, IISRegKeyValue, RegistryValueKind.DWord, out majorVersion))
            {
                GetRegistryValue(RegistryHive.LocalMachine, IISRegKeyName, IISRegKeyMinorVersionValue, RegistryValueKind.DWord, out minorVersion);
                version = new Version(majorVersion, minorVersion);
            }

            return version;
        }
        #endregion

        #region GetRegistryValue
        /// <summary>
        /// Gets a value which indicates if the specified registry value was found.
        /// </summary>
        /// <typeparam name="T">The type of the stored registry value.</typeparam>
        /// <param name="hive">The top-level registry hive to open.</param>
        /// <param name="key">The specified sub key to open.</param>
        /// <param name="value">The name of the value to retrieve.</param>
        /// <param name="kind">The registry data type of the specified value.</param>
        /// <param name="data">The data associated with the registry hive,
        /// key, and value name.</param>
        /// <returns><see langword="true"/> if the registry value was found;
        /// otherwise, <see langword="false"/>.</returns>
        private static bool GetRegistryValue<T>(RegistryHive hive, string key, string value, RegistryValueKind kind, out T data)
        {
            bool success = false;
            data = default(T);

            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
            {
                if (baseKey != null)
                {
#if DNXCORE50
                    using (RegistryKey registryKey = baseKey.OpenSubKey(key, System.Security.AccessControl.RegistryRights.EnumerateSubKeys))
#else
                    using (RegistryKey registryKey = baseKey.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
#endif
                    {
                        if (registryKey != null)
                        {
                            try
                            {
                                // If the key was opened, try to retrieve the value.
                                RegistryValueKind kindFound = registryKey.GetValueKind(value);
                                if (kindFound == kind)
                                {
                                    object regValue = registryKey.GetValue(value, null);
                                    if (regValue != null)
                                    {
                                        data = (T)Convert.ChangeType(regValue, typeof(T), CultureInfo.InvariantCulture);
                                        success = true;
                                    }
                                }
                            }
                            catch (IOException)
                            {
                                // The registry value doesn't exist. Since the
                                // value doesn't exist we have to assume that
                                // the component isn't installed and return
                                // false and leave the data param as the
                                // default value.
                            }
                        }
                    }
                }
            }

            return success;
        }
        #endregion

        #region IsAspNetRegistered functions

        #region IsAspNet10Registered
        /// <summary>
        /// Detects if ASP.NET 1.0 is registered with IIS.
        /// </summary>
        /// <returns><see langword="true"/> if ASP.NET 1.0 is
        /// registered; otherwise <see langword="false"/>.</returns>
        private static bool IsAspNet10Registered()
        {
            // TODO: Determine how to detect ASP.NET 1.0
            return false;
        }
        #endregion

        #region IsAspNet11Registered
        /// <summary>
        /// Detects if ASP.NET 1.1 is registered with IIS.
        /// </summary>
        /// <returns><see langword="true"/> if ASP.NET 1.1 is
        /// registered; otherwise <see langword="false"/>.</returns>
        private static bool IsAspNet11Registered()
        {
            string regValue = string.Empty;
            return GetRegistryValue(RegistryHive.LocalMachine, Netfx11RegKeyName, NetRegKeyValue, RegistryValueKind.String, out regValue);
        }
        #endregion

        #region IsAspNet20Registered
        /// <summary>
        /// Detects if ASP.NET 2.0 is registered with IIS.
        /// </summary>
        /// <returns><see langword="true"/> if ASP.NET 2.0 is
        /// registered; otherwise <see langword="false"/>.</returns>
        private static bool IsAspNet20Registered()
        {
            string regValue = string.Empty;
            return GetRegistryValue(RegistryHive.LocalMachine, Netfx20RegKeyName, NetRegKeyValue, RegistryValueKind.String, out regValue);
        }
        #endregion

        #region IsAspNet40Registered
        /// <summary>
        /// Detects if ASP.NET 4.0 is registered with IIS.
        /// </summary>
        /// <returns><see langword="true"/> if ASP.NET 4.0 is
        /// registered; otherwise <see langword="false"/>.</returns>
        private static bool IsAspNet40Registered()
        {
            string regValue = string.Empty;
            return GetRegistryValue(RegistryHive.LocalMachine, Netfx40RegKeyName, NetRegKeyValue, RegistryValueKind.String, out regValue);
        }
        #endregion

        #endregion

        #region IsFeatureInstalled
        /// <summary>
        /// Detects if the specified feature is installed.
        /// </summary>
        /// <param name="featureKey">The registry value representing the feature to test.</param>
        /// <returns><see langword="true"/> if the feature is
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsFeatureInstalled(string featureKey)
        {
            bool found = false;
            int regValue = -1;

            if (GetRegistryValue(RegistryHive.LocalMachine, IISComponentRegKeyName, featureKey, RegistryValueKind.DWord, out regValue))
            {
                found = regValue == 1;
            }

            return found;
        }

        /// <summary>
        /// Detects if the specified features are installed.
        /// </summary>
        /// <param name="featureKey1">The registry value representing the first feature to test.</param>
        /// <param name="featureKey2">The registry value representing the second feature to test.</param>
        /// <returns><see langword="true"/> if both of the features are
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsFeatureInstalled(string featureKey1, string featureKey2)
        {
            int regValue = -1;
            bool[] found = { false, false };

            if (GetRegistryValue(RegistryHive.LocalMachine, IISComponentRegKeyName, featureKey1, RegistryValueKind.DWord, out regValue))
            {
                found[0] = regValue == 1;
            }

            if (GetRegistryValue(RegistryHive.LocalMachine, IISComponentRegKeyName, featureKey2, RegistryValueKind.DWord, out regValue))
            {
                found[1] = regValue == 1;
            }

            return found.All(v => v == true);
        }

        /// <summary>
        /// Detects if the specified features are installed.
        /// </summary>
        /// <param name="featureKeys">An array of feature registry keys to test.</param>
        /// <returns><see langword="true"/> if all of the features are
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsFeatureInstalled(string[] featureKeys)
        {
            int regValue = -1;
            bool[] found = new bool[featureKeys.Length];
            for (int i = 0; i < featureKeys.Length; i++)
            {
                if (GetRegistryValue(RegistryHive.LocalMachine, IISComponentRegKeyName, featureKeys[i], RegistryValueKind.DWord, out regValue))
                {
                    found[i] = regValue == 1;
                }
            }

            return found.All(v => v == true);
        }
        #endregion

        #region IsSubcomponentInstalled
        /// <summary>
        /// Detects if the specified subcomponent is installed.
        /// </summary>
        /// <param name="subcomponent">The registry value representing the subcomponent to test.</param>
        /// <returns><see langword="true"/> if the subcomponent is
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsSubcomponentInstalled(string subcomponent)
        {
            bool found = false;
            int regValue = -1;

            if (GetRegistryValue(RegistryHive.LocalMachine, IISLegacyComponentRegKeyName, subcomponent, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            return found;
        }
        #endregion

       #endregion
    }
}
