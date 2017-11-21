using System;
using System.Diagnostics.CodeAnalysis;
using Cadru.Environment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace Cadru.UnitTests.Environment
{
    /// <summary>
    ///This is a test class for CadruUtilities.EnumerationUtilities and is intended
    ///to contain all CadruUtilities.EnumerationUtilities Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class FrameworkVersionDetectionTests
    {
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
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(FrameworkVersion.Fx45));
        }

        [TestMethod]
        public void GetServicePackLevel()
        {
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx10), -1);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx11), -1);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx20), 2);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx30), 2);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx35), -1);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx35ClientProfile), -1);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx35ServerCoreProfile), -1);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx40ClientProfile), 0);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx40), 0);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx45), 0);
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
        public void IsFoundationLibraryInstalled()
        {
            Assert.IsFalse(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.CardSpace));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.WCF));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.WF));
            Assert.IsTrue(FrameworkVersionDetection.IsInstalled(WindowsFoundationLibrary.WPF));
        }

        [TestMethod]
        public void GetFoundationLibraryServicePackLevel()
        {
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.CardSpace), 2);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.WCF), 2);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.WF), 2);
            Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(WindowsFoundationLibrary.WPF), 2);
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
        [Ignore]
        public void FakeFramework10()
        {
            try
            {
                using (var regKey = Registry.LocalMachine.CreateSubKey(@"Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}"))
                {
                    regKey.SetValue("Version", "1,0,0000,1");
                    var emptyVersion = new Version(0, 0, 0, 0);

                    Assert.AreEqual(FrameworkVersionDetection.GetServicePackLevel(FrameworkVersion.Fx10), 1);
                    Assert.AreNotEqual(emptyVersion, FrameworkVersionDetection.GetExactVersion(FrameworkVersion.Fx10));
                }
            }
            finally
            {
                Registry.LocalMachine.DeleteSubKey(@"Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}");
            }
        }

    }
}
