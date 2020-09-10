//------------------------------------------------------------------------------
// <copyright file="FrameworkVersionDetection.cs"
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

namespace Cadru.Environment
{
    using Cadru.Environment.InteropServices;
    using Cadru.Environment.Resources;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Security.AccessControl;

    /// <summary>
    /// Provides support for determining if a specific version of the .NET
    /// Framework runtime is installed and the service pack level for the
    /// runtime version.
    /// </summary>
    public static class FrameworkVersionDetection
    {
        #region fields

        // Constants representing registry key names and value names
        private const string NetfxRegKeyRoot = "Software\\Microsoft\\NET Framework Setup\\NDP";

        private const string Netfx10RegKeyName = "Software\\Microsoft\\.NETFramework\\Policy\\v1.0";
        private const string Netfx10RegKeyValue = "3705";
        private const string Netfx10SPxMSIRegKeyName = "Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}";
        private const string Netfx10SPxOCMRegKeyName = "Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}";

        private const string Netfx11RegKeyName = NetfxRegKeyRoot + "\\v1.1.4322";

        private const string Netfx20RegKeyName = NetfxRegKeyRoot + "\\v2.0.50727";
        private const string Netfx20PlusBuildRegValueName = "Increment";

        private const string Netfx30RegKeyName = NetfxRegKeyRoot + "\\v3.0\\Setup";
        private const string Netfx30SpRegKeyName = NetfxRegKeyRoot + "\\v3.0";
        private const string Netfx30RegValueName = "InstallSuccess";
        private const string Netfx30PlusWCFRegKeyName = Netfx30RegKeyName + "\\Windows Communication Foundation";
        private const string Netfx30PlusWPFRegKeyName = Netfx30RegKeyName + "\\Windows Presentation Foundation";
        private const string Netfx30PlusWFRegKeyName = Netfx30RegKeyName + "\\Windows Workflow Foundation";
        private const string Netfx30PlusWFPlusVersionRegValueName = "FileVersion";
        private const string CardSpaceServicesRegKeyName = "System\\CurrentControlSet\\Services\\idsvc";
        private const string CardSpaceServicesPlusImagePathRegName = "ImagePath";

        private const string Netfx35RegKeyName = NetfxRegKeyRoot + "\\NDP\\v3.5";
        private const string Netfx35ClientProfileRegKeyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\NET Framework Setup\\DotNetClient\\v3.5";
        // At this point, I don't know the correct registry key for the server core profile.
        private const string Netfx35ServerCoreProfileRegKeyName = "";
        private const string Netfx35PlusBuildRegValueName = "Build";

        private const string Netfx40RegKeyName = NetfxRegKeyRoot + "\\v4";
        private const string Netfx40ClientProfileRegKeyName = Netfx40RegKeyName + "\\Client";
        private const string Netfx40FullProfileRegKeyName = Netfx40RegKeyName + "\\Full";
        private const string Netfx40SPxRegValueName = "Servicing";

        private const string Netfx45RegKeyName = NetfxRegKeyRoot + "\\v4\\Full";
        private const string Netfx45RegValueName = "Release";

        private const string NetfxStandardRegValueName = "Install";
        private const string NetfxStandrdSpxRegValueName = "SP";
        private const string NetfxStandardVersionRegValueName = "Version";

        private const string NetfxInstallRootRegKeyName = "Software\\Microsoft\\.NETFramework";
        private const string NetFxInstallRootRegValueName = "InstallRoot";

        private const string Netfx10VersionString = "v1.0.3705";
        private const string Netfx11VersionString = "v1.1.4322";
        private const string Netfx20VersionString = "v2.0.50727";
        private const string Netfx40VersionString = "v4.0.30319";
        private const string NetfxMscorwks = "mscorwks.dll";
        private const string NetfxMscorlib = "mscorlib.dll";

