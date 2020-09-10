//------------------------------------------------------------------------------
// <copyright file="InternetInformationServicesDetectionTests.cs"
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

using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Environment.Tests
{
    /// <summary>
    ///This is a test class for CadruUtilities.EnumerationUtilities and is intended
    ///to contain all CadruUtilities.EnumerationUtilities Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class InternetInformationServicesDetectionTests
    {
        [TestMethod]
        public void IsAspNetRegistered()
        {
            Assert.IsFalse(InternetInformationServicesDetection.IsAspNetRegistered(FrameworkVersion.Fx10));
            Assert.IsFalse(InternetInformationServicesDetection.IsAspNetRegistered(FrameworkVersion.Fx11));
            Assert.IsTrue(InternetInformationServicesDetection.IsAspNetRegistered(FrameworkVersion.Fx20));
            Assert.IsTrue(InternetInformationServicesDetection.IsAspNetRegistered(FrameworkVersion.Fx30));
            Assert.IsTrue(InternetInformationServicesDetection.IsAspNetRegistered(FrameworkVersion.Fx35));
        }

        [TestMethod]
        public void IsAspRegistered()
        {
            Assert.IsFalse(InternetInformationServicesDetection.IsAspRegistered());
        }

        [TestMethod]
        [Ignore("Requires IIS to be installed on the server running tests.")]
        public void IsFeatureInstalled()
        {
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.ApplicationInitialization));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.ASP));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.AspNet));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.AspNet45));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.Authorization));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.BasicAuthentication));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.CGI));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.ClientCertificateMappingAuthentication));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.CustomLogging));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.DefaultDocument));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.DigestAuthentication));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.DirectoryBrowsing));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.DynamicContentCompression));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.FTP));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.FTPExtensibility));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.HttpLogging));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.HttpRedirection));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.IISCertificateMappingAuthentication));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.IPSecurity));
            Assert.IsTrue(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesFeature.ISAPIExtensions));
        }

        [TestMethod]
        public void IsIISInstalled()
        {
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesVersion.IIS4));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesVersion.IIS5));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesVersion.IIS6));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesVersion.IIS7));
        }

        [TestMethod]
        public void IsSubcomponentInstalled()
        {
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.ASP));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.Bits));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.BitsISAPI));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.Common));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.FrontPageExtensions));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.FTP));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.ManagementConsole));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.InternetDataConnector));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.InternetPrinting));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.NNTP));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.RemoteAdmin));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.ServerSideIncludes));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.SMTP));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.TSWebClient));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.WebDAV));
            Assert.IsFalse(InternetInformationServicesDetection.IsInstalled(InternetInformationServicesSubcomponent.WWW));
        }
    }
}