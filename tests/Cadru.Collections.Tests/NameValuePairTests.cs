//------------------------------------------------------------------------------
// <copyright file="NameValuePairTests.cs"
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

using System.Collections;
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Collections.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class NameValuePairTests
    {
        [TestMethod]
        public void Add()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");
            CustomAssert.IsNotEmpty((ICollection)nvp.Value);
            Assert.IsTrue(nvp.Value.Count == 2);
            Assert.AreEqual("one", nvp.Value[0]);
            Assert.AreEqual("two", nvp.Value[1]);
        }

        [TestMethod]
        public void Constructor()
        {
            var nvp = new NameValuePair<string>("test");
            Assert.IsNotNull(nvp.Key);
            Assert.AreEqual("test", nvp.Key);
            Assert.IsNotNull(nvp.Value);
            CustomAssert.IsEmpty((ICollection)nvp.Value);
        }

        [TestMethod]
        public void Equals()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");

            var nvp2 = new NameValuePair<string>("test");
            nvp2.Value.Add("one");
            nvp2.Value.Add("two");

            var nvp3 = new NameValuePair<string>("test3");
            nvp3.Value.Add("one");
            nvp3.Value.Add("two");

            Assert.IsTrue(nvp == nvp2);
            Assert.IsFalse(nvp != nvp2);
            Assert.IsTrue(nvp.Equals(nvp2));
            Assert.IsTrue(nvp.Equals((object)nvp2));

            Assert.IsFalse(nvp == null);
            Assert.IsTrue(nvp != null);
            Assert.IsFalse(nvp.Equals(null));

            Assert.IsFalse(nvp.Equals("test"));
            Assert.IsFalse(nvp == nvp3);
            Assert.IsTrue(nvp != nvp3);
            Assert.IsFalse(nvp.Equals(nvp3));
            Assert.IsFalse(nvp.Equals((object)nvp3));
        }

        [TestMethod]
        public void GetHashCodeTests()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");

            Assert.AreEqual("test".GetHashCode(), nvp.GetHashCode());
        }

        [TestMethod]
        public void String()
        {
            var nvp = new NameValuePair<string>("test");
            nvp.Value.Add("one");
            nvp.Value.Add("two");

            Assert.AreEqual("[test: one, two]", nvp.ToString());
        }
    }
}