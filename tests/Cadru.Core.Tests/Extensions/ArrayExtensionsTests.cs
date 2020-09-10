//------------------------------------------------------------------------------
// <copyright file="ArrayExtensionsTests.cs"
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

using Cadru.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void BytesToBinaryString()
        {
            Assert.AreEqual("[100]", new byte[] { 4 }.BytesToBinaryString());
            Assert.AreEqual("[1001011]", new byte[] { 75 }.BytesToBinaryString());
            Assert.AreEqual("[100][1001011]", new byte[] { 4, 75 }.BytesToBinaryString());
        }

        [TestMethod]
        public void BytesToString()
        {
            Assert.AreEqual("04", new byte[] { 4 }.BytesToString());
            Assert.AreEqual("4b", new byte[] { 75 }.BytesToString());
            Assert.AreEqual("044b", new byte[] { 4, 75 }.BytesToString());
        }

        [TestMethod]
        public void ReverseArray()
        {
            var array = new byte[] { 4, 75 };
            var reversed = array.ReverseArray();

            CollectionAssert.AreEqual(new byte[] { 75, 4 }, reversed);
            CollectionAssert.AreEqual(new byte[] { 4, 75 }, array);
            CollectionAssert.AreNotEqual(array, reversed);
        }

        [TestMethod]
        public void ReverseArrayInPlace()
        {
            var array = new byte[] { 4, 75 };
            array.ReverseArrayInPlace();

            CollectionAssert.AreEqual(new byte[] { 75, 4 }, array);
            CollectionAssert.AreNotEqual(new byte[] { 4, 75 }, array);
        }
    }
}