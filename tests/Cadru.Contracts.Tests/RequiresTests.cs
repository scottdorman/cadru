//------------------------------------------------------------------------------
// <copyright file="RequiresTests.cs"
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Contracts.Tests
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
        public void IsEnum()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.IsEnum(null, "foo"));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsEnum(2, "foo"));
            Requires.IsEnum(DayOfWeek.Friday, "foo");

            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.IsEnum((object)null, "foo"));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsEnum((object)2, "foo"));
            Requires.IsEnum((object)DayOfWeek.Friday, "foo");
        }

        [TestMethod]
        public void IsFalse()
        {
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsFalse(true));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsFalse(true, "test.")).WithMessage("test.");
            ExceptionAssert.Throws<ArgumentException>(() => Requires.IsFalse(true, "param", "test.")).WithMessage("test.", ExceptionMessageComparison.StartsWith).WithParameter("param");

            Requires.IsFalse(false);
            Requires.IsFalse(false, "param");
            Requires.IsFalse(false, "param", "test");
        }

        [TestMethod]
        public void IsTrue()
        {
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsTrue(false));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsTrue(false, "test.")).WithMessage("test.");
            ExceptionAssert.Throws<ArgumentException>(() => Requires.IsTrue(false, "param", "test.")).WithMessage("test.", ExceptionMessageComparison.StartsWith).WithParameter("param");

            Requires.IsTrue(true);
            Requires.IsTrue(true, "param");
            Requires.IsTrue(true, "param", "test");
        }

        [TestMethod]
        public void IsType()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.IsType(null, typeof(string), "foo"));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsType(2, typeof(string), "foo"));
            Requires.IsType(String.Empty, typeof(string), "foo");

            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.IsType<string>(null, "foo"));
            ExceptionAssert.Throws<InvalidOperationException>(() => Requires.IsType<string>(2, "foo"));
            Requires.IsType<string>(String.Empty, "foo");
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
            Requires.NotNullOrEmpty("test", "param", "test");

            Requires.NotNullOrEmpty(new[] { 0, 1, 2 }, "param", "test");
            Requires.NotNullOrEmpty(new[] { "A", "B", "C" }, "param", "test");

            ExceptionAssert.Throws<ArgumentException>(() => Requires.NotNullOrEmpty(Enumerable.Empty<string>(), "param"));
            ExceptionAssert.Throws<ArgumentException>(() => Requires.NotNullOrEmpty(Enumerable.Empty<string>(), "param", "test"));
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNullOrEmpty(((IEnumerable<string>)null), "param"));
            ExceptionAssert.Throws<ArgumentNullException>(() => Requires.NotNullOrEmpty(((IEnumerable<string>)null), "param", "test"));
        }

        [TestMethod]
        public void ValidElements()
        {
            var values = new List<string>()
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

        [TestMethod]
        public void ValidRange()
        {
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => Requires.ValidRange(true, "param")).WithParameter("param");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => Requires.ValidRange(true, "param", "test")).WithMessage("test", ExceptionMessageComparison.StartsWith).WithParameter("param");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => Requires.ValidRange(true, "param", null)).WithParameter("param");
            ExceptionAssert.DoesNotThrow(() => Requires.ValidRange(false, "param"));
        }

        internal class DisposeTester : IDisposablePattern
        {
            public DisposeTester(bool flag)
            {
                this.Disposed = flag;
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

        //[TestMethod]
        //public void NotDisposed()
        //{
        //    ExceptionAssert.Throws<ObjectDisposedException>(() => Requires.NotDisposed(new DisposeTester(true), "test")).WithMessage("Object name: 'test'.", ExceptionMessageComparison.EndsWith);
        //    ExceptionAssert.DoesNotThrow(() => Requires.NotDisposed(new DisposeTester(false), "test"));
        //}
    }
}