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
using System.Diagnostics;
using Cadru.IO;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTests.IO
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExtendedFileInfoTests
    {
        [TestInitialize]
        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void Constructor()
        {
            try
            {
                ExtendedFileInfo efi = new ExtendedFileInfo(null);
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
                ExtendedFileInfo efi = new ExtendedFileInfo(String.Empty);
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
            ExtendedFileInfo efi = new ExtendedFileInfo(Path.GetTempFileName());
            Assert.IsNotNull(efi);
        }

        [TestMethod]
        public void CopyTo()
        {
            string tempFile = Path.GetTempFileName();
            string tempFile2 = Path.GetFileNameWithoutExtension(tempFile) + "Copy" + Path.GetExtension(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.CopyTo(tempFile2);

            Assert.IsTrue(File.Exists(tempFile2));
        }

        [TestMethod]
        public void CopyTo2()
        {
            string tempFile = Path.GetTempFileName();
            string tempFile2 = Path.GetFileNameWithoutExtension(tempFile) + "Copy" + Path.GetExtension(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.CopyTo(tempFile2, true);

            Assert.IsTrue(File.Exists(tempFile2));

            try
            {
                efi.CopyTo(tempFile2, false);
            }
            catch (IOException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Create()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), "CreatedFile.txt");
            File.Delete(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            using (FileStream stream = efi.Create())
            {
                Assert.IsTrue(File.Exists(tempFile));
            }
        }

        [TestMethod]
        public void Delete()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.Delete();

            Assert.IsFalse(File.Exists(tempFile));
        }

        [TestMethod]
        public void MoveTo()
        {
            string tempFile = Path.GetTempFileName();
            string tempFile2 = Path.GetFileNameWithoutExtension(tempFile) + "Copy" + Path.GetExtension(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.MoveTo(tempFile2);

            Assert.IsTrue(File.Exists(tempFile2));
        }

        [TestMethod]
        public void AppendText()
        {
            string tempFile = Path.GetTempFileName();
            string expected = "This is a test.";

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            using (StreamWriter writer = efi.AppendText())
            {
                Assert.IsNotNull(writer);
                writer.Write(expected);
            }

            string actual = File.ReadAllText(tempFile);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OpenWrite()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            using (FileStream stream = efi.OpenWrite())
            {
                Assert.IsFalse(stream.CanRead);
                Assert.IsTrue(stream.CanWrite);
            }
        }

        [TestMethod]
        public void OpenRead()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            using (FileStream stream = efi.OpenRead())
            {
                Assert.IsTrue(stream.CanRead);
                Assert.IsFalse(stream.CanWrite);
            }
        }

        [TestMethod]
        public void OpenText()
        {
            string tempFile = Path.GetTempFileName();
            string expected = "This is a test.";

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            File.AppendAllText(tempFile, expected);

            string actual = String.Empty;
            using (StreamReader stream = efi.OpenText())
            {
                Assert.IsTrue(stream.BaseStream.CanRead);
                Assert.IsFalse(stream.BaseStream.CanWrite);

                Assert.IsTrue(stream.CurrentEncoding == Encoding.UTF8);
                actual = stream.ReadToEnd();
            }

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Replace()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), "OriginalFile.txt");
            string tempFile2 = Path.Combine(Path.GetTempPath(), "NewFile.txt");
            string backupFile = Path.GetFileNameWithoutExtension(tempFile) + "Backup" + Path.GetExtension(tempFile);
            string expected = "This is a test.";

            File.Delete(tempFile);
            File.Delete(tempFile2);
            File.Delete(backupFile);

            File.AppendAllText(tempFile, expected);
            File.AppendAllText(tempFile2, String.Empty);

            // Move the contents of tempFile into tempFile2
            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.Replace(tempFile2, null);

            Assert.IsTrue(File.Exists(tempFile2));

            string actual = File.ReadAllText(tempFile2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Replace2()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), "OriginalFile.txt");
            string tempFile2 = Path.Combine(Path.GetTempPath(), "NewFile.txt");
            string backupFile = Path.Combine(Path.GetTempPath(), "OriginalFileBackup.txt");
            string expected = "This is a test.";

            File.Delete(tempFile);
            File.Delete(tempFile2);
            File.Delete(backupFile);

            File.AppendAllText(tempFile, expected);
            File.AppendAllText(tempFile2, String.Empty);

            // Move the contents of tempFile into tempFile2
            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.Replace(tempFile2, backupFile);

            Assert.IsTrue(File.Exists(backupFile));
            Assert.IsTrue(File.Exists(tempFile2));

            string actual = File.ReadAllText(tempFile2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Replace3()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), "OriginalFile.txt");
            string tempFile2 = Path.Combine(Path.GetTempPath(), "NewFile.txt");
            string backupFile = Path.Combine(Path.GetTempPath(), "OriginalFileBackup.txt");
            string expected = "This is a test.";

            File.Delete(tempFile);
            File.Delete(tempFile2);
            File.Delete(backupFile);

            File.AppendAllText(tempFile, expected);
            File.AppendAllText(tempFile2, String.Empty);

            // Move the contents of tempFile into tempFile2
            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.Replace(tempFile2, backupFile, true);

            Assert.IsTrue(File.Exists(backupFile));
            Assert.IsTrue(File.Exists(tempFile2));

            string actual = File.ReadAllText(tempFile2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAccessControl()
        {
            string tempFile = Path.GetTempFileName();
            FileInfo fi = new FileInfo(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            FileSecurity actual = efi.GetAccessControl();
            FileSecurity expected = fi.GetAccessControl();

            CollectionAssert.AreEqual(expected.GetSecurityDescriptorBinaryForm(), actual.GetSecurityDescriptorBinaryForm());
        }

        [TestMethod]
        [Ignore]
        public void GetAccessControl1()
        {
            string tempFile = Path.GetTempFileName();
            FileInfo fi = new FileInfo(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            FileSecurity actual = efi.GetAccessControl(AccessControlSections.All);
            FileSecurity expected = fi.GetAccessControl(AccessControlSections.All);

            CollectionAssert.AreEqual(expected.GetSecurityDescriptorBinaryForm(), actual.GetSecurityDescriptorBinaryForm());
        }

        [TestMethod]
        [Ignore]
        public void SetAccessControl()
        {
            string tempFile = Path.GetTempFileName();

            FileInfo fi = new FileInfo(tempFile);
            FileSecurity expected = fi.GetAccessControl(AccessControlSections.All);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            FileSecurity fileSecurity = new FileSecurity();
            fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));

            efi.SetAccessControl(fileSecurity);

            Assert.AreNotEqual(expected.GetSecurityDescriptorBinaryForm(), efi.GetAccessControl().GetSecurityDescriptorBinaryForm());

            FileSecurity actualFileSecurity = File.GetAccessControl(tempFile);
            AuthorizationRuleCollection rules = actualFileSecurity.GetAccessRules(true, true, typeof(NTAccount));
            foreach (AuthorizationRule rule in rules)
            {
                FileSystemAccessRule accessRule = (FileSystemAccessRule)rule;

                if (accessRule.IdentityReference.Value == "Everyone")
                {
                    Assert.IsTrue(accessRule.AccessControlType == AccessControlType.Allow);
                    Assert.IsTrue(accessRule.FileSystemRights == FileSystemRights.FullControl);
                }
            }

            fi.SetAccessControl(expected);
        }

        [TestMethod]
        public void Refresh()
        {
            string tempFile = Path.GetTempFileName();

            FileInfo fi = new FileInfo(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            Assert.AreEqual(efi.Attributes, fi.Attributes);

            fi.Attributes |= FileAttributes.ReadOnly;
            efi.Refresh();

            Assert.AreEqual(efi.Attributes, fi.Attributes);

            fi.Attributes &= ~FileAttributes.ReadOnly;
        }

        [TestMethod]
        public void ToStringTest()
        {
            string tempFile = Path.GetTempFileName();

            FileInfo fi = new FileInfo(tempFile);
            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            Assert.AreEqual(fi.ToString(), efi.ToString());
        }

        [TestMethod]
        public void Serialization()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);
            byte[] data = null;

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, efi);
                data = stream.ToArray();
            }

            Assert.IsNotNull(data);
            ConditionAssert.Greater(data.Length, 0);

            using (MemoryStream stream = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                ExtendedFileInfo efi2 = bf.Deserialize(stream) as ExtendedFileInfo;
                Assert.IsNotNull(efi2);
                Assert.AreEqual(efi.FullName, efi2.FullName);
            }
        }

        [TestMethod]
        public void Open()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            using (FileStream stream = efi.Open(FileMode.Open))
            {
                Assert.IsNotNull(stream);
                Assert.IsTrue(stream.Length == 0);
            }

            tempFile = Path.GetTempFileName();
            efi = new ExtendedFileInfo(tempFile);
            using (FileStream stream = efi.Open(FileMode.Open, FileAccess.Read))
            {
                Assert.IsNotNull(stream);
                Assert.IsTrue(stream.Length == 0);
                Assert.IsTrue(stream.CanRead);
                Assert.IsFalse(stream.CanWrite);
            }

            tempFile = Path.GetTempFileName();
            efi = new ExtendedFileInfo(tempFile);
            using (FileStream stream = efi.Open(FileMode.Open, FileAccess.Read, FileShare.None))
            {
                Assert.IsNotNull(stream);
                Assert.IsTrue(stream.Length == 0);
                Assert.IsTrue(stream.CanRead);
                Assert.IsFalse(stream.CanWrite);

                try
                {
                    File.Open(tempFile, FileMode.Open);
                }
                catch (IOException)
                {
                    Assert.IsTrue(true);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }
            }
        }

        [TestMethod]
        public void Properties()
        {
            string tempFile = Path.GetTempFileName();
            FileInfo fi = new FileInfo(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            Assert.AreEqual(efi.Name, fi.Name);
            Assert.AreEqual(efi.LastWriteTimUtc, fi.LastWriteTimeUtc);
            Assert.AreEqual(efi.LastWriteTime, fi.LastWriteTime);
            Assert.AreEqual(efi.LastAccessTimeUtc, fi.LastAccessTimeUtc);
            Assert.AreEqual(efi.LastAccessTime, fi.LastAccessTime);
            Assert.AreEqual(efi.FullName, fi.FullName);
            Assert.AreEqual(efi.Extension, fi.Extension);
            Assert.AreEqual(efi.Exists, fi.Exists);
            Assert.AreEqual(efi.CreationTime, fi.CreationTime);
            Assert.AreEqual(efi.CreateTimeUtc, fi.CreationTimeUtc);
            Assert.AreEqual(efi.Attributes, fi.Attributes);
            Assert.AreEqual(efi.Directory.FullName, fi.Directory.FullName);
            Assert.AreEqual(efi.DirectoryName, fi.DirectoryName);
            Assert.AreEqual(efi.IsReadOnly, fi.IsReadOnly);
            Assert.AreEqual(efi.Length, fi.Length);
        }

        [TestMethod]
        public void Properties1()
        {
            string tempFile = Path.GetTempFileName();
            FileVersionInfo fi = FileVersionInfo.GetVersionInfo(tempFile);

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            Assert.AreEqual(efi.Comments, fi.Comments);
            Assert.AreEqual(efi.CompanyName, fi.CompanyName);
            Assert.AreEqual(efi.FileBuildPart, fi.FileBuildPart);
            Assert.AreEqual(efi.FileDescription, fi.FileDescription);
            Assert.AreEqual(efi.FileMajorPart, fi.FileMajorPart);
            Assert.AreEqual(efi.FileMinorPart, fi.FileMinorPart);
            Assert.AreEqual(efi.FileName, fi.FileName);
            Assert.AreEqual(efi.FilePrivatePart, fi.FilePrivatePart);
            Assert.AreEqual(efi.FileVersion, fi.FileVersion);
            Assert.AreEqual(efi.InternalName, fi.InternalName);
            Assert.AreEqual(efi.IsDebug, fi.IsDebug);
            Assert.AreEqual(efi.IsPatched, fi.IsPatched);
            Assert.AreEqual(efi.IsPreRelease, fi.IsPreRelease);
            Assert.AreEqual(efi.IsPrivateBuild, fi.IsPrivateBuild);
            Assert.AreEqual(efi.IsSpecialBuild, fi.IsSpecialBuild);
            Assert.AreEqual(efi.Language, fi.Language);
            Assert.AreEqual(efi.LegalCopyright, fi.LegalCopyright);
            Assert.AreEqual(efi.LegalTrademarks, fi.LegalTrademarks);
            Assert.AreEqual(efi.OriginalFilename, fi.OriginalFilename);
            Assert.AreEqual(efi.PrivateBuild, fi.PrivateBuild);
            Assert.AreEqual(efi.ProductBuildPart, fi.ProductBuildPart);
            Assert.AreEqual(efi.ProductMajorPart, fi.ProductMajorPart);
            Assert.AreEqual(efi.ProductMinorPart, fi.ProductMinorPart);
            Assert.AreEqual(efi.ProductName, fi.ProductName);
            Assert.AreEqual(efi.ProductPrivatePart, fi.ProductPrivatePart);
            Assert.AreEqual(efi.ProductVersion, fi.ProductVersion);
            Assert.AreEqual(efi.SpecialBuild, fi.SpecialBuild);
         }

        [TestMethod]
        public void Properties2()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            efi.Attributes |= FileAttributes.ReadOnly;

            FileInfo fi = new FileInfo(tempFile);

            Assert.AreEqual(efi.Attributes, fi.Attributes);

            fi.Attributes &= ~FileAttributes.ReadOnly;
        }

        [TestMethod]
        public void Properties3()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            efi.CreationTime = new DateTime(2006, 1, 1);

            FileInfo fi = new FileInfo(tempFile);

            Assert.AreEqual(efi.CreationTime, fi.CreationTime);
        }

        [TestMethod]
        public void Properties4()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            efi.IsReadOnly = true;

            FileInfo fi = new FileInfo(tempFile);

            Assert.IsTrue((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);

            fi.Attributes &= ~FileAttributes.ReadOnly;
        }

        [TestMethod]
        public void FileOwner()
        {
            string tempFile = Path.GetTempFileName();

            FileInfo fi = new FileInfo(tempFile);
            FileSecurity fs = fi.GetAccessControl(AccessControlSections.Owner);
            string expected = fs.GetOwner(typeof(NTAccount)).ToString();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);
            string actual = efi.FileOwner;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Properties5()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            Assert.IsNotNull(efi);

            Assert.IsNotNull(efi.FileType);
            Assert.AreEqual(ExecutableType.Unknown, efi.ExecutableType);

            string systemPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.System);
            DirectoryInfo di = new DirectoryInfo(systemPath);
            tempFile = Path.Combine(di.Parent.FullName, "explorer.exe");
            efi = new ExtendedFileInfo(tempFile);
            Assert.AreEqual("Application", efi.FileType);
            Assert.AreEqual(ExecutableType.Windows, efi.ExecutableType);

            tempFile = Path.Combine(Path.Combine(di.Parent.FullName, "system32"), "at.exe");
            efi = new ExtendedFileInfo(tempFile);
            Assert.AreEqual(ExecutableType.Win32Console, efi.ExecutableType);

            tempFile = Path.Combine(Path.Combine(di.Parent.FullName, "system32"), "more.com");
            efi = new ExtendedFileInfo(tempFile);
            Assert.AreEqual(ExecutableType.DOS, efi.ExecutableType);

            tempFile = Path.Combine(di.Parent.FullName, "system.ini");
            efi = new ExtendedFileInfo(tempFile);
            Assert.AreEqual(ExecutableType.Unknown, efi.ExecutableType);
        }

        [TestMethod]
        [Ignore]
        public void Encryption()
        {
            string tempFile = Path.GetTempFileName();

            ExtendedFileInfo efi = new ExtendedFileInfo(tempFile);
            efi.Encrypt();
            efi.Refresh();

            Assert.IsTrue((efi.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted);

            efi.Decrypt();
            efi.Refresh();
            Assert.IsFalse((efi.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted);
        }
    }
}