        private static readonly Version EmptyVersion = new Version(0, 0, 0, 0);
        private static readonly Version Netfx10Version = new Version(1, 0, 3705, 0);
        private static readonly Version Netfx11Version = new Version(1, 1, 4322, 573);
        private static readonly Version Netfx20Version = new Version(2, 0, 50727, 42);
        private static readonly Version Netfx30Version = new Version(3, 0, 4506, 26);
        private static readonly Version Netfx35Version = new Version(3, 5, 21022, 8);
        private static readonly Version Netfx40Version = new Version(4, 0, 30319);
        private static readonly Version Netfx45Version = new Version(4, 5, 50709);

        private static readonly Dictionary<FrameworkVersion, int> NetfxReleaseVersion = new Dictionary<FrameworkVersion, int>
        {
            { FrameworkVersion.Fx45,  378389 },
            { FrameworkVersion.Fx451,  378675 },
            { FrameworkVersion.Fx452,  379893 },
            { FrameworkVersion.Fx46,  393295 },
            { FrameworkVersion.Fx461,  3944254 },
            { FrameworkVersion.Fx462,  394802 },
            { FrameworkVersion.Fx47,  460798 },
        };
        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        private static void GetVersionFromRegistry()
        {
        }
        //private static void GetVersionFromRegistry()
        //{
        //    // Opens the registry key for the .NET Framework entry.
        //    using (RegistryKey ndpKey =
        //        RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
        //        OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
        //    {
        //        // As an alternative, if you know the computers you will query are running .NET Framework 4.5
        //        // or later, you can use:
        //        // using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
        //        // RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
        //        foreach (string versionKeyName in ndpKey.GetSubKeyNames())
        //        {
        //            if (versionKeyName.StartsWith("v"))
        //            {

        //                RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
        //                string name = (string)versionKey.GetValue("Version", "");
        //                string sp = versionKey.GetValue("SP", "").ToString();
        //                string install = versionKey.GetValue("Install", "").ToString();
        //                if (install == "") //no install info, must be later.
        //                    Console.WriteLine(versionKeyName + "  " + name);
        //                else
        //                {
        //                    if (sp != "" && install == "1")
        //                    {
        //                        Console.WriteLine(versionKeyName + "  " + name + "  SP" + sp);
        //                    }

        //                }
        //                if (name != "")
        //                {
        //                    continue;
        //                }
        //                foreach (string subKeyName in versionKey.GetSubKeyNames())
        //                {
        //                    RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
        //                    name = (string)subKey.GetValue("Version", "");
        //                    if (name != "")
        //                        sp = subKey.GetValue("SP", "").ToString();
        //                    install = subKey.GetValue("Install", "").ToString();
        //                    if (install == "") //no install info, must be later.
        //                        Console.WriteLine(versionKeyName + "  " + name);
        //                    else
        //                    {
        //                        if (sp != "" && install == "1")
        //                        {
        //                            Console.WriteLine("  " + subKeyName + "  " + name + "  SP" + sp);
        //                        }
        //                        else if (install == "1")
        //                        {
        //                            Console.WriteLine("  " + subKeyName + "  " + name);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }




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
            var version = new Version(0, 0, 0, 0);

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
            var version = new Version(0, 0, 0, 0);

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
            var servicePackLevel = -1;

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
            var servicePackLevel = -1;

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
            var ret = false;

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
                case FrameworkVersion.Fx451:
                case FrameworkVersion.Fx452:
                case FrameworkVersion.Fx46:
                case FrameworkVersion.Fx461:
                case FrameworkVersion.Fx462:
                case FrameworkVersion.Fx47:
                    ret = IsNetfx45OrLaterInstalled(frameworkVersion);
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
            var ret = false;

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
            var version = new Version(0, 0, 0, 0);
            var min = new Version(0, 0, 0, 0);
            var max = new Version(0, 0, 0, 0);

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

