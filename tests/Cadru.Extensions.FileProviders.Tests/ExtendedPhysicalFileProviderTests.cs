using System.IO;

using Cadru.UnitTest.Framework;

using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Extensions.FileProviders.Tests
{
    [TestClass]
    public class ExtendedPhysicalFileProviderTests
    {
        [TestMethod]
        public void CreateDirectory()
        {
            using (var provider = new ExtendedPhysicalFileProvider(Path.GetTempPath()))
            {
                var info = provider.CreateDirectory("ExtendedPhysicalFileProvider-CreateDirectoryTest");
                Assert.That.IsType<PhysicalDirectoryInfo>(info);
                Assert.IsTrue(info.Exists);
            }
        }

        [TestMethod]
        public void CreateFile()
        {
            using (var provider = new ExtendedPhysicalFileProvider(Path.GetTempPath()))
            {
                var info = provider.CreateFile("ExtendedPhysicalFileProvider-CreateFileTest.txt");
                Assert.That.IsType<PhysicalFileInfo>(info);
                Assert.IsTrue(info.Exists);
            }
        }

        [TestMethod]
        public void GetDirectoryInfo()
        {
            using (var provider = new ExtendedPhysicalFileProvider(Path.GetTempPath()))
            {
                var info = provider.CreateDirectory("ExtendedPhysicalFileProvider-CreateDirectoryTest");
                Assert.That.IsType<PhysicalDirectoryInfo>(info);
                Assert.IsTrue(info.Exists);

                info = provider.GetDirectoryInfo("ExtendedPhysicalFileProvider-CreateDirectoryTest");
                Assert.That.IsType<PhysicalDirectoryInfo>(info);
                Assert.IsTrue(info.Exists);

                info = provider.GetDirectoryInfo("ExtendedPhysicalFileProvider-DoesNotExistCreateDirectoryTest");
                Assert.That.IsType<PhysicalDirectoryInfo>(info);
                Assert.IsFalse(info.Exists);
            }
        }
    }
}
