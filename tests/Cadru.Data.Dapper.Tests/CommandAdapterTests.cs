//------------------------------------------------------------------------------
// <copyright file="CommandAdapterTests.cs"
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Dapper.Tests
{
    [TestClass]
    public class CommandAdapterTests
    {
        [TestMethod]
        public void IsValidIdentifier()
        {
            var adapter = new CommandAdapter();
            Assert.IsTrue(adapter.IsValidIdentifier("test"));
            Assert.IsTrue(adapter.IsValidIdentifier("@test"));
            Assert.IsTrue(adapter.IsValidIdentifier("#test"));
            Assert.IsTrue(adapter.IsValidIdentifier("_test"));
            Assert.IsFalse(adapter.IsValidIdentifier("1test"));
            Assert.IsFalse(adapter.IsValidIdentifier("$test"));
            Assert.IsFalse(adapter.IsValidIdentifier("t est"));
            Assert.IsFalse(adapter.IsValidIdentifier("t'est"));
            Assert.IsFalse(adapter.IsValidIdentifier("[test]"));
            Assert.IsTrue(adapter.IsValidIdentifier("@@test"));
            Assert.IsTrue(adapter.IsValidIdentifier("##test"));
            Assert.IsTrue(adapter.IsValidIdentifier("@_test"));
            Assert.IsTrue(adapter.IsValidIdentifier("@#test"));
            Assert.IsTrue(adapter.IsValidIdentifier("t$est"));
        }

        [TestMethod]
        public void QouteStringLiteral()
        {
            var adapter = new CommandAdapter();
            Assert.AreEqual("'test'", adapter.QuoteStringLiteral("test"));
            Assert.AreEqual("'tes''t'", adapter.QuoteStringLiteral("tes't"));
        }

        [TestMethod]
        public void QuoteIdentifier()
        {
            var adapter = new CommandAdapter();
            Assert.AreEqual("test", adapter.QuoteIdentifier("test"));
            Assert.ThrowsException<InvalidOperationException>(() => adapter.QuoteIdentifier("tes't"));

            adapter = new SqlCommandAdapter();
            Assert.AreEqual("[test]", adapter.QuoteIdentifier("test"));
            Assert.ThrowsException<InvalidOperationException>(() => adapter.QuoteIdentifier("tes't"));
        }

        [TestMethod]
        public void UnquoteIdentifier()
        {
            var adapter = new CommandAdapter();
            Assert.AreEqual("[test]", adapter.UnquoteIdentifier("[test]"));
            Assert.AreEqual("[tes''t]", adapter.UnquoteIdentifier("[tes''t]"));

            adapter = new SqlCommandAdapter();
            Assert.AreEqual("test", adapter.UnquoteIdentifier("[test]"));
            Assert.ThrowsException<InvalidOperationException>(() => adapter.UnquoteIdentifier("[tes't]"));
        }
    }
}