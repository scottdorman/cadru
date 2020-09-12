using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

using Cadru.Data.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Tests
{
    /// <summary>Base class for test classes the use temporary files that need to be cleaned up.</summary>
    public abstract class FileCleanupTestBase : IDisposable
    {
        private string fallbackGuid = Guid.NewGuid().ToString("N").Substring(0, 10);

        /// <summary>Initialize the test class base.  This creates the associated test directory.</summary>
        protected FileCleanupTestBase()
        {
            // Use a unique test directory per test class.  The test directory lives in the user's temp directory,
            // and includes both the name of the test class and a random string.  The test class name is included
            // so that it can be easily correlated if necessary, and the random string to helps avoid conflicts if
            // the same test should be run concurrently with itself (e.g. if a [Fact] method lives on a base class)
            // or if some stray files were left over from a previous run.

            // Make 3 attempts since we have seen this on rare occasions fail with access denied, perhaps due to machine
            // configuration, and it doesn't make sense to fail arbitrary tests for this reason.
            string failure = string.Empty;
            for (int i = 0; i <= 2; i++)
            {
                TestDirectory = Path.Combine(Path.GetTempPath(), GetType().Name + "_" + Path.GetRandomFileName());
                try
                {
                    Directory.CreateDirectory(TestDirectory);
                    break;
                }
                catch (Exception ex)
                {
                    failure += ex.ToString() + Environment.NewLine;
                    Thread.Sleep(10); // Give a transient condition like antivirus/indexing a chance to go away
                }
            }

            Assert.IsTrue(Directory.Exists(TestDirectory), $"FileCleanupTestBase failed to create {TestDirectory}. {failure}");
        }

        /// <summary>Delete the associated test directory.</summary>
        ~FileCleanupTestBase()
        {
            Dispose(false);
        }

        /// <summary>Delete the associated test directory.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Delete the associated test directory.</summary>
        protected virtual void Dispose(bool disposing)
        {
            // No managed resources to clean up, so disposing is ignored.

            try { Directory.Delete(TestDirectory, recursive: true); }
            catch { } // avoid exceptions escaping Dispose
        }

        /// <summary>
        /// Gets the test directory into which all files and directories created by tests should be stored.
        /// This directory is isolated per test class.
        /// </summary>
        protected string TestDirectory { get; }

        /// <summary>Gets a test file full path that is associated with the call site.</summary>
        /// <param name="index">An optional index value to use as a suffix on the file name.  Typically a loop index.</param>
        /// <param name="memberName">The member name of the function calling this method.</param>
        /// <param name="lineNumber">The line number of the function calling this method.</param>
        protected string GetTestFilePath(int? index = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0) =>
            Path.Combine(TestDirectory, GetTestFileName(index, memberName, lineNumber));

        /// <summary>Gets a test file name that is associated with the call site.</summary>
        /// <param name="index">An optional index value to use as a suffix on the file name.  Typically a loop index.</param>
        /// <param name="memberName">The member name of the function calling this method.</param>
        /// <param name="lineNumber">The line number of the function calling this method.</param>
        protected string GetTestFileName(int? index = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            string testFileName = GenerateTestFileName(index, memberName, lineNumber);
            string testFilePath = Path.Combine(TestDirectory, testFileName);

            const int maxLength = 260 - 5; // Windows MAX_PATH minus a bit

            int excessLength = testFilePath.Length - maxLength;

            if (excessLength > 0)
            {
                // The path will be too long for Windows -- can we
                // trim memberName to fix it?
                if (excessLength < memberName.Length + "...".Length)
                {
                    // Take a chunk out of the middle as perhaps it's the least interesting part of the name
                    memberName = memberName.Substring(0, memberName.Length / 2 - excessLength / 2) + "..." + memberName.Substring(memberName.Length / 2 + excessLength / 2);

                    testFileName = GenerateTestFileName(index, memberName, lineNumber);
                    testFilePath = Path.Combine(TestDirectory, testFileName);
                }
                else
                {
                    return fallbackGuid;
                }
            }

            Debug.Assert(testFilePath.Length <= maxLength + "...".Length);

            return testFileName;
        }

        private string GenerateTestFileName(int? index, string memberName, int lineNumber) =>
            string.Format(
                index.HasValue ? "{0}_{1}_{2}_{3}" : "{0}_{1}_{3}",
                memberName ?? "TestBase",
                lineNumber,
                index.GetValueOrDefault(),
                Guid.NewGuid().ToString("N").Substring(0, 8)); // randomness to avoid collisions between derived test classes using same base method concurrently
    }


    [TestClass]
    public class TextFieldParserTests : FileCleanupTestBase
    {
        [TestMethod]
        public void Constructors()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path, "abc123");

            // public TextFieldParser(System.IO.Stream stream)
            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var parser = new TextFieldParser(stream))
                {
                }
                Assert.ThrowsException<ObjectDisposedException>(() => stream.ReadByte());
            }

            // public TextFieldParser(System.IO.Stream stream, System.Text.Encoding defaultEncoding, bool detectEncoding, bool leaveOpen);
            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var parser = new TextFieldParser(stream, defaultEncoding: System.Text.Encoding.Unicode, detectEncoding: true, leaveOpen: true))
                {
                }
                _ = stream.ReadByte();
            }

            // public TextFieldParser(System.IO.TextReader reader)
            using (var reader = new StreamReader(path))
            {
                using (var parser = new TextFieldParser(reader))
                {
                }
                Assert.ThrowsException<ObjectDisposedException>(() => reader.ReadToEnd());
            }

            // public TextFieldParser(string path)
            using (var parser = new TextFieldParser(path))
            {
            }

            // public TextFieldParser(string path)
            Assert.ThrowsException<FileNotFoundException>(() => new TextFieldParser(GetTestFilePath()));
        }

        [TestMethod]
        public void Close()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path, "abc123");

            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var parser = new TextFieldParser(stream))
                {
                    parser.Close();
                }
            }

            using (var parser = new TextFieldParser(path))
            {
                parser.Close();
            }

            {
                var parser = new TextFieldParser(path);
                parser.Close();
                parser.Close();
            }

            {
                TextFieldParser parser;
                using (parser = new TextFieldParser(path))
                {
                }
                parser.Close();
            }
        }

        [TestMethod]
        public void Properties()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path, "abc123");

            using (var parser = new TextFieldParser(path))
            {
                CollectionAssert.AreEquivalent(new string[0], parser.CommentTokens.ToArray());
                parser.SetCommentTokens(new[] { "[", "]" });
                CollectionAssert.AreEquivalent(new[] { "[", "]" }, parser.CommentTokens.ToArray());

                CollectionAssert.AreEquivalent(Array.Empty<string>(), parser.Delimiters.ToArray());
                parser.SetDelimiters(new[] { "A", "123" });
                CollectionAssert.AreEquivalent(new[] { "A", "123" }, parser.Delimiters.ToArray());
                parser.SetDelimiters(new[] { "123", "B" });
                CollectionAssert.AreEquivalent(new[] { "123", "B" }, parser.Delimiters.ToArray());

                CollectionAssert.AreEquivalent(Array.Empty<int>(), parser.FieldWidths.ToArray());
                parser.SetFieldWidths(new[] { 1, 2, int.MaxValue });
                CollectionAssert.AreEquivalent(new[] { 1, 2, int.MaxValue }, parser.FieldWidths.ToArray());
                parser.SetFieldWidths(new[] { int.MaxValue, 3 });
                CollectionAssert.AreEquivalent(new[] { int.MaxValue, 3 }, parser.FieldWidths.ToArray());
                Assert.ThrowsException<ArgumentException>(() => parser.SetFieldWidths(new[] { -1, -1 }));

                Assert.IsTrue(parser.HasFieldsEnclosedInQuotes);
                parser.HasFieldsEnclosedInQuotes = false;
                Assert.IsFalse(parser.HasFieldsEnclosedInQuotes);

                Assert.AreEqual(FieldType.Delimited, parser.TextFieldType);
                parser.TextFieldType = FieldType.FixedWidth;
                Assert.AreEqual(FieldType.FixedWidth, parser.TextFieldType);

                Assert.IsTrue(parser.TrimWhiteSpace);
                parser.TrimWhiteSpace = false;
                Assert.IsFalse(parser.TrimWhiteSpace);
            }
        }

        // Not tested:
        //   public string[] CommentTokens { get { throw null; } set { } }

        [TestMethod]
        public void ErrorLine()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path,
