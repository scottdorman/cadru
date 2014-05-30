using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections;
using Microsoft.Win32;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using Cadru.Collections;
using Cadru.IO;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTests.IO
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExtendedDirectoryInfoTests
    {

        [TestInitialize]
        [TestCleanup]
        public void Cleanup()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            string path2 = Path.Combine(Path.GetTempPath(), "CadruTesttMoved");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            if (Directory.Exists(path2))
            {
                Directory.Delete(path2, true);
            }
        }

        [TestMethod]
        public void Constructor()
        {
            try
            {
                ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(null);
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
                ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(String.Empty);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Constructor1()
        {
            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(Path.GetTempPath());
            Assert.IsNotNull(edi);
        }

        [TestMethod]
        public void Create()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            edi.Create();
            Assert.IsTrue(Directory.Exists(path));
        }

        [TestMethod]
        public void Create1()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            DirectorySecurity directorySecurity = new DirectorySecurity();

            directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            edi.Create(directorySecurity);
            Assert.IsTrue(Directory.Exists(path));

            DirectorySecurity actualDirectorySecurity = Directory.GetAccessControl(path);
            AuthorizationRuleCollection rules = actualDirectorySecurity.GetAccessRules(true, true, typeof(NTAccount));
            foreach (AuthorizationRule rule in rules)
            {
                FileSystemAccessRule accessRule = (FileSystemAccessRule)rule;

                if (accessRule.IdentityReference.Value == "Everyone")
                {
                    Assert.IsTrue(accessRule.AccessControlType == AccessControlType.Allow);
                    Assert.IsTrue(accessRule.FileSystemRights == FileSystemRights.FullControl);
                }
            }
        }

        [TestMethod]
        public void CreateSubdirectory()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            string path2 = edi.CreateSubdirectory("Subdir").FullName;
            Assert.IsTrue(Directory.Exists(path2));
        }

        [TestMethod]
        public void CreateSubdirectory1()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            path = Path.Combine(path, "Subdir");

            DirectorySecurity directorySecurity = new DirectorySecurity();
            directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));

            string path2 = edi.CreateSubdirectory("Subdir", directorySecurity).FullName;
            Assert.IsTrue(Directory.Exists(path2));

            DirectorySecurity actualDirectorySecurity = Directory.GetAccessControl(path2);
            AuthorizationRuleCollection rules = actualDirectorySecurity.GetAccessRules(true, true, typeof(NTAccount));
            foreach (AuthorizationRule rule in rules)
            {
                FileSystemAccessRule accessRule = (FileSystemAccessRule)rule;

                if (accessRule.IdentityReference.Value == "Everyone")
                {
                    Assert.IsTrue(accessRule.AccessControlType == AccessControlType.Allow);
                    Assert.IsTrue(accessRule.FileSystemRights == FileSystemRights.FullControl);
                }
            }
        }

        [TestMethod]
        public void Delete()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            edi.Create();
            Assert.IsTrue(Directory.Exists(path));

            edi.Delete();

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");
        }

        [TestMethod]
        public void Delete1()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            string path2 = edi.CreateSubdirectory("Subdir").FullName;
            Assert.IsTrue(Directory.Exists(path));

            File.WriteAllText(Path.Combine(path2, "testfile.txt"), "This is a test.");
            edi.Delete(true);

            Assert.IsFalse(Directory.Exists(path), "Unable to delete the test directory.");
        }

        [TestMethod]
        public void GetAccessControl()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            DirectorySecurity actual = edi.GetAccessControl();
            DirectorySecurity expected = di.GetAccessControl();

            CollectionAssert.AreEqual(expected.GetSecurityDescriptorBinaryForm(), actual.GetSecurityDescriptorBinaryForm());
        }

        [TestMethod]
        [Ignore]
        public void GetAccessControl1()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            DirectorySecurity actual = edi.GetAccessControl(AccessControlSections.All);
            DirectorySecurity expected = di.GetAccessControl(AccessControlSections.All);

            CollectionAssert.AreEqual(expected.GetSecurityDescriptorBinaryForm(), actual.GetSecurityDescriptorBinaryForm());
        }

        [TestMethod]
        public void GetDirectories()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            DirectoryInfo[] actual = edi.GetDirectories();
            DirectoryInfo[] expected = di.GetDirectories();

            CollectionAssert.AreEqual(expected, actual, ComparisonComparer<DirectoryInfo>.Create((a, b) => a.FullName.CompareTo(b.FullName)));

            foreach (DirectoryInfo d in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(DirectoryInfo o)
                    {
                        return (d.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void GetDirectories1()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            DirectoryInfo[] actual = edi.GetDirectories("*");
            DirectoryInfo[] expected = di.GetDirectories("*");

            CollectionAssert.AreEqual(expected, actual, ComparisonComparer<DirectoryInfo>.Create((a, b) => a.FullName.CompareTo(b.FullName)));
 
            foreach (DirectoryInfo d in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(DirectoryInfo o)
                    {
                        return (d.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void GetDirectories2()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            DirectoryInfo[] actual = edi.GetDirectories("*", SearchOption.TopDirectoryOnly);
            DirectoryInfo[] expected = di.GetDirectories("*", SearchOption.TopDirectoryOnly);

            CollectionAssert.AreEqual(expected, actual, ComparisonComparer<DirectoryInfo>.Create((a, b) => a.FullName.CompareTo(b.FullName)));

            foreach (DirectoryInfo d in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(DirectoryInfo o)
                    {
                        return (d.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void GetFiles()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            FileInfo[] actual = edi.GetFiles();
            FileInfo[] expected = di.GetFiles();

            CustomAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreNotNull(actual);

            foreach (FileInfo f in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(FileInfo o)
                    {
                        return (f.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void GetFiles1()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            FileInfo[] actual = edi.GetFiles("*");
            FileInfo[] expected = di.GetFiles("*");

            CustomAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreNotNull(actual);

            foreach (FileInfo f in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(FileInfo o)
                    {
                        return (f.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void GetFiles2()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            FileInfo[] actual = edi.GetFiles("*", SearchOption.TopDirectoryOnly);
            FileInfo[] expected = di.GetFiles("*", SearchOption.TopDirectoryOnly);

            CustomAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreNotNull(actual);

            foreach (FileInfo f in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(FileInfo o)
                    {
                        return (f.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void GetFileSystemInfos()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            FileSystemInfo[] actual = edi.GetFileSystemInfos();
            FileSystemInfo[] expected = di.GetFileSystemInfos();

            CustomAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreNotNull(actual);

            foreach (FileSystemInfo f in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(FileSystemInfo o)
                    {
                        return (f.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void GetFileSystemInfos1()
        {
            string path = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            FileSystemInfo[] actual = edi.GetFileSystemInfos("*");
            FileSystemInfo[] expected = di.GetFileSystemInfos("*");

            CustomAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreNotNull(actual);

            foreach (FileSystemInfo f in actual)
            {
                Assert.IsTrue(Array.Exists(expected,
                    delegate(FileSystemInfo o)
                    {
                        return (f.FullName == o.FullName);
                    }));
            }
        }

        [TestMethod]
        public void MoveTo()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            string path2 = Path.Combine(Path.GetTempPath(), "CadruTestMoved");
            edi.MoveTo(path2);

            Assert.IsTrue(Directory.Exists(path2));

            Directory.Delete(path2);
        }

        [TestMethod]
        public void Exists()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            try
            {
                Directory.CreateDirectory(path);

                ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
                Assert.IsNotNull(edi);

                Assert.IsTrue(edi.Exists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [Ignore]
        public void SetAccessControl()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);

            DirectoryInfo di = new DirectoryInfo(path);
            DirectorySecurity expected = di.GetAccessControl(AccessControlSections.All);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            DirectorySecurity directorySecurity = new DirectorySecurity();
            directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));

            edi.SetAccessControl(directorySecurity);

            Assert.AreNotEqual(expected.GetSecurityDescriptorBinaryForm(), edi.GetAccessControl().GetSecurityDescriptorBinaryForm());

            DirectorySecurity actualDirectorySecurity = Directory.GetAccessControl(path);
            AuthorizationRuleCollection rules = actualDirectorySecurity.GetAccessRules(true, true, typeof(NTAccount));
            foreach (AuthorizationRule rule in rules)
            {
                FileSystemAccessRule accessRule = (FileSystemAccessRule)rule;

                if (accessRule.IdentityReference.Value == "Everyone")
                {
                    Assert.IsTrue(accessRule.AccessControlType == AccessControlType.Allow);
                    Assert.IsTrue(accessRule.FileSystemRights == FileSystemRights.FullControl);
                }
            }

            di.SetAccessControl(expected);
        }

        [TestMethod]
        public void Properties()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            Assert.AreEqual(edi.Root.FullName, di.Root.FullName);
            Assert.AreEqual(edi.Parent.FullName, di.Parent.FullName);
            Assert.AreEqual(edi.Name, di.Name);
            Assert.AreEqual(edi.LastWriteTimUtc, di.LastWriteTimeUtc);
            Assert.AreEqual(edi.LastWriteTime, di.LastWriteTime);
            Assert.AreEqual(edi.LastAccessTimeUtc, di.LastAccessTimeUtc);
            Assert.AreEqual(edi.LastAccessTime, di.LastAccessTime);
            Assert.AreEqual(edi.FullName, di.FullName);
            Assert.AreEqual(edi.Extension, di.Extension);
            Assert.AreEqual(edi.Exists, di.Exists);
            Assert.AreEqual(edi.CreationTime, di.CreationTime);
            Assert.AreEqual(edi.CreateTimeUtc, di.CreationTimeUtc);
            Assert.AreEqual(edi.Attributes, di.Attributes);
        }

        [TestMethod]
        public void Properties1()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            edi.Attributes |= FileAttributes.ReadOnly;

            DirectoryInfo di = new DirectoryInfo(path);

            Assert.AreEqual(edi.Attributes, di.Attributes);

            di.Attributes &= ~FileAttributes.ReadOnly;
        }

        [TestMethod]
        public void Properties2()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            edi.CreationTime = new DateTime(2006, 1, 1);

            DirectoryInfo di = new DirectoryInfo(path);

            Assert.AreEqual(edi.CreationTime, di.CreationTime);
        }

        [TestMethod]
        public void Refresh()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);
            DirectoryInfo di = new DirectoryInfo(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            Assert.AreEqual(edi.Attributes, di.Attributes);

            di.Attributes |= FileAttributes.ReadOnly;
            edi.Refresh();

            Assert.AreEqual(edi.Attributes, di.Attributes);

            di.Attributes &= ~FileAttributes.ReadOnly;
        }

        [TestMethod]
        public void DirectoryOwner()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);

            DirectoryInfo di = new DirectoryInfo(path);
            DirectorySecurity ds = di.GetAccessControl(AccessControlSections.Owner);
            string expected = ds.GetOwner(typeof(NTAccount)).ToString();

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);
            string actual = edi.DirectoryOwner;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);

            DirectoryInfo di = new DirectoryInfo(path);
            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);

            Assert.AreEqual(di.ToString(), edi.ToString());
        }

        [TestMethod]
        public void Serialization()
        {
            string path = Path.Combine(Path.GetTempPath(), "CadruTest");
            Directory.CreateDirectory(path);

            ExtendedDirectoryInfo edi = new ExtendedDirectoryInfo(path);
            Assert.IsNotNull(edi);
            byte[] data = null;

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, edi);
                data = stream.ToArray();
            }

            Assert.IsNotNull(data);
            ConditionAssert.Greater(data.Length, 0);

            using (MemoryStream stream = new MemoryStream(data))
            {
                    BinaryFormatter bf = new BinaryFormatter();
                    ExtendedDirectoryInfo edi2 = bf.Deserialize(stream) as ExtendedDirectoryInfo;
                    Assert.IsNotNull(edi2);
                    Assert.AreEqual(edi.FullName, edi2.FullName);
            }


        }
    }
}
