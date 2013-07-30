//------------------------------------------------------------------------------
// <copyright file="FrameworkVersionDetection.cs" 
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
// http://support.microsoft.com/kb/318785
// http://msdn.microsoft.com/en-us/library/cc843122(v=vs.90).aspx
// http://download-codeplex.sec.s-msft.com/Download?ProjectName=wix&DownloadId=119157&Build=20172
// http://blogs.msdn.com/b/astebner/archive/2009/06/16/9763379.aspx
// http://blogs.msdn.com/astebner/archive/2006/08/02/687233.aspx
//
// Additional (historical) information can be found here:
// http://blogs.msdn.com/astebner/archive/2004/09/14/229802.aspx
// http://blogs.msdn.com/astebner/archive/2004/09/14/229574.aspx
// http://blogs.msdn.com/astebner/archive/2007/11/29/6608419.aspx
// http://blogs.msdn.com/astebner/archive/2006/11/09/clarification-about-net-framework-3-0-detection-registry-key-on-a-64-bit-os.aspx

namespace Cadru
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using Cadru.InteropServices;
    using Cadru.Properties;
    using Microsoft.Win32;

    /// <summary>
    /// Provides support for determining if a specific version of the .NET
    /// Framework runtime is installed and the service pack level for the
    /// runtime version.
    /// </summary>
    public static class FrameworkVersionDetection
    {
        #region fields

        // Constants representing registry key names and value names
        private const string Netfx10RegKeyName = "Software\\Microsoft\\.NETFramework\\Policy\\v1.0";
        private const string Netfx10RegKeyValue = "3705";
        private const string Netfx10SPxMSIRegKeyName = "Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}";
        private const string Netfx10SPxOCMRegKeyName = "Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}";
        private const string Netfx11RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322";
        private const string Netfx20RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727";
        private const string Netfx30RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup";
        private const string Netfx30SpRegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0";
        private const string Netfx30RegValueName = "InstallSuccess";
        private const string Netfx35RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5";
        private const string Netfx35ClientProfileRegKeyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\NET Framework Setup\\DotNetClient\\v3.5";
        
        // At this point, I don't know the correct registry key for the server core profile.
        private const string Netfx35ServerCoreProfileRegKeyName = "";

        private const string Netfx40RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v4";
        private const string Netfx40ClientProfileRegKeyName = Netfx40RegKeyName + "\\Client";
        private const string Netfx40FullProfileRegKeyName = Netfx40RegKeyName + "\\Full";
        private const string Netfx45RegKeyName = "Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full";

        private const string Netfx40SPxRegValueName = "Servicing";
        private const string NetfxStandardRegValueName = "Install";
        private const string NetfxStandrdSpxRegValueName = "SP";
        private const string NetfxStandardVersionRegValueName = "Version";
        private const string Netfx20PlusBuildRegValueName = "Increment";
        private const string Netfx35PlusBuildRegValueName = "Build";
        private const string Netfx30PlusWCFRegKeyName = Netfx30RegKeyName + "\\Windows Communication Foundation";
        private const string Netfx30PlusWPFRegKeyName = Netfx30RegKeyName + "\\Windows Presentation Foundation";
        private const string Netfx30PlusWFRegKeyName = Netfx30RegKeyName + "\\Windows Workflow Foundation";
        private const string Netfx30PlusWFPlusVersionRegValueName = "FileVersion";
        private const string CardSpaceServicesRegKeyName = "System\\CurrentControlSet\\Services\\idsvc";
        private const string CardSpaceServicesPlusImagePathRegName = "ImagePath";

        private const string NetfxInstallRootRegKeyName = "Software\\Microsoft\\.NETFramework";
        private const string NetFxInstallRootRegValueName = "InstallRoot";

        private const string Netfx10VersionString = "v1.0.3705";
        private const string Netfx11VersionString = "v1.1.4322";
        private const string Netfx20VersionString = "v2.0.50727";
        private const string Netfx40VersionString = "v4.0.30319";
        private const string NetfxMscorwks = "mscorwks.dll";
        private const string NetfxMscorlib = "mscorlib.dll";

        private static readonly Version Netfx10Version = new Version(1, 0, 3705, 0);
        private static readonly Version Netfx11Version = new Version(1, 1, 4322, 573);
        private static readonly Version Netfx20Version = new Version(2, 0, 50727, 42);
        private static readonly Version Netfx30Version = new Version(3, 0, 4506, 26);
        private static readonly Version Netfx35Version = new Version(3, 5, 21022, 8);
        private static readonly Version Netfx40Version = new Version(4, 0, 30319);
        private static readonly Version Netfx45Version = new Version(4, 5, 50709);
        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        #region GetExactVersion

        #region GetExactVersion(FrameworkVersion frameworkVersion)
        /// <overloads>
        /// Retrieves the exact version number for the specified .NET Framework or
        /// Foundation Library.
        /// </overloads>
        /// <summary>
        /// Retrieves the exact version number for the specified .NET Framework
        /// version.
        /// </summary>
        /// <param name="frameworkVersion">The .NET Framework whose version should be 
        /// retrieved.</param>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the specified .NET Framework version.
        /// If the specified .NET Framework version is not found, a 
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number.
        /// </returns>
        public static Version GetExactVersion(FrameworkVersion frameworkVersion)
        {
            Version version = new Version();

            switch (frameworkVersion)
            {
                case FrameworkVersion.Fx10:
                    version = GetNetfx10ExactVersion();
                    break;

                case FrameworkVersion.Fx11:
                    version = GetNetfx11ExactVersion();
                    break;

                case FrameworkVersion.Fx20:
                    version = GetNetfx20ExactVersion();
                    break;

                case FrameworkVersion.Fx30:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx30), NetfxStandardVersionRegValueName);
                    break;

                case FrameworkVersion.Fx35:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx35), NetfxStandardVersionRegValueName);
                    break;

                case FrameworkVersion.Fx35ClientProfile:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx35ClientProfile), NetfxStandardVersionRegValueName);
                    break;
                
                case FrameworkVersion.Fx35ServerCoreProfile:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx35ServerCoreProfile), NetfxStandardVersionRegValueName);
                    break;

                case FrameworkVersion.Fx40:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx40), NetfxStandardVersionRegValueName);
                    break;

                case FrameworkVersion.Fx40ClientProfile:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx40ClientProfile), NetfxStandardVersionRegValueName);
                    break;

                case FrameworkVersion.Fx45:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx45), NetfxStandardVersionRegValueName);
                    break;
            }

            return version;
        }
        #endregion

        #region GetExactVersion(WindowsFoundationLibrary foundationLibrary)
        /// <summary>
        /// Retrieves the exact version number for the specified .NET Framework
        /// Foundation Library.
        /// </summary>
        /// <param name="foundationLibrary">The Foundation Library whose version
        /// should be retrieved.</param>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the specified .NET Framework Foundation
        /// Library. If the specified .NET Framework Foundation Library is not
        /// found, a <see cref="Version"/> is returned that represents a 
        /// 0.0.0.0 version number.
        /// </returns>
        public static Version GetExactVersion(WindowsFoundationLibrary foundationLibrary)
        {
            Version version = new Version();

            switch (foundationLibrary)
            {
                case WindowsFoundationLibrary.CardSpace:
                    version = GetNetfx30CardSpaceExactVersion();
                    break;

                case WindowsFoundationLibrary.WCF:
                    version = GetNetfx30WCFExactVersion();
                    break;

                case WindowsFoundationLibrary.WF:
                    version = GetNetfx30WFExactVersion();
                    break;

                case WindowsFoundationLibrary.WPF:
                    version = GetNetfx30WPFExactVersion();
                    break;
            }

            return version;
        }
        #endregion

        #endregion

        #region GetServicePackLevel

        #region GetServicePackLevel(FrameworkVersion frameworkVersion)
        /// <overloads>
        /// Retrieves the service pack level for the specified .NET Framework or  
        /// Foundation Library.
        /// </overloads>
        /// <summary>
        /// Retrieves the service pack level for the specified .NET Framework
        /// version.
        /// </summary>
        /// <param name="frameworkVersion">The .NET Framework whose service pack 
        /// level should be retrieved.</param>
        /// <returns>An <see cref="Int32">integer</see> value representing
        /// the service pack level for the specified .NET Framework version. If
        /// the specified .NET Framework version is not found, -1 is returned.
        /// </returns>
        public static int GetServicePackLevel(FrameworkVersion frameworkVersion)
        {
            int servicePackLevel = -1;

            switch (frameworkVersion)
            {
                case FrameworkVersion.Fx10:
                    servicePackLevel = GetNetfx10SPLevel();
                    break;

                case FrameworkVersion.Fx11:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx11), NetfxStandrdSpxRegValueName);
                    break;

                case FrameworkVersion.Fx20:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx20), NetfxStandrdSpxRegValueName);
                    break;

                case FrameworkVersion.Fx30:
                    servicePackLevel = GetNetfxSPLevel(Netfx30SpRegKeyName, NetfxStandrdSpxRegValueName);
                    break;

                case FrameworkVersion.Fx35:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx35), NetfxStandrdSpxRegValueName);
                    break;

                case FrameworkVersion.Fx35ClientProfile:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx35ClientProfile), NetfxStandrdSpxRegValueName);
                    break;
                
                case FrameworkVersion.Fx35ServerCoreProfile:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx35ServerCoreProfile), NetfxStandrdSpxRegValueName);
                    break;

                case FrameworkVersion.Fx40:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx40), Netfx40SPxRegValueName);
                    break;

                case FrameworkVersion.Fx40ClientProfile:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx40ClientProfile), Netfx40SPxRegValueName);
                    break;

                case FrameworkVersion.Fx45:
                    servicePackLevel = GetNetfxSPLevel(GetRegistryKey(FrameworkVersion.Fx45), Netfx40SPxRegValueName);
                    break;
            }

            return servicePackLevel;
        }
        #endregion

        #region GetServicePackLevel(WindowsFoundationLibrary foundationLibrary)
        /// <summary>
        /// Retrieves the service pack level for the specified .NET Framework
        /// Foundation Library.
        /// </summary>
        /// <param name="foundationLibrary">The Foundation Library whose service pack 
        /// level should be retrieved.</param>
        /// <returns>An <see cref="Int32">integer</see> value representing
        /// the service pack level for the specified .NET Framework Foundation
        /// Library. If the specified .NET Framework Foundation Library is not
        /// found, -1 is returned.
        /// </returns>
        public static int GetServicePackLevel(WindowsFoundationLibrary foundationLibrary)
        {
            int servicePackLevel = -1;

            switch (foundationLibrary)
            {
                case WindowsFoundationLibrary.CardSpace:
                    servicePackLevel = GetNetfx30CardSpaceSPLevel();
                    break;

                case WindowsFoundationLibrary.WCF:
                    servicePackLevel = GetNetfx30WCFSPLevel();
                    break;

                case WindowsFoundationLibrary.WF:
                    servicePackLevel = GetNetfx30WFSPLevel();
                    break;

                case WindowsFoundationLibrary.WPF:
                    servicePackLevel = GetNetfx30WPFSPLevel();
                    break;
            }

            return servicePackLevel;
        }
        #endregion

        #endregion

        #region IsInstalled

        #region IsInstalled(FrameworkVersion frameworkVersion)
        /// <overloads>
        /// Determines if the specified .NET Framework or Foundation Library is 
        /// installed on the local computer.
        /// </overloads>
        /// <summary>
        /// Determines if the specified .NET Framework version is installed
        /// on the local computer.
        /// </summary>
        /// <param name="frameworkVersion">The version of the .NET Framework to test.
        /// </param>
        /// <returns><see langword="true"/> if the specified .NET Framework
        /// version is installed; otherwise <see langword="false"/>.</returns>
        public static bool IsInstalled(FrameworkVersion frameworkVersion)
        {
            bool ret = false;

            switch (frameworkVersion)
            {
                case FrameworkVersion.Fx10:
                    ret = IsNetfx10Installed();
                    break;

                case FrameworkVersion.Fx11:
                    ret = IsNetfx11Installed();
                    break;

                case FrameworkVersion.Fx20:
                    ret = IsNetfx20Installed();
                    break;

                case FrameworkVersion.Fx30:
                    ret = IsNetfx30Installed();
                    break;

                case FrameworkVersion.Fx35:
                case FrameworkVersion.Fx35ClientProfile:
                case FrameworkVersion.Fx35ServerCoreProfile:
                    ret = IsNetfx35Installed(frameworkVersion);
                    break;

                case FrameworkVersion.Fx40:
                case FrameworkVersion.Fx40ClientProfile:
                    ret = IsNetfx40Installed(frameworkVersion);
                    break;

                case FrameworkVersion.Fx45:
                    ret = IsNetfx45Installed();
                    break;
            }

            return ret;
        }
        #endregion

        #region IsInstalled(WindowsFoundationLibrary foundationLibrary)
        /// <summary>
        /// Determines if the specified .NET Framework Foundation Library is
        /// installed on the local computer.
        /// </summary>
        /// <param name="foundationLibrary">The Foundation Library to test.
        /// </param>
        /// <returns><see langword="true"/> if the specified .NET Framework
        /// Foundation Library is installed; otherwise <see langword="false"/>.</returns>
        public static bool IsInstalled(WindowsFoundationLibrary foundationLibrary)
        {
            bool ret = false;

            switch (foundationLibrary)
            {
                case WindowsFoundationLibrary.CardSpace:
                    ret = IsNetfx30CardSpaceInstalled();
                    break;

                case WindowsFoundationLibrary.WCF:
                    ret = IsNetfx30WCFInstalled();
                    break;

                case WindowsFoundationLibrary.WF:
                    ret = IsNetfx30WFInstalled();
                    break;

                case WindowsFoundationLibrary.WPF:
                    ret = IsNetfx30WPFInstalled();
                    break;
            }

            return ret;
        }
        #endregion

        #endregion

        #region CheckFxVersion
        /// <summary>
        /// Retrieves the .NET Framework version number from the registry
        /// and validates that it is not a pre-release version number.
        /// </summary>
        /// <param name="frameworkVersion">The version of the .NET Framework to test.</param>
        /// <returns><see langword="true"/> if the build number is greater than the 
        /// requested version; otherwise <see langword="false"/>.
        /// </returns>
        private static bool CheckFxVersion(FrameworkVersion frameworkVersion)
        {
            bool valid = false;
            Version version = new Version();
            Version min = new Version();
            Version max = new Version();

            switch (frameworkVersion)
            {
                case FrameworkVersion.Fx10:
                    version = GetNetfx10ExactVersion();
                    min = Netfx10Version;
                    max = Netfx10Version;
                    break;
                
                case FrameworkVersion.Fx11:
                    version = GetNetfx11ExactVersion();
                    min = Netfx11Version;
                    max = Netfx11Version;
                    break;
                
                case FrameworkVersion.Fx20:
                    version = GetNetfx20ExactVersion();
                    min = Netfx20Version;
                    max = Netfx20Version;
                    break;
                
                case FrameworkVersion.Fx30:
                    version = GetNetfxExactVersion(Netfx30RegKeyName, NetfxStandardVersionRegValueName);
                    min = Netfx20Version;
                    max = Netfx30Version;
                    break;
                
                case FrameworkVersion.Fx35:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx35), NetfxStandardVersionRegValueName);
                    min = Netfx20Version;
                    max = Netfx35Version;
                    break;
                
                case FrameworkVersion.Fx35ClientProfile:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx35ClientProfile), NetfxStandardVersionRegValueName);
                    min = Netfx20Version;
                    max = Netfx35Version;
                    break;

                case FrameworkVersion.Fx35ServerCoreProfile:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx35ServerCoreProfile), NetfxStandardVersionRegValueName);
                    min = Netfx20Version;
                    max = Netfx35Version;
                    break;

                case FrameworkVersion.Fx40:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx40), NetfxStandardVersionRegValueName);
                    min = Netfx40Version;
                    max = Netfx40Version;
                    break;

                case FrameworkVersion.Fx40ClientProfile:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx40ClientProfile), NetfxStandardVersionRegValueName);
                    min = Netfx40Version;
                    max = Netfx40Version;
                    break;

                case FrameworkVersion.Fx45:
                    version = GetNetfxExactVersion(GetRegistryKey(FrameworkVersion.Fx45), NetfxStandardVersionRegValueName);
                    min = Netfx40Version;
                    max = Netfx45Version;
                    break;
            }

            Version coreVersion;
            valid = GetCoreFrameworkVersion(frameworkVersion, out coreVersion) ? (version >= max && coreVersion >= min) : version >= max;

            return valid;
        }
        #endregion

        #region GetCoreFrameworkVersion
        /// <summary>
        /// Gets a value which indicates if the version of the specified .NET
        /// Framework could be determined.
        /// </summary>
        /// <param name="frameworkVersion">The version of the .NET Framework to test.</param>
        /// <param name="version">The version number of the Framework.</param>
        /// <returns><see langword="true"/> if the version could be determined; otherwise <see langword="false"/>.
        /// </returns>
        private static bool GetCoreFrameworkVersion(FrameworkVersion frameworkVersion, out Version version)
        {
            bool valid = false;
            string installPath = GetCorePath(frameworkVersion);
            FileVersionInfo fvi = null;
            version = null;
            if (!String.IsNullOrEmpty(installPath))
            {
                fvi = FileVersionInfo.GetVersionInfo(installPath);
                if (fvi != null)
                {
                    version = new Version(fvi.ProductVersion);
                    valid = true;
                }
            }

            return valid;
        }
        #endregion

        #region GetInstallRoot
        /// <summary>
        /// Gets the installation root path for the .NET Framework.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the installation root 
        /// path for the .NET Framework.</returns>
        private static string GetInstallRoot()
        {
            string installRoot = String.Empty;
            if (!GetRegistryValue(RegistryHive.LocalMachine, NetfxInstallRootRegKeyName, NetFxInstallRootRegValueName, RegistryValueKind.String, out installRoot))
            {
                throw new DirectoryNotFoundException(Resources.ApplicationExcpetion_UnableToDetermineInstallRoot);
            }

            return installRoot;
        }
        #endregion

        #region GetCorePath
        /// <summary>
        /// Gets the path to a DLL which represents a core component of the .NET Framework.
        /// </summary>
        /// <param name="frameworkVersion">The version of the .NET Framework to test.</param>
        /// <returns>The fully qualified path to the core component DLL for the specified .NET
        /// Framework.
        /// </returns>
        private static string GetCorePath(FrameworkVersion frameworkVersion)
        {
            string ret = String.Empty;

            switch (frameworkVersion)
            {
                case FrameworkVersion.Fx10:
                    ret = Path.Combine(GetInstallRoot(), Netfx10VersionString, NetfxMscorlib);
                    break;

                case FrameworkVersion.Fx11:
                    ret = Path.Combine(GetInstallRoot(), Netfx11VersionString, NetfxMscorlib);
                    break;

                case FrameworkVersion.Fx20:
                case FrameworkVersion.Fx30:
                case FrameworkVersion.Fx35:
                case FrameworkVersion.Fx35ClientProfile:
                case FrameworkVersion.Fx35ServerCoreProfile:
                    ret = Path.Combine(GetInstallRoot(), Netfx20VersionString, NetfxMscorlib);
                    break;

                case FrameworkVersion.Fx40:
                case FrameworkVersion.Fx40ClientProfile:
                case FrameworkVersion.Fx45:
                    ret = Path.Combine(GetInstallRoot(), Netfx40VersionString, NetfxMscorlib);
                    break;
            }

            return ret;
        }
        #endregion

        #region GetNetfxSPLevel functions

        #region GetNetfx10SPLevel
        /// <summary>
        /// Detects the service pack level for the .NET Framework 1.0.
        /// </summary>
        /// <returns>An <see cref="Int32"/> representing the service pack 
        /// level for the .NET Framework.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://blogs.msdn.com/astebner/archive/2004/09/14/229802.aspx 
        /// to determine what service pack for the .NET Framework 1.0 is 
        /// installed on the machine.
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", 
            MessageId = "System.Int32.TryParse(System.String,System.Int32@)", 
            Justification = "In this case, we're already defaulting the out parameter but want to make sure the Parse isn't going to throw an exception.")]
        private static int GetNetfx10SPLevel()
        {
            bool foundKey = false;
            int servicePackLevel = -1;
            string regValue;

            if (IsTabletOrMediaCenter())
            {
                foundKey = GetRegistryValue(RegistryHive.LocalMachine, Netfx10SPxOCMRegKeyName, NetfxStandardVersionRegValueName, RegistryValueKind.String, out regValue);
            }
            else
            {
                foundKey = GetRegistryValue(RegistryHive.LocalMachine, Netfx10SPxMSIRegKeyName, NetfxStandardVersionRegValueName, RegistryValueKind.String, out regValue);
            }

            if (foundKey)
            {
                // This registry value should be of the format
                // #,#,#####,# where the last # is the SP level
                // Try to parse off the last # here
                int index = regValue.LastIndexOf(',');
                if (index > 0)
                {
                    Int32.TryParse(regValue.Substring(index + 1), out servicePackLevel);
                }
            }

            return servicePackLevel;
        }
        #endregion

        #region GetNetfxSPLevel
        /// <summary>
        /// Detects the service pack level for a version of .NET Framework.
        /// </summary>
        /// <param name="key">The registry key name.</param>
        /// <param name="value">The registry value name.</param>
        /// <returns>An <see cref="Int32"/> representing the service pack 
        /// level for the .NET Framework.</returns>
        private static int GetNetfxSPLevel(string key, string value)
        {
            int regValue = 0;

            // We can only get -1 if the .NET Framework is not
            // installed or there was some kind of error retrieving
            // the data from the registry
            int servicePackLevel = -1;

            if (GetRegistryValue(RegistryHive.LocalMachine, key, value, RegistryValueKind.DWord, out regValue))
            {
                servicePackLevel = regValue;
            }

            return servicePackLevel;
        }
        #endregion

        #endregion

        #region GetNetfxExactVersion functions

        #region GetNetfx10ExactVersion
        /// <summary>
        /// Retrieves the exact version number for the .NET Framework 1.0.
        /// </summary>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the .NET Framework 1.0 or a
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number if the .NET Framework 1.0 is not found.
        /// </returns>
        private static Version GetNetfx10ExactVersion()
        {
            bool foundKey = false;
            Version version = new Version();
            string regValue;

            if (IsTabletOrMediaCenter())
            {
                foundKey = GetRegistryValue(RegistryHive.LocalMachine, Netfx10SPxOCMRegKeyName, NetfxStandardVersionRegValueName, RegistryValueKind.String, out regValue);
            }
            else
            {
                foundKey = GetRegistryValue(RegistryHive.LocalMachine, Netfx10SPxMSIRegKeyName, NetfxStandardVersionRegValueName, RegistryValueKind.String, out regValue);
            }

            if (foundKey)
            {
                // This registry value should be of the format
                // #,#,#####,# where the last # is the SP level
                // Try to parse off the last # here
                int index = regValue.LastIndexOf(',');
                if (index > 0)
                {
                    string[] tokens = regValue.Substring(0, index).Split(',');
                    if (tokens.Length == 3)
                    {
                        version = new Version(Convert.ToInt32(tokens[0], NumberFormatInfo.InvariantInfo), Convert.ToInt32(tokens[1], NumberFormatInfo.InvariantInfo), Convert.ToInt32(tokens[2], NumberFormatInfo.InvariantInfo));
                    }
                }
            }

            return version;
        }
        #endregion

        #region GetNetfx11ExactVersion
        /// <summary>
        /// Retrieves the exact version number for the .NET Framework 1.1.
        /// </summary>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the .NET Framework 1.1 or a
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number if the .NET Framework 1.1 is not found.
        /// </returns>
        private static Version GetNetfx11ExactVersion()
        {
            int regValue = 0;

            // We can only get -1 if the .NET Framework is not
            // installed or there was some kind of error retrieving
            // the data from the registry
            Version version = new Version();

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx11RegKeyName, NetfxStandardRegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    // In the strict sense, we are cheating here, but the registry key name itself
                    // contains the version number.
                    string[] tokens = Netfx11RegKeyName.Split(new string[] { "NDP\\v" }, StringSplitOptions.None);
                    if (tokens.Length == 2)
                    {
                        version = new Version(tokens[1]);
                    }
                }
            }

            return version;
        }
        #endregion

        #region GetNetfx20ExactVersion
        /// <summary>
        /// Retrieves the exact version number for the .NET Framework 2.0.
        /// </summary>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the .NET Framework 2.0 or a
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number if the .NET Framework 2.0 is not found.
        /// </returns>
        private static Version GetNetfx20ExactVersion()
        {
            string regValue = String.Empty;

            // We can only get -1 if the .NET Framework is not
            // installed or there was some kind of error retrieving
            // the data from the registry
            Version version = new Version();

            // If we have a Version registry value, use that.
            try
            {
                version = GetNetfxExactVersion(Netfx20RegKeyName, NetfxStandardVersionRegValueName);
            }
            catch (IOException)
            {
                // If we hit an exception here, the Version registry key probably doesn't exist so try
                // to get the version based on the registry key name itself.
                if (GetRegistryValue(RegistryHive.LocalMachine, Netfx20RegKeyName, Netfx20PlusBuildRegValueName, RegistryValueKind.String, out regValue))
                {
                    if (!String.IsNullOrEmpty(regValue))
                    {
                        string[] versionTokens = Netfx20RegKeyName.Split(new string[] { "NDP\\v" }, StringSplitOptions.None);
                        if (versionTokens.Length == 2)
                        {
                            string[] tokens = versionTokens[1].Split('.');
                            if (tokens.Length == 3)
                            {
                                version = new Version(Convert.ToInt32(tokens[0], NumberFormatInfo.InvariantInfo), Convert.ToInt32(tokens[1], NumberFormatInfo.InvariantInfo), Convert.ToInt32(tokens[2], NumberFormatInfo.InvariantInfo), Convert.ToInt32(regValue, NumberFormatInfo.InvariantInfo));
                            }
                        }
                    }
                }
            }

            return version;
        }
        #endregion

        #region GetNetfxExactVersion
        /// <summary>
        /// Retrieves the .NET Framework version number from the registry.
        /// </summary>
        /// <param name="key">The registry key name.</param>
        /// <param name="value">The registry value name.</param>
        /// <returns>A <see cref="Version"/> that represents the .NET 
        /// Framework version or a <see cref="Version"/> is returned
        /// that represents a 0.0.0.0 version number if the .NET Framework
        /// version is not found.</returns>
        private static Version GetNetfxExactVersion(string key, string value)
        {
            string regValue = String.Empty;

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            Version version = new Version();

            if (GetRegistryValue(RegistryHive.LocalMachine, key, value, RegistryValueKind.String, out regValue))
            {
                if (!String.IsNullOrEmpty(regValue))
                {
                    version = new Version(regValue);
                }
            }

            return version;
        }
        #endregion

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

            using (RegistryKey baseKey = RegistryKey.OpenRemoteBaseKey(hive, String.Empty))
            {
                if (baseKey != null)
                {
                    using (RegistryKey registryKey = baseKey.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
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
                                // We can get here if the registry key doesn't exist. In that case,
                                // we don't actually want to throw an exception and instead return
                                // false. The upstream callers of this method will handle this
                                // appropriately and return a valid response.
                            }
                        }
                    }
                }
            }

            return success;
        }
        #endregion

        #region GetRegistryKey
        /// <summary>
        /// Gets the registry key for the given .NET Framework version and (optionally) profile.
        /// </summary>
        /// <param name="frameworkVersion">The version of the .NET Framework.</param>
        /// <returns>A string representing the registry key for the given .NET Framework version and profile.</returns>
        private static string GetRegistryKey(FrameworkVersion frameworkVersion)
        {
            string regKeyName = String.Empty;
            switch (frameworkVersion)
            {
                case FrameworkVersion.Fx10:
                    regKeyName = Netfx10RegKeyName;
                    break;

                case FrameworkVersion.Fx11:
                    regKeyName = Netfx11RegKeyName;
                    break;

                case FrameworkVersion.Fx20:
                    regKeyName = Netfx20RegKeyName;
                    break;

                case FrameworkVersion.Fx30:
                    regKeyName = Netfx30RegKeyName;
                    break;

                case FrameworkVersion.Fx35:
                    regKeyName = Netfx35RegKeyName;
                    break;

                case FrameworkVersion.Fx35ClientProfile:
                    regKeyName = Netfx35ClientProfileRegKeyName;
                    break;

                case FrameworkVersion.Fx35ServerCoreProfile:
                    regKeyName = Netfx35ServerCoreProfileRegKeyName;
                    break;

                case FrameworkVersion.Fx40:
                    regKeyName = Netfx40FullProfileRegKeyName;
                    break;

                case FrameworkVersion.Fx40ClientProfile:
                    regKeyName = Netfx40ClientProfileRegKeyName;
                    break;

                case FrameworkVersion.Fx45:
                    regKeyName = Netfx45RegKeyName;
                    break;
            }

            return regKeyName;
        }
        #endregion

        #region IsNetfxInstalled functions

        #region IsNetfx10Installed
        /// <summary>
        /// Detects if the .NET Framework 1.0 is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 1.0 is 
        /// installed; otherwise <see langword="false"/>.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://msdn.microsoft.com/library/ms994349.aspx to determine
        /// whether the .NET Framework 1.0 is installed on the machine.
        /// </devdoc>
        private static bool IsNetfx10Installed()
        {
            bool found = false;
            string regValue = string.Empty;
            found = GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx10), Netfx10RegKeyValue, RegistryValueKind.String, out regValue);

            return found && CheckFxVersion(FrameworkVersion.Fx10);
        }
        #endregion

        #region IsNetfx11Installed
        /// <summary>
        /// Detects if the .NET Framework 1.1 is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 1.1 is 
        /// installed; otherwise <see langword="false"/>.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://msdn.microsoft.com/library/ms994339.aspx to determine
        /// whether the .NET Framework 1.1 is installed on the machine.
        /// </devdoc>
        private static bool IsNetfx11Installed()
        {
            bool found = false;
            int regValue = 0;

            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx11), NetfxStandardRegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            return found && CheckFxVersion(FrameworkVersion.Fx11);
        }
        #endregion

        #region IsNetfx20Installed
        /// <summary>
        /// Detects if the .NET Framework 2.0 is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 2.0 is 
        /// installed; otherwise <see langword="false"/>.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://msdn.microsoft.com/library/aa480243.aspx to determine
        /// whether the .NET Framework 2.0 is installed on the machine.
        /// </devdoc>
        private static bool IsNetfx20Installed()
        {
            bool found = false;
            int regValue = 0;

            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx20), NetfxStandardRegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            return found && CheckFxVersion(FrameworkVersion.Fx20);
        }
        #endregion

        #region IsNetfx30Installed
        /// <summary>
        /// Detects if the .NET Framework 3.0 is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 3.0 is 
        /// installed; otherwise <see langword="false"/>.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://msdn.microsoft.com/library/aa964979.aspx to determine
        /// whether the .NET Framework 3.0 is installed on the machine.
        /// </devdoc>
        private static bool IsNetfx30Installed()
        {
            bool found = false;
            int regValue = 0;

            // The .NET Framework 3.0 is an add-in that installs on top of
            // the .NET Framework 2.0, so validate that both 2.0 and 3.0
            // are installed.
            if (IsNetfx20Installed())
            {
                // Check that the InstallSuccess registry value exists and equals 1.
                if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx30), Netfx30RegValueName, RegistryValueKind.DWord, out regValue))
                {
                    if (regValue == 1)
                    {
                        found = true;
                    }
                }
            }

            // A system with a pre-release version of the .NET Framework 3.0 can have
            // the InstallSuccess value. As an added verification, check the
            // version number listed in the registry.
            return found && CheckFxVersion(FrameworkVersion.Fx30);
        }
        #endregion

        #region IsNetfx35Installed
        /// <summary>
        /// Detects if the .NET Framework 3.5 is installed.
        /// </summary>
        /// <param name="frameworkVersion">The version of the .NET Framework.</param>
        /// <returns><see langword="true"/> if the .NET Framework 3.5 is 
        /// installed; otherwise <see langword="false"/>.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://msdn.microsoft.com/library/cc160716.aspx to determine
        /// whether the .NET Framework 3.5 is installed on the machine.
        /// Also uses the method described at 
        /// http://blogs.msdn.com/astebner/archive/2008/07/13/8729636.aspx.
        /// </devdoc>
        private static bool IsNetfx35Installed(FrameworkVersion frameworkVersion)
        {
            bool found = false;
            int regValue = 0;

            // The .NET Framework 3.0 is an add-in that installs on top of
            // the .NET Framework 2.0 and 3.0, so validate that 2.0, 3.0,
            // and 3.5 are installed.
            if (IsNetfx20Installed() && IsNetfx30Installed())
            {
                // Check that the Install registry value exists and equals 1.
                if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(frameworkVersion), NetfxStandardRegValueName, RegistryValueKind.DWord, out regValue))
                {
                    if (regValue == 1)
                    {
                        found = true;
                    }
                }
            }

            // A system with a pre-release version of the .NET Framework 3.5 can have
            // the Install value. As an added verification, check the
            // version number listed in the registry.
            return found && CheckFxVersion(frameworkVersion);
        }
        #endregion

        #region IsNetfx40Installed
        /// <summary>
        /// Detects if the specified .NET Framework 4.0 profile is installed.
        /// </summary>
        /// <param name="frameworkVersion">The version of the .NET Framework.</param>
        /// <returns><see langword="true"/> if the specified .NET Framework
        /// 4.0 profile is installed; otherwise <see langword="false"/>.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://msdn.microsoft.com/library/ee942965(v=VS.100).aspx to
        /// determine whether the specified .NET Framework 4.0 profile is
        /// installed on the machine. Also uses the method described at 
        /// http://blogs.msdn.com/astebner/archive/2008/07/13/8729636.aspx.
        /// </devdoc>
        private static bool IsNetfx40Installed(FrameworkVersion frameworkVersion)
        {
            bool found = false;
            int regValue = 0;

            // The .NET Framework 4.0 introduced a new CLR version, so it
            // isn't dependent on other .NET Framework versions being
            // installed.
            // Check that the Install registry value exists and equals 1.
            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(frameworkVersion), NetfxStandardRegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            // A system with a pre-release version of the .NET Framework 4 can have
            // the Install value. As an added verification, check the
            // version number listed in the registry.
            return found && CheckFxVersion(frameworkVersion);
        }
        #endregion

        #region IsNetfx45Installed
        /// <summary>
        /// Detects if the specified .NET Framework 4.5 profile is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the specified .NET Framework
        /// 4.5 profile is installed; otherwise <see langword="false"/>.</returns>
        /// <devdoc>Uses the detection method recommended at
        /// http://msdn.microsoft.com/library/ee942965(v=VS.100).aspx to
        /// determine whether the specified .NET Framework 4.5 profile is
        /// installed on the machine. Also uses the method described at 
        /// http://blogs.msdn.com/astebner/archive/2008/07/13/8729636.aspx.
        /// </devdoc>
        private static bool IsNetfx45Installed()
        {
            bool found = false;
            int regValue = 0;

            // The .NET Framework 4.5 introduced a new CLR version, but it's
            // an in-place update of the .NET Framework 4, so checking to see
            // if an earlier Framework version is installed isn't necessary.
            // Check that the Install registry value exists and equals 1.
            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx45), NetfxStandardRegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            // A system with a pre-release version of the .NET Framework 4 can have
            // the Install value. As an added verification, check the
            // version number listed in the registry.
            return found && CheckFxVersion(FrameworkVersion.Fx45);
        }
        #endregion

        #endregion

        #region IsTabletOrMediaCenter
        /// <summary>
        /// Determines if the operating system is Windows Media Center or Windows XP Tablet.
        /// </summary>
        /// <returns><see langword="true"/> if the operating system is Windows Media Center or Windows XP Tablet;
        /// otherwise, <see langword="false"/>.</returns>
        private static bool IsTabletOrMediaCenter()
        {
            return (SafeNativeMethods.GetSystemMetrics(SystemMetric.SM_TABLETPC) != 0) || (SafeNativeMethods.GetSystemMetrics(SystemMetric.SM_MEDIACENTER) != 0);
        }
        #endregion

        #region WindowsFounationLibrary functions

        #region CardSpace

        #region IsNetfx30CardSpaceInstalled
        /// <summary>
        /// Detects if the .NET Framework 3.0 CardSpace is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 3.0 CardSpace is 
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsNetfx30CardSpaceInstalled()
        {
            bool found = false;
            string regValue = String.Empty;

            if (GetRegistryValue(RegistryHive.LocalMachine, CardSpaceServicesRegKeyName, CardSpaceServicesPlusImagePathRegName, RegistryValueKind.ExpandString, out regValue))
            {
                if (!String.IsNullOrEmpty(regValue))
                {
                    found = true;
                }
            }

            return found;
        }
        #endregion

        #region GetNetfx30CardSpaceSPLevel
        /// <summary>
        /// Detects the service pack level for the .NET Framework 3.0 CardSpace.
        /// </summary>
        /// <returns>An <see cref="Int32"/> representing the service pack 
        /// level for the .NET Framework 3.0 CardSpace.</returns>
        /// <remarks>It doesn't appear that the Windows Foundation Libraries are
        /// serviced independently of the .NET Framework 3.0, so this returns
        /// the .NET Framework 3.0 service pack level.</remarks>
        private static int GetNetfx30CardSpaceSPLevel()
        {
            return GetServicePackLevel(FrameworkVersion.Fx30);
        }
        #endregion

        #region GetNetfx30CardSpaceExactVersion
        /// <summary>
        /// Retrieves the exact version number for the .NET Framework 3.0 CardSpace.
        /// </summary>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the .NET Framework 3.0 CardSpace or a
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number if the .NET Framework 3.0 CardSpace is not found.
        /// </returns>
        private static Version GetNetfx30CardSpaceExactVersion()
        {
            string regValue = String.Empty;

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            Version version = new Version();

            if (GetRegistryValue(RegistryHive.LocalMachine, CardSpaceServicesRegKeyName, CardSpaceServicesPlusImagePathRegName, RegistryValueKind.ExpandString, out regValue))
            {
                if (!String.IsNullOrEmpty(regValue))
                {
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(regValue.Trim('"'));
                    int index = fileVersionInfo.FileVersion.IndexOf(' ');
                    version = new Version(fileVersionInfo.FileVersion.Substring(0, index));
                }
            }

            return version;
        }
        #endregion

        #endregion

        #region Windows Communication Foundation

        #region IsNetfx30WCFInstalled
        /// <summary>
        /// Detects if the .NET Framework 3.0 WCF is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 3.0 WCF is 
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsNetfx30WCFInstalled()
        {
            bool found = false;
            int regValue = 0;

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWCFRegKeyName, Netfx30RegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            return found;
        }
        #endregion

        #region GetNetfx30WCFSPLevel
        /// <summary>
        /// Detects the service pack level for the .NET Framework 3.0 WCF.
        /// </summary>
        /// <returns>An <see cref="Int32"/> representing the service pack 
        /// level for the .NET Framework 3.0 WCF.</returns>
        /// <remarks>It doesn't appear that the Windows Foundation Libraries are
        /// serviced independently of the .NET Framework 3.0, so this returns
        /// the .NET Framework 3.0 service pack level.</remarks>
        private static int GetNetfx30WCFSPLevel()
        {
            return GetServicePackLevel(FrameworkVersion.Fx30);
        }
        #endregion

        #region GetNetfx30WCFExactVersion
        /// <summary>
        /// Retrieves the exact version number for the .NET Framework 3.0 WCF.
        /// </summary>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the .NET Framework 3.0 WCF or a
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number if the .NET Framework 3.0 WCF is not found.
        /// </returns>
        private static Version GetNetfx30WCFExactVersion()
        {
            string regValue = String.Empty;

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            Version version = new Version();

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWCFRegKeyName, NetfxStandardVersionRegValueName, RegistryValueKind.String, out regValue))
            {
                if (!String.IsNullOrEmpty(regValue))
                {
                    version = new Version(regValue);
                }
            }

            return version;
        }
        #endregion

        #endregion

        #region Windows Presentation Foundation

        #region IsNetfx30WPFInstalled
        /// <summary>
        /// Detects if the .NET Framework 3.0 WPF is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 3.0 WPF is 
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsNetfx30WPFInstalled()
        {
            bool found = false;
            int regValue = 0;

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWPFRegKeyName, Netfx30RegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            return found;
        }
        #endregion

        #region GetNetfx30WPFSPLevel
        /// <summary>
        /// Detects the service pack level for the .NET Framework 3.0 WOF.
        /// </summary>
        /// <returns>An <see cref="Int32"/> representing the service pack 
        /// level for the .NET Framework 3.0 WPF.</returns>
        /// <remarks>It doesn't appear that the Windows Foundation Libraries are
        /// serviced independently of the .NET Framework 3.0, so this returns
        /// the .NET Framework 3.0 service pack level.</remarks>
        private static int GetNetfx30WPFSPLevel()
        {
            return GetServicePackLevel(FrameworkVersion.Fx30);
        }
        #endregion

        #region GetNetfx30WPFExactVersion
        /// <summary>
        /// Retrieves the exact version number for the .NET Framework 3.0 WPF.
        /// </summary>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the .NET Framework 3.0 WPF or a
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number if the .NET Framework 3.0 WPF is not found.
        /// </returns>
        private static Version GetNetfx30WPFExactVersion()
        {
            string regValue = String.Empty;

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            Version version = new Version();

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWPFRegKeyName, NetfxStandardVersionRegValueName, RegistryValueKind.String, out regValue))
            {
                if (!String.IsNullOrEmpty(regValue))
                {
                    version = new Version(regValue);
                }
            }

            return version;
        }
        #endregion

        #endregion

        #region Windows Workflow Foundation

        #region IsNetfx30WFInstalled
        /// <summary>
        /// Detects if the .NET Framework 3.0 WF is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 3.0 WF is 
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsNetfx30WFInstalled()
        {
            bool found = false;
            int regValue = 0;

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWFRegKeyName, Netfx30RegValueName, RegistryValueKind.DWord, out regValue))
            {
                if (regValue == 1)
                {
                    found = true;
                }
            }

            return found;
        }
        #endregion

        #region GetNetfx30WFSPLevel
        /// <summary>
        /// Detects the service pack level for the .NET Framework 3.0 WF.
        /// </summary>
        /// <returns>An <see cref="Int32"/> representing the service pack 
        /// level for the .NET Framework 3.0 WF.</returns>
        /// <remarks>It doesn't appear that the Windows Foundation Libraries are
        /// serviced independently of the .NET Framework 3.0, so this returns
        /// the .NET Framework 3.0 service pack level.</remarks>
        private static int GetNetfx30WFSPLevel()
        {
            return GetServicePackLevel(FrameworkVersion.Fx30);
        }
        #endregion

        #region GetNetfx30WFExactVersion
        /// <summary>
        /// Retrieves the exact version number for the .NET Framework 3.0 WF.
        /// </summary>
        /// <returns>A <see cref="Version">version</see> representing
        /// the exact version number for the .NET Framework 3.0 WF or a
        /// <see cref="Version"/> is returned that represents a 0.0.0.0 version
        /// number if the .NET Framework 3.0 WF is not found.
        /// </returns>
        private static Version GetNetfx30WFExactVersion()
        {
            string regValue = String.Empty;

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            Version version = new Version();

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWFRegKeyName, Netfx30PlusWFPlusVersionRegValueName, RegistryValueKind.String, out regValue))
            {
                if (!String.IsNullOrEmpty(regValue))
                {
                    version = new Version(regValue);
                }
            }

            return version;
        }
        #endregion

        #endregion

        #endregion

        #endregion
    }
}
