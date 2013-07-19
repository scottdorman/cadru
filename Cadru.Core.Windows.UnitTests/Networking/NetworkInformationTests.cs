using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cadru.Networking;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Cadru.UnitTest.Framework;

namespace Cadru.UnitTests.Networking
{
    /// <summary>
    ///This is a test class for CadruNetworking.NetworkInformation and is intended
    ///to contain all CadruNetworking.NetworkInformation Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class NetworkInformationTests
    {
        /// <summary>
        ///A test for GetDomains ()
        ///</summary>
        [TestMethod]
        public void GetDomains()
        {
            ServerInfo[] actual;

            actual = ExtendedNetworkInformation.GetDomains();
            CustomAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreNotNull(actual);
            CollectionAssert.AllItemsAreUnique(actual);
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(ServerInfo));
        }

        /// <summary>
        ///A test for GetServerList (ServerTypes, string)
        ///</summary>
        [TestMethod]
        public void GetServerList()
        {
            ServerTypes serverType = ServerTypes.WindowsNT;

            string domain = null;

            ServerInfo[] actual;

            actual = ExtendedNetworkInformation.GetServerList(serverType, domain);
            CustomAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreNotNull(actual);
            CollectionAssert.AllItemsAreUnique(actual);
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(ServerInfo));
        }

        [TestMethod]
        public void GetServerList2()
        {
            ServerTypes serverType = ServerTypes.VMS;

            string domain = "GOOFY";

            ServerInfo[] actual;

            try
            {
                actual = ExtendedNetworkInformation.GetServerList(serverType, domain);
            }
            catch (Win32Exception e)
            {
                Assert.IsTrue(true, e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void HostName()
        {
            string hostName = ExtendedNetworkInformation.GetHostName();
            CustomAssert.IsNotEmpty(hostName);
        }

        [TestMethod]
        public void GetServerInfo()
        {
            ServerInfo server = ExtendedNetworkInformation.GetServerInfo();
            Assert.IsNotNull(server);
            Assert.IsNotNull(server.Name);
            Assert.IsTrue(server.Name.Length > 0);
            
            Assert.IsNotNull(server.Version);
            ConditionAssert.GreaterOrEqual(server.MajorVersion, 0);
            ConditionAssert.GreaterOrEqual(server.MinorVersion, 0);
            Assert.AreEqual(server.MajorVersion, server.Version.Major);
            Assert.AreEqual(server.MinorVersion, server.Version.Minor);

            Assert.IsNotNull(server.Comment);
            
            Assert.IsTrue(Enum.IsDefined(typeof(PlatformId), server.PlatformId));
            Assert.IsTrue(server.ServerType.HasFlag(ServerTypes.WindowsNT));


            ServerInfo server2 = ExtendedNetworkInformation.GetServerInfo("localhost");
            Assert.IsNotNull(server2);

            ServerInfo server3 = ExtendedNetworkInformation.GetServerInfo(@"\\localhost");
            Assert.IsNotNull(server3);

            Assert.AreNotEqual(server, server2);
            Assert.AreEqual(server2, server3);

            try
            {
                ExtendedNetworkInformation.GetServerInfo(null);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                ExtendedNetworkInformation.GetServerInfo("\\goofy");
            }
            catch (Win32Exception e)
            {
                Assert.IsTrue(true, e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetIpAddresses()
        {
            var ipAddresses = ExtendedNetworkInformation.GetIPAddresses();
            CustomAssert.IsNotEmpty(ipAddresses);
        }
    }
}