            var valid = GetCoreFrameworkVersion(frameworkVersion, out var coreVersion) ? version >= max && coreVersion >= min : version >= max;

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
            var valid = false;
            var installPath = GetCorePath(frameworkVersion);
            version = null;
            if (!String.IsNullOrEmpty(installPath))
            {
                var fvi = FileVersionInfo.GetVersionInfo(installPath);
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
            string installRoot;
            if (!GetRegistryValue(RegistryHive.LocalMachine, NetfxInstallRootRegKeyName, NetFxInstallRootRegValueName, RegistryValueKind.String, out installRoot))
            {
                throw new DirectoryNotFoundException(Strings.ApplicationExcpetion_UnableToDetermineInstallRoot);
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
            var ret = String.Empty;

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
            var servicePackLevel = -1;
            string regValue;

            bool foundKey;
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
                var index = regValue.LastIndexOf(',');
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
            // We can only get -1 if the .NET Framework is not
            // installed or there was some kind of error retrieving
            // the data from the registry
            var servicePackLevel = -1;

            if (GetRegistryValue(RegistryHive.LocalMachine, key, value, RegistryValueKind.DWord, out int regValue))
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
            var version = new Version(0, 0, 0, 0);
            string regValue;

            bool foundKey;
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
                var index = regValue.LastIndexOf(',');
                if (index > 0)
                {
                    var tokens = regValue.Substring(0, index).Split(',');
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
            // We can only get -1 if the .NET Framework is not
            // installed or there was some kind of error retrieving
            // the data from the registry
            var version = new Version(0, 0, 0, 0);

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx11RegKeyName, NetfxStandardRegValueName, RegistryValueKind.DWord, out int regValue))
            {
                if (regValue == 1)
                {
                    // In the strict sense, we are cheating here, but the registry key name itself
                    // contains the version number.
                    var tokens = Netfx11RegKeyName.Split(new string[] { "NDP\\v" }, StringSplitOptions.None);
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

            // We can only get -1 if the .NET Framework is not
            // installed or there was some kind of error retrieving
            // the data from the registry
            var version = new Version(0, 0, 0, 0);

            // If we have a Version registry value, use that.
            try
            {
                version = GetNetfxExactVersion(Netfx20RegKeyName, NetfxStandardVersionRegValueName);
            }
            catch (IOException)
            {
                string regValue;
                // If we hit an exception here, the Version registry key probably doesn't exist so try
                // to get the version based on the registry key name itself.
                if (GetRegistryValue(RegistryHive.LocalMachine, Netfx20RegKeyName, Netfx20PlusBuildRegValueName, RegistryValueKind.String, out regValue))
                {
                    if (!String.IsNullOrEmpty(regValue))
                    {
                        var versionTokens = Netfx20RegKeyName.Split(new string[] { "NDP\\v" }, StringSplitOptions.None);
                        if (versionTokens.Length == 2)
                        {
                            var tokens = versionTokens[1].Split('.');
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

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            var version = new Version(0, 0, 0, 0);

            string regValue;
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
            var success = false;
            data = default(T);

            using (var registryKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry32)?.OpenSubKey(key, RegistryRights.ReadKey))
            {
                if (registryKey != null)
                {
                    try
                    {
                        // If the key was opened, try to retrieve the value.
                        var kindFound = registryKey.GetValueKind(value);
                        if (kindFound == kind)
                        {
                            var regValue = registryKey.GetValue(value, null);
                            if (regValue != null)
                            {
                                data = (T)regValue;//(T)Convert.ChangeType(regValue, typeof(T), CultureInfo.InvariantCulture);
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
            var regKeyName = String.Empty;
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
                case FrameworkVersion.Fx451:
                case FrameworkVersion.Fx452:
                case FrameworkVersion.Fx46:
                case FrameworkVersion.Fx461:
                case FrameworkVersion.Fx462:
                case FrameworkVersion.Fx47:
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
        private static bool IsNetfx10Installed()
        {
            var found = GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx10), Netfx10RegKeyValue, RegistryValueKind.String, out string regValue);
            return found && CheckFxVersion(FrameworkVersion.Fx10);
        }
        #endregion

        #region IsNetfx11Installed
        /// <summary>
        /// Detects if the .NET Framework 1.1 is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the .NET Framework 1.1 is
        /// installed; otherwise <see langword="false"/>.</returns>
        private static bool IsNetfx11Installed()
        {
            var found = false;

            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx11), NetfxStandardRegValueName, RegistryValueKind.DWord, out int regValue))
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
        private static bool IsNetfx20Installed()
        {
            var found = false;

            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx20), NetfxStandardRegValueName, RegistryValueKind.DWord, out int regValue))
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
        private static bool IsNetfx30Installed()
        {
            var found = false;

            // The .NET Framework 3.0 is an add-in that installs on top of
            // the .NET Framework 2.0, so validate that both 2.0 and 3.0
            // are installed.
            if (IsNetfx20Installed())
            {
                // Check that the InstallSuccess registry value exists and equals 1.
                if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(FrameworkVersion.Fx30), Netfx30RegValueName, RegistryValueKind.DWord, out int regValue))
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
        private static bool IsNetfx35Installed(FrameworkVersion frameworkVersion)
        {
            var found = false;

            // The .NET Framework 3.0 is an add-in that installs on top of
            // the .NET Framework 2.0 and 3.0, so validate that 2.0, 3.0,
            // and 3.5 are installed.
            if (IsNetfx20Installed() && IsNetfx30Installed())
            {
                // Check that the Install registry value exists and equals 1.
                if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(frameworkVersion), NetfxStandardRegValueName, RegistryValueKind.DWord, out int regValue))
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
        private static bool IsNetfx40Installed(FrameworkVersion frameworkVersion)
        {
            var found = false;

            // The .NET Framework 4.0 introduced a new CLR version, so it
            // isn't dependent on other .NET Framework versions being
            // installed.
            // Check that the Install registry value exists and equals 1.
            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(frameworkVersion), NetfxStandardRegValueName, RegistryValueKind.DWord, out int regValue))
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

        #region IsNetfx45OrLaterInstalled
        /// <summary>
        /// Detects if the .NET Framework 4.5 or later profile is installed.
        /// </summary>
        /// <returns><see langword="true"/> if the specified .NET Framework
        /// 4.5 or later profile is installed; otherwise <see langword="false"/>.</returns>
        private static bool IsNetfx45OrLaterInstalled(FrameworkVersion frameworkVersion)
        {
            var found = false;
            if (GetRegistryValue(RegistryHive.LocalMachine, GetRegistryKey(frameworkVersion), Netfx45RegValueName, RegistryValueKind.DWord, out int regValue))
            {
                if (NetfxReleaseVersion[frameworkVersion] >= regValue)
                {
                    found = true;
                }
            }

            return found && CheckFxVersion(frameworkVersion);
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
            var found = false;
            string regValue;
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

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            var version = new Version(0, 0, 0, 0);

            string regValue;
            if (GetRegistryValue(RegistryHive.LocalMachine, CardSpaceServicesRegKeyName, CardSpaceServicesPlusImagePathRegName, RegistryValueKind.ExpandString, out regValue))
            {
                if (!String.IsNullOrEmpty(regValue))
                {
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(regValue.Trim('"'));
                    var index = fileVersionInfo.FileVersion.IndexOf(' ');
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
            var found = false;

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWCFRegKeyName, Netfx30RegValueName, RegistryValueKind.DWord, out int regValue))
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

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            var version = EmptyVersion;

            string regValue;
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
            var found = false;

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWPFRegKeyName, Netfx30RegValueName, RegistryValueKind.DWord, out int regValue))
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

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            var version = new Version(0, 0, 0, 0);

            string regValue;
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
            var found = false;

            if (GetRegistryValue(RegistryHive.LocalMachine, Netfx30PlusWFRegKeyName, Netfx30RegValueName, RegistryValueKind.DWord, out int regValue))
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

            // We can only get the default version if the .NET Framework
            // is not installed or there was some kind of error retrieving
            // the data from the registry
            var version = new Version(0, 0, 0, 0);

            string regValue;
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