@"abc 123
def 45
ghi 789");

            using (var parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.FixedWidth;
                parser.SetFieldWidths(new[] { 3, 4 });

                Assert.AreEqual(-1, parser.ErrorLineNumber);
                Assert.AreEqual("", parser.ErrorLine);

                CollectionAssert.AreEquivalent(new[] { "abc", "123" }, parser.ReadFields());
                Assert.AreEqual(-1, parser.ErrorLineNumber);
                Assert.AreEqual("", parser.ErrorLine);

                Assert.ThrowsException<MalformedLineException>(() => parser.ReadFields());
                Assert.AreEqual(2, parser.ErrorLineNumber);
                Assert.AreEqual("def 45", parser.ErrorLine);

                CollectionAssert.AreEquivalent(new[] { "ghi", "789" }, parser.ReadFields());
                Assert.AreEqual(2, parser.ErrorLineNumber);
                Assert.AreEqual("def 45", parser.ErrorLine);
            }
        }

        [TestMethod]
        public void HasFieldsEnclosedInQuotes_TrimWhiteSpace()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path, @""""", "" "" ,""abc"", "" 123 "" ,");

            using (var parser = new TextFieldParser(path))
            {
                parser.SetDelimiters(new[] { "," });
                CollectionAssert.AreEquivalent(new[] { "", "", "abc", "123", "" }, parser.ReadFields());
            }

            using (var parser = new TextFieldParser(path))
            {
                parser.TrimWhiteSpace = false;
                parser.SetDelimiters(new[] { "," });
                CollectionAssert.AreEquivalent(new[] { "", " ", "abc", " 123 ", "" }, parser.ReadFields());
            }

            using (var parser = new TextFieldParser(path))
            {
                parser.HasFieldsEnclosedInQuotes = false;
                parser.SetDelimiters(new[] { "," });
                CollectionAssert.AreEquivalent(new[] { @"""""", @""" """, @"""abc""", @""" 123 """, "" }, parser.ReadFields());
            }

            using (var parser = new TextFieldParser(path))
            {
                parser.TrimWhiteSpace = false;
                parser.HasFieldsEnclosedInQuotes = false;
                parser.SetDelimiters(new[] { "," });
                CollectionAssert.AreEquivalent(new[] { @"""""", @" "" "" ", @"""abc""", @" "" 123 "" ", "" }, parser.ReadFields());
            }
        }

        [TestMethod]
        public void PeekChars()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path,
@"abc,123
def,456
ghi,789");

            using (var parser = new TextFieldParser(path))
            {
                Assert.ThrowsException<ArgumentException>(() => parser.PeekChars(0));

                Assert.AreEqual("a", parser.PeekChars(1));
                Assert.AreEqual("abc,123", parser.PeekChars(10));

                Assert.AreEqual("abc,123", parser.ReadLine());

                parser.TextFieldType = FieldType.FixedWidth;
                parser.SetFieldWidths(new[] { 3, -1 });

                Assert.AreEqual("d", parser.PeekChars(1));
                Assert.AreEqual("def,456", parser.PeekChars(10));
                CollectionAssert.AreEquivalent(new[] { "def", ",456" }, parser.ReadFields());

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(new[] { "," });

                Assert.AreEqual("g", parser.PeekChars(1));
                Assert.AreEqual("ghi,789", parser.PeekChars(10));
                CollectionAssert.AreEquivalent(new[] { "ghi", "789" }, parser.ReadFields());

                Assert.IsNull(parser.PeekChars(1));
                Assert.IsNull(parser.PeekChars(10));
            }
        }

        [TestMethod]
        public void ReadFields_FieldWidths()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path,
@"abc,123
def,456
ghi,789");

            using (var parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.FixedWidth;

                Assert.ThrowsException<InvalidOperationException>(() => parser.ReadFields());

                parser.SetFieldWidths(new[] { -1 });
                CollectionAssert.AreEquivalent(new[] { "abc,123" }, parser.ReadFields());

                parser.SetFieldWidths(new[] { 3, -1 });
                CollectionAssert.AreEquivalent(new[] { "def", ",456" }, parser.ReadFields());

                parser.SetFieldWidths(new[] { 3, 2 });
                CollectionAssert.AreEquivalent(new[] { "ghi", ",7" }, parser.ReadFields());

                parser.SetFieldWidths(new[] { 3, 2 });
                Assert.IsNull(parser.ReadFields());
            }
        }

        [TestMethod]
        public void ReadFields_Delimiters_LineNumber()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path,
@"abc,123
def,456
ghi,789");

            using (var parser = new TextFieldParser(path))
            {
                Assert.AreEqual(1, parser.LineNumber);

                Assert.ThrowsException<ArgumentException>(() => parser.ReadFields());
                Assert.AreEqual(1, parser.LineNumber);

                parser.SetDelimiters(new[] { ", " });
                CollectionAssert.AreEquivalent(new[] { "abc,123" }, parser.ReadFields());
                Assert.AreEqual(2, parser.LineNumber);

                parser.SetDelimiters(new[] { ";", "," });
                CollectionAssert.AreEquivalent(new[] { "def", "456" }, parser.ReadFields());
                Assert.AreEqual(3, parser.LineNumber);

                parser.SetDelimiters(new[] { "g", "9" });
                CollectionAssert.AreEquivalent(new[] { "", "hi,78", "" }, parser.ReadFields());
                Assert.AreEqual(-1, parser.LineNumber);
            }

            File.WriteAllText(path,
@",,
,
");

            using (var parser = new TextFieldParser(path))
            {
                Assert.AreEqual(1, parser.LineNumber);

                parser.SetDelimiters(new[] { "," });
                CollectionAssert.AreEquivalent(new[] { "", "", "" }, parser.ReadFields());
                Assert.AreEqual(2, parser.LineNumber);

                CollectionAssert.AreEquivalent(new[] { "", "" }, parser.ReadFields());
                Assert.AreEqual(-1, parser.LineNumber);

                Assert.IsNull(parser.ReadFields());
                Assert.AreEqual(-1, parser.LineNumber);

                Assert.IsNull(parser.ReadFields());
                Assert.AreEqual(-1, parser.LineNumber);
            }
        }

        [TestMethod]
        public void ReadLine_ReadToEnd()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path,
@"abc
123");

            using (var parser = new TextFieldParser(path))
            {
                Assert.IsFalse(parser.EndOfData);

                Assert.AreEqual(
@"abc
123",
                    parser.ReadToEnd());
                Assert.AreEqual(-1, parser.LineNumber);
                Assert.IsTrue(parser.EndOfData);
            }

            using (var parser = new TextFieldParser(path))
            {
                Assert.AreEqual("abc", parser.ReadLine());
                Assert.AreEqual(2, parser.LineNumber);
                Assert.IsFalse(parser.EndOfData);

                Assert.AreEqual("123", parser.ReadToEnd());
                Assert.AreEqual(-1, parser.LineNumber);
                Assert.IsTrue(parser.EndOfData);
            }

            using (var parser = new TextFieldParser(path))
            {
                Assert.AreEqual("abc", parser.ReadLine());
                Assert.AreEqual(2, parser.LineNumber);
                Assert.IsFalse(parser.EndOfData);

                Assert.AreEqual("123", parser.ReadLine());
                Assert.AreEqual(-1, parser.LineNumber);
                Assert.IsTrue(parser.EndOfData);

                Assert.IsNull(parser.ReadToEnd());
                Assert.AreEqual(-1, parser.LineNumber);
                Assert.IsTrue(parser.EndOfData);
            }
        }

        [TestMethod]
        public void UnmatchedQuote_MalformedLineException()
        {
            var path = GetTestFilePath();
            File.WriteAllText(path, @""""", """);

            using (var parser = new TextFieldParser(path))
            {
                parser.SetDelimiters(new[] { "," });
                Assert.ThrowsException<MalformedLineException>(() => parser.ReadFields());
            }
        }
    }
}
