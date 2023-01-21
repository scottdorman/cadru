using System;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// A collection of helpers to test various conditions within unit tests.
    /// If the condition being tested is not met, an exception is thrown.
    /// </summary>
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static partial class StringAssertExtensions
    {
        private static readonly object[] Empty = Array.Empty<object>();
    }
}
