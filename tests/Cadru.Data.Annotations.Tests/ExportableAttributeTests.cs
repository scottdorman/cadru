//------------------------------------------------------------------------------
// <copyright file="ExportableAttributeTests.cs"
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

using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Annotations.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExportableAttributeTests
    {
        [TestMethod]
        public void BasicTests()
        {
            Assert.IsTrue(new ExportableAttribute(true).AllowExport);
            Assert.IsFalse(new ExportableAttribute(false).AllowExport);
            Assert.AreEqual(100, new ExportableAttribute(true) { Order = 100 }.Order);
            Assert.AreEqual(ExportableAttribute.DefaultOrder, new ExportableAttribute(true).Order);
        }
    }
}