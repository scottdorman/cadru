//------------------------------------------------------------------------------
// <copyright file="ViewAttributeTests.cs"
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
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Annotations.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ViewAttributeTests
    {
        [TestMethod]
        public void BasicTests()
        {
            var attribute = new ViewAttribute("test");
            Assert.AreEqual("test", attribute.Name);
            Assert.IsTrue(String.IsNullOrWhiteSpace(attribute.Schema));

            attribute = new ViewAttribute("test") { Schema = "dbo" };
            Assert.AreEqual("test", attribute.Name);
            Assert.AreEqual("dbo", attribute.Schema);

            ExceptionAssert.Throws<ArgumentNullException>(() => new ViewAttribute(null));
            ExceptionAssert.Throws<ArgumentException>(() => new ViewAttribute(String.Empty));

            ExceptionAssert.Throws<ArgumentNullException>(() => new ViewAttribute("test") { Schema = null });
            ExceptionAssert.Throws<ArgumentException>(() => new ViewAttribute("test") { Schema = String.Empty });
        }
    }
}