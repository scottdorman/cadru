//------------------------------------------------------------------------------
// <copyright file="FrameworkVersionDetectionTests.cs"
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
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace Cadru.Environment.Tests
{
    /// <summary>
    ///This is a test class for CadruUtilities.EnumerationUtilities and is intended
    ///to contain all CadruUtilities.EnumerationUtilities Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class FrameworkVersionDetectionTests
    {
        [TestMethod]
        [Ignore(".NET Framework 1.0 pseudo-test and requires access to create/delete registry keys")]
        public void FakeFramework10()
        {
            try
            {
                using var regKey = Registry.LocalMachine.CreateSubKey(@"Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}");
                regKey.SetValue("Version", "1,0,0000,1");
                var emptyVersion = new Version(0, 0, 0, 0);

                Assert.AreEqual(1, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx10));
                Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx10));
            }
            finally
            {
                Registry.LocalMachine.DeleteSubKey(@"Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}");
            }
        }

        [TestMethod]
        public void GetExactVersion()
        {
            var emptyVersion = new Version(0, 0, 0, 0);

            Assert.AreEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx11));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx20));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx30));
            Assert.AreEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx35));
            Assert.AreEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx35ClientProfile));
            Assert.AreEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx35ServerCoreProfile));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx40ClientProfile));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx40));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx45));
        }

        [TestMethod]
        public void GetFoundationLibraryExactVersion()
        {
            var emptyVersion = new Version(0, 0, 0, 0);

            Assert.AreEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(WindowsFoundationLibrary.CardSpace));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(WindowsFoundationLibrary.WCF));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(WindowsFoundationLibrary.WF));
            Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(WindowsFoundationLibrary.WPF));
        }

        [TestMethod]
        public void GetFoundationLibraryServicePackLevel()
        {
            Assert.AreEqual(2, FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.CardSpace));
            Assert.AreEqual(2, FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.WCF));
            Assert.AreEqual(2, FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.WF));
            Assert.AreEqual(2, FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.WPF));
        }

        [TestMethod]
        public void GetServicePackLevel()
        {
            Assert.AreEqual(-1, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx10));
            Assert.AreEqual(-1, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx11));
            Assert.AreEqual(2, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx20));
            Assert.AreEqual(2, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx30));
            Assert.AreEqual(-1, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx35));
            Assert.AreEqual(-1, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx35ClientProfile));
            Assert.AreEqual(-1, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx35ServerCoreProfile));
            Assert.AreEqual(0, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx40ClientProfile));
            Assert.AreEqual(0, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx40));
            Assert.AreEqual(0, FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx45));
        }

        [TestMethod]
        public void IsFoundationLibraryInstalled()
        {
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.CardSpace));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.WCF));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.WF));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.WPF));
        }

        [TestMethod]
        public void IsFrameworkInstalled()
        {
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx10));
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx11));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx20));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx30));
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx35));
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx35ClientProfile));
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx35ServerCoreProfile));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx40ClientProfile));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx40));
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx45));
        }
    }
}