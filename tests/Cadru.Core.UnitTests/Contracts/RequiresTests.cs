using Cadru.Contracts;
using Cadru.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Cadru.UnitTest.Framework.UnitTests.Contracts
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RequiresTests
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            Trace.Listeners.Clear();
        }

        [TestMethod]
        public void IsFalse()
        {
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsFalse(true));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsFalse(true, "test.")).WithMessage("test.");
            ExceptionAssert.Throws<ArgumentException>(() => Requires.IsFalse(true, "param", "test.")).WithMessage("test.", ExceptionMessageComparison.StartsWith).WithParameter("param");

            Requires.IsFalse(false);
        }

        [TestMethod]
        public void IsTrue()
        {
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsTrue(false));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsTrue(false, "test.")).WithMessage("test.");
            ExceptionAssert.Throws<ArgumentException>(() => Requires.IsTrue(false, "param", "test.")).WithMessage("test.", ExceptionMessageComparison.StartsWith).WithParameter("param");

            Requires.IsTrue(true);
        }

        [TestMethod]
        public void NotNull()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNull(((string)null), "param")).WithParameter("param");
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNull(((string)null), "param", "test")).WithMessage("test", ExceptionMessageComparison.StartsWith).WithParameter("param");
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNull(((string)null), "param", null)).WithParameter("param");
            Requires.NotNull("", "");


            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNull(((IEnumerable)null), "param")).WithParameter("param");
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNull(((List<string>)null), "param")).WithParameter("param");
        }

        [TestMethod]
        public void NotNullOrEmpty()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNullOrEmpty(((string)null), "param")).WithParameter("param");
            ExceptionAssert.Throws<ArgumentException>(() => Requires.NotNullOrEmpty(String.Empty, "param")).WithParameter("param");

            Requires.NotNullOrEmpty("test", "param");

            ExceptionAssert.Throws<ArgumentException>(() => Requires.NotNullOrEmpty(Enumerable.Empty<string>(), "param"));
            ExceptionAssert.Throws<ArgumentException>(() => Requires.NotNullOrEmpty(Enumerable.Empty<string>(), "param", "test"));
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNullOrEmpty(((IEnumerable<string>)null), "param"));
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNullOrEmpty(((IEnumerable<string>)null), "param", "test"));
        }

        [TestMethod]
        public void ValidRange()
        {
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => Requires.ValidRange(true, "param")).WithParameter("param");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => Requires.ValidRange(true, "param", "test")).WithMessage("test", ExceptionMessageComparison.StartsWith).WithParameter("param");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => Requires.ValidRange(true, "param", null)).WithParameter("param");
            ExceptionAssert.DoesNotThrow(() => Requires.ValidRange(false, "param"));
        }

        [TestMethod]
        public void ValidElements()
        {
            List<string> values = new List<string>()
            {
                "this",
                null,
                "is",
                "a",
                "test"
            };

            ExceptionAssert.Throws<ArgumentException>(() => Requires.NotNullElements(values, "param"));
            ExceptionAssert.Throws<ArgumentException>(() => Requires.ValidElements(values, x => !String.IsNullOrWhiteSpace(x), "param", "test"));

            values.Remove(null);

            ExceptionAssert.DoesNotThrow(() => Requires.NotNullElements(values, "param"));
            ExceptionAssert.DoesNotThrow(() => Requires.ValidElements(values, x => !String.IsNullOrWhiteSpace(x), "param", "test"));
        }

        internal class DisposeTester : IDisposablePattern
        {
            public DisposeTester(bool flag)
            {
                Disposed = flag;
            }

            public bool Disposed
            {
                get;
                private set;
            }

            public void Dispose()
            {
                
            }
        }

        [TestMethod]
        public void NotDisposed()
        {
            ExceptionAssert.Throws<ObjectDisposedException>(() => Requires.NotDisposed(new DisposeTester(true), "test")).WithMessage("Object name: 'test'.", ExceptionMessageComparison.EndsWith);
            ExceptionAssert.DoesNotThrow(() => Requires.NotDisposed(new DisposeTester(false), "test"));
        }

    }
}
